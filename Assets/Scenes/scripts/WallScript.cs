﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.layer = 11;
		gameObject.tag = "Wall";	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Projectile"){
			Destroy(other.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Projectile"){
			Destroy(collision.gameObject);
		}	
	}
}
