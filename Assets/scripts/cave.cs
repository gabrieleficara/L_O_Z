using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cave : MonoBehaviour {
    public int scene;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().save();
            SceneManager.LoadScene(scene);
        }
    }
}
