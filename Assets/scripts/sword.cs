using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour {
	float timer;
	float s_timer;
	public bool special = false;
	public GameObject s_particle;

	// Use this for initialization
	void Start () {
		timer = .2f;
		s_timer = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		s_timer -= Time.deltaTime;
		timer_check();
    }

	void	timer_check()
	{
		if (timer <= 0)
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("att", 5); 
		if (!special) 
		    if (timer <= 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().c_move = true;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().c_attack = true;
				Destroy(gameObject);
			}
		if (s_timer <= 0)
		{
			Instantiate(s_particle, transform.position, transform.rotation);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().c_attack = true;
			Destroy(gameObject);
		}
	}

	public void c_part()
	{
		if (special)
			Instantiate(s_particle, transform.position, transform.rotation);
    }
}
