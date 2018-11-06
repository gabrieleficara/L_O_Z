using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environment : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("att", 5);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().c_move = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().c_attack = true;
        Destroy(col.gameObject);
    }
}
