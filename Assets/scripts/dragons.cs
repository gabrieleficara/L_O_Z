using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragons : MonoBehaviour {
	public int health;
	public GameObject particle;
	public float speed;
	Animator anim;
	int dir;
	float timer = 2f; 
    float t_attack = 2f;
    public GameObject projectile;
    int thrustpower = 200;
    float colltimer;
    bool coll;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
		dir = Random.Range(0,4);
        coll = false;
        colltimer = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        if (coll)
        {
            colltimer -= Time.deltaTime;
            if (colltimer <= 0)
            {
                coll = false;
                colltimer = 0.3f;
            }
        }
        timer -= Time.deltaTime;
		if (timer <= 0)
		{
			timer = 1.5f;
			dir = Random.Range(0,4); 	
		}
        t_attack -= Time.deltaTime;
        if (t_attack <= 0)
            attack();
		movement();
	}

    void attack()
    {
        GameObject newprojectile;
        t_attack = 2f;
        newprojectile = Instantiate(projectile, transform.position, transform.rotation);
        if (dir == 0)
            newprojectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustpower);
        if (dir == 1)
            newprojectile.GetComponent<Rigidbody2D>().AddForce(Vector2.down * thrustpower);
        if (dir == 2)
            newprojectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrustpower);
        if (dir == 3)
            newprojectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustpower);
    }

	void movement()
	{
		anim.SetInteger("dir", dir);
		if (dir == 0)
			transform.Translate(0, speed * Time.deltaTime, 0);
		else if (dir == 1)
			transform.Translate(0, - speed * Time.deltaTime, 0);
		else if (dir == 2)
			transform.Translate(- speed * Time.deltaTime, 0, 0);
		else if (dir == 3)
			transform.Translate(speed * Time.deltaTime, 0, 0);
	}

	void m_health()
	{
		health--;
		if (health <= 0)
		{
			Instantiate(particle, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Sword")
		{
			m_health();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("att", 5);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().c_move = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().c_attack = true;
            col.GetComponent<sword>().c_part();	
			Destroy (col.gameObject);
		}
        if (col.gameObject.tag == "Wall")
        {
            if (!coll)
            {
                coll = true;
                if (dir == 0)
                {
                    Debug.Log("shit!a");
                    dir = 1;
                }
                else if (dir == 1)
                {
                    Debug.Log("shit!b");
                    dir = 0;
                }
                else if (dir == 2)
                {
                    Debug.Log("shit!c");
                    dir = 3;
                }
                else if (dir == 3)
                {
                    Debug.Log("shit!dß");
                    dir = 2;
                }
            }
        }
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			m_health();
			if (col.gameObject.GetComponent<Player>().iniframe == false)
			{
				col.gameObject.GetComponent<Player>().iniframe = true;
				col.gameObject.GetComponent<Player>().current_health--;
			}
		}
		if (col.gameObject.tag == "Wall")
		{
            if (!coll)
            {
                coll = true;
                if (dir == 0)
                    dir = 1;
                else if (dir == 1)
                    dir = 0;
                else if (dir == 2)
                    dir = 3;
                else if (dir == 3)
                    dir = 2;
            }
        }
	}
}
