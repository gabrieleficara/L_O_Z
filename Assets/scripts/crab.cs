using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab : MonoBehaviour {
	public int health;
	public GameObject particle;
	SpriteRenderer s_renderer;
	public float speed;
	public Sprite[] facing;
	int dir;
	float timer = 1f;
    float colltimer;
    bool coll;

    // Use this for initialization
    void Start () {
		s_renderer = GetComponent<SpriteRenderer>();
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
			timer = 1f;
			dir = Random.Range(0,4); 	
		}
		movement();
    }

	void movement()
	{
		s_renderer.sprite = facing[dir];
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
