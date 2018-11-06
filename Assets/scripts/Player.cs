using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public float speed = 5;
	Animator anim;
	public Image[] hearts;
	public int max_health;
	public GameObject sword;
	public int current_health;
	public int thrustPower;
	public bool c_move;
	public bool c_attack;
	public bool iniframe = false;
	float initimer;
	public SpriteRenderer sr;
    public bool cam_move;
    public GameObject message;
    bool lost;

    // Use this for initialization
    void Start ()
	{
        lost = false;
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		anim.SetInteger("dir", 1);
        if (PlayerPrefs.HasKey("max_health"))
            load();
        else
    		current_health = max_health;
		c_move = true;
		c_attack = true;
		anim.SetInteger("att", 5);
		initimer = 1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKey("escape"))
            Application.Quit();
        get_health();
        if (Input.GetKeyDown(KeyCode.Space) && !lost)
			attack();
		if (iniframe == true)
		{
            if (current_health > 0)
            {
                sr.enabled = (sr.enabled == true) ? false : true;
                initimer -= Time.deltaTime;
                if (initimer <= 0)
                {
                    sr.enabled = true;
                    iniframe = false;
                    initimer = 1f;
                }
            }
            else
                sr.enabled = false;
        }
        if (!lost)
		    Movement(); 
	}

	void attack_dir(int sdir, GameObject newsword)
	{
		if (sdir == 0)
		{
			newsword.transform.Rotate(0, 0, 0);
			newsword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower); 
		}
		else if (sdir == 1)
		{
			newsword.transform.Rotate(0, 0, 180);
			newsword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * thrustPower);
		} 
		else if (sdir == 2)
		{
			newsword.transform.Rotate(0, 0, 90);
			newsword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrustPower); 
		}
		else if (sdir == 3)
		{
			newsword.transform.Rotate(0, 0, -90);
			newsword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower); 
		}
	}

	void attack()
	{
		GameObject	newsword;
		int			sdir;


        if (c_attack)
        {

            thrustPower = 200;
            c_attack = false;
            c_move = false;
            newsword = Instantiate(sword, transform.position, transform.rotation);
            if (current_health == max_health)
            {
                thrustPower = 500;
                c_move = true;
                newsword.GetComponent<sword>().special = true;
            }
            sdir = anim.GetInteger("dir");
            anim.SetInteger("att", sdir);
            attack_dir(sdir, newsword);
        }
	}

	void c_anim(float x, float y, int d)
	{
		anim.speed = 2;
		transform.Translate(x, y, 0);
		anim.SetInteger("dir", d);
	}

	void Movement()
	{
		if (!c_move)
			return;
        if (Input.GetKey(KeyCode.W))
			c_anim(0, speed * Time.deltaTime, 0);
		else if (Input.GetKey(KeyCode.S))
			c_anim(0, - (speed * Time.deltaTime), 1);
		else if (Input.GetKey(KeyCode.A))
			c_anim(- (speed * Time.deltaTime), 0, 2);
		else if (Input.GetKey(KeyCode.D))
			c_anim(speed * Time.deltaTime, 0, 3);
		else
			anim.speed = 0;
	}
	
	void get_health()
	{
		int i;

        if (current_health <= 0 && !lost)
        {
            lost = true;
            Instantiate(message, message.transform.position, message.transform.rotation);
        }
        i = 0;
		while (i < hearts.Length)
		{
			hearts[i].gameObject.SetActive(false);
			i++;
		} 
        i = 0;
        while (i < current_health)
        {
            hearts[i].gameObject.SetActive(true);
            i++;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "E_bullet")
        {
            if (!iniframe)
            {
                current_health--;
                if (current_health > 0)
                    iniframe = true;
                col.GetComponent<enemyprojectile>().c_part();
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Potion")
        {
            if (max_health < hearts.Length)
                max_health++;
            current_health = max_health;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Trophy")
        {
            lost = true;
            sr.enabled = false;
            Instantiate(message, message.transform.position, message.transform.rotation);
            Destroy(col.gameObject);
        }
    }

    public void save()
    {
        PlayerPrefs.SetInt("max_health", max_health);
        PlayerPrefs.SetInt("current_health", current_health);
    }

    public void load()
    {
        max_health = PlayerPrefs.GetInt("max_health");
        current_health =  PlayerPrefs.GetInt("current_health");
    }

    public void reset()
    {
        sr.enabled = true;
        PlayerPrefs.DeleteAll(); // This is the line needed
        SceneManager.LoadScene(0);
    }
}
