using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public Transform player;
	public float health = 100.0f;
	public float speed = 1;
	public GameObject Bullet;
	private CharacterController controller;
	private Vector3 MoveDirection;
	
	private float AttackTimer = 0;
	private float AttackCD = 0.5f;
	
	void Start () {
		controller = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
		
		MoveDirection = new Vector3((player.transform.position.x - transform.position.x),
						(player.transform.position.y - transform.position.y),
						(player.transform.position.z - transform.position.z)); 
		//transform.eulerAngles = new Vector3(0,0,z);
//		controller.SimpleMove(speed * MoveDirection);
		
		if (AttackTimer > 0) {
			AttackTimer -= Time.deltaTime;
		}
		
		if (AttackTimer <= 0) {
			var bullet = (GameObject)Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
			//bullet.GetComponent<Rigidbody2D>().velocity = 5 * MoveDirection;
			Destroy(bullet, 5.0f);
			AttackTimer = AttackCD;
		}
		
	}
	
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Call a damage function on the object we hit.
        //Debug.Log("ASDASD");
		if (hit.gameObject.CompareTag("Player") && AttackTimer <= 0) {
			//print(hit.gameObject.name);
			hit.gameObject.SendMessage("ApplyDamage", 5);
			AttackTimer = AttackCD;
		}
    }
}
