using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winlost : MonoBehaviour {
    SpriteRenderer s_renderer;
    public Sprite[] message;
    float timer;
    int health;
    Transform cam;

    // Use this for initialization
    void Start () {
        s_renderer = GetComponent<SpriteRenderer>();
        timer = 5f;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().current_health;
        if (health <= 0)
            s_renderer.sprite = message[1];
        else
            s_renderer.sprite = message[0];
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(cam.position.x, cam.position.y, 1);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().reset();
            Destroy(gameObject);
        }
	}
}
