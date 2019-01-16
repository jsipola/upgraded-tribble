using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour {

	private CharacterController controller;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += gameObject.transform.right * speed;
		//controller.SimpleMove(speed * transform.TransformDirection(Vector3.forward));
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Call a damage function on the object we hit.
        Debug.Log("ASDASD");
		if (hit.gameObject.CompareTag("Player")) {// && AttackTimer <= 0) {
			//print(hit.gameObject.name);
			hit.gameObject.SendMessage("ApplyDamage", 5);
//			AttackTimer = AttackCD;
		}
    }
}