using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove2d : MonoBehaviour {

	public Transform player;
	public float health;
	public GameObject projectile;
	public float BulletSpeed;
	public Transform[] locs;
	
	private Rigidbody2D rigidbody2D;
	private NavMeshAgent agent;
	private int destpoint = 0;
	private float AttackTimer = 0;
	private float AttackCD = 0.65f;
	private int facing = 1;
	
	void Start(){
		rigidbody2D = GetComponent<Rigidbody2D>();
		gameObject.tag = "Enemy";
		agent = GetComponent<NavMeshAgent>();
		agent.updateUpAxis = false;
		agent.autoBraking = false;

		GotoNext();
		//agent.destination = goal.position;
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		// turns always towards player
		float z = Mathf.Atan2((player.transform.position.y - transform.position.y),
							(player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

//		if (CastRay2()) {
			transform.eulerAngles = new Vector3(0,0,z);
//		}
		if (health <= 0) {
			Destroy(gameObject);
		}
		if (AttackTimer > 0) {
			//check for firing speed
			AttackTimer -= Time.deltaTime;
		} else {
			if (CastRay()) {
				Shoot();
			}
		}
		if (!agent.pathPending && agent.remainingDistance < 0.5f) {
			GotoNext();
		}
	}
	
	void ApplyDamage(float damage){
		//take damage
		health = health - damage;
		print(health);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage("ApplyDamage", 10);
		} else {
			Destroy(other.gameObject);
		}
	}
	
	bool CastRay(){
		//check if player can be seen
		 RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up *1f, transform.up);
		 if (hit.collider != null){
			 //print (hit.collider.tag);
			 if (hit.collider.tag == "Player") {
				 return true;
			 } else {
				 return false;
			 }
		 } else {
			 return false;
		 }
	}
	
	bool CastRay2(){
		//check if player can be seen
		Vector2 newas = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
		 RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * 1f, newas);
		 if (hit.collider != null){
			 //print (hit.collider.tag);
			 if (hit.collider.tag == "Player") {
				 return true;
			 } else {
				 return false;
			 }
		 } else {
			 return false;
		 }
	}
	
	void Shoot(){
		//shoot at current direction
		AttackTimer = AttackCD;
		GameObject clone = (GameObject)Instantiate(projectile) as GameObject;
		clone.transform.position = transform.position + transform.up *1f;
		clone.transform.rotation = transform.rotation;
		clone.SetActive(true);
		clone.GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed);
	}
	
	void GotoNext(){

		if (locs.Length == 0) return;

		agent.destination = locs[destpoint].position;

		destpoint = (destpoint + 1) % locs.Length;
	}
}
