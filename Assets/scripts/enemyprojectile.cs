using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyprojectile : MonoBehaviour {
    float timer;
    public GameObject particles;
	// Use this for initialization
	void Start () {
        timer = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void c_part()
    {
        Instantiate(particles, transform.position, transform.rotation);
    }
}
