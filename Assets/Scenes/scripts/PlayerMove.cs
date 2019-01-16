using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	private CharacterController controller;
	private Vector3 MoveDir = Vector3.zero;
	private float speed = 10.0f;
	private float health = 100;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		gameObject.tag = "Player";
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButton("Horizontal")) || (Input.GetButton("Vertical"))) {
			MoveDir = new Vector3(Input.GetAxis("Horizontal"), 6.0f, Input.GetAxis("Vertical"));
			controller.SimpleMove(speed * MoveDir);	
		}
	}
	
	
//	void OnControllerColliderHit(ControllerColliderHit hit){
//		health = health - 10;
//		Debug.Log("asdsad");
//	}
	
	void ApplyDamage(float damage){
		health = health - 10;
		print(health);
	}
	
}
