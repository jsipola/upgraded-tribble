using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove2d : MonoBehaviour {

	//projectile for player character

	public float speed;
	public int direction;
	public Vector2 moveDir = Vector2.zero;
	private Rigidbody2D rb2d;
//	public float ttl;
	
	void Start(){
//		Destroy(this, ttl);
		rb2d = GetComponent<Rigidbody2D>();
		gameObject.tag = "Projectile";
		gameObject.layer = 9;
	}
	
	// Update is called once per frame
	void Update () {
		/*switch (direction) {
			case 1:
				gameObject.transform.position += gameObject.transform.right * speed;
				break;
			case 2:
				gameObject.transform.position -= gameObject.transform.right * speed;
				break;
			case 3:
				gameObject.transform.position += gameObject.transform.up * speed;
				break;
			case 4:
				gameObject.transform.position -= gameObject.transform.up * speed;
				break;
		}
		*/
		rb2d.velocity = moveDir;
		//if (rb2d.velocity.magnitude > speed) {
		//	rb2d.velocity *= speed / rb2d.velocity.magnitude;
		//}

		//gameObject.transform.position += gameObject.transform.right * speed;
	}

	void OnTriggerEnter2D(Collider2D other) {
//		//Destroy(other.gameObject);
		other.gameObject.SendMessage("ApplyDamage", 10);
		//Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Projectile") {
			Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		} else if(collision.gameObject.tag == "Wall") {
			print("asdasd");
			//Destroy(gameObject);
		}
	}

}
