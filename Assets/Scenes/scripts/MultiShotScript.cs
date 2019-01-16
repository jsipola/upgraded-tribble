﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
	rb2d.rotation = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.rotation += 1.0f;
    }


    void OnTriggerEnter2D(Collider2D other){
    	//if (other.tag == "Player"){
		other.SendMessage("PowerUp", 1); //Multishot PowerUp
		Destroy(gameObject);
	//}
    }
}
