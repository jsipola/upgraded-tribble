using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2d : MonoBehaviour {

	public float speed;
	public float health;
	public projectileMove2d projectile;
	private Rigidbody2D rigidbody2D;
	private BoxCollider2D playerCollider;
	private int facing = 1;
	private int projSpeed = 35;
	private float input;
	private bool multiShot = false;
	
	private float AttackTimer = 0;
	private float AttackCD = 0.20f;
	
	void Start(){
		rigidbody2D = GetComponent<Rigidbody2D>();
		playerCollider = GetComponent<BoxCollider2D>();
		gameObject.tag = "Player";
	}
	
	// Use this for initialization
	void FixedUpdate(){
		
		if (AttackTimer > 0) {
			//check for attacking cooldown
			AttackTimer -= Time.deltaTime;
		}
		//zero velocity to prevent wobble
		rigidbody2D.velocity = Vector2.zero;
		//horizontal and vertical movements 
		//
		if (Input.GetButton("Horizontal")) {
			var MoveDir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
			
//			input = Input.GetAxis("Horizontal");
			input = Input.GetAxisRaw("Horizontal") * speed;
			if (input > 0 ) {
				facing = 1;
			} else {
				facing = 2;
			}

			Vector2 VeloX = rigidbody2D.velocity;
			VeloX.x = input;
			rigidbody2D.velocity = VeloX;

			if (rigidbody2D.velocity.magnitude > speed) {
				rigidbody2D.velocity *= speed / rigidbody2D.velocity.magnitude;
			}
				
		}
		else if (Input.GetButton("Vertical")) {
			rigidbody2D.angularVelocity = 0;
//			rigidbody2D.AddForce(input * speed * gameObject.transform.up);

			//input = Input.GetAxis("Vertical");
			input = Input.GetAxisRaw("Vertical") * speed;
			if (input > 0 ) {
				facing = 3;
			} else {
				facing = 4;
			}
			
			Vector2 VeloY = rigidbody2D.velocity;
			VeloY.y = input;
			rigidbody2D.velocity = VeloY;
			
			if (rigidbody2D.velocity.magnitude > speed) {
				rigidbody2D.velocity *= speed / rigidbody2D.velocity.magnitude;
			}
			
		} else {
			//if no direction is pressed stop player
			rigidbody2D.velocity = Vector2.zero;
		}
		
		if (Input.GetButton("Jump") && AttackTimer <= 0) {
			//fire weapon in current direction
			AttackTimer = AttackCD;
			Vector3 pos = Vector3.one;
			Vector2 projDir = Vector2.zero;
			switch (facing) {
				case 1:
					pos = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
					projDir = new Vector2(1,0);
					break;
				case 2:
					pos = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
					projDir = new Vector2(-1,0);
					break;
				case 3:
					pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
					projDir = new Vector2(0,1);
					break;
				case 4:
					pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
					projDir = new Vector2(0,-1);
					break;
			}
//			Vector3 pos = new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y, gameObject.transform.position.z);
			if (multiShot) {
				for (int a=-1;a<2;++a){
					if (facing == 1 || facing == 2) {
						projDir.y = a;
						projectileMove2d clone = (projectileMove2d)Instantiate(projectile, pos, Quaternion.identity);
						clone.moveDir = projDir * projSpeed;
						clone.direction = facing;
					}
					if (facing == 3 || facing == 4) {
						projDir.x = a;
						projectileMove2d clone = (projectileMove2d)Instantiate(projectile, pos, Quaternion.identity);
						clone.moveDir = projDir * projSpeed;
						clone.direction = facing;
					}
				}
			} else {
				projectileMove2d clone = (projectileMove2d)Instantiate(projectile, pos, Quaternion.identity);
				clone.moveDir = projDir * projSpeed;
				clone.direction = facing;
			}
		
		}
		
		if (Input.GetButton("Fire1") && AttackTimer <= 0) {
			Teleport();
			AttackTimer = AttackCD;
		}
		
		if (health <= 0){
			print("YOU ARE DEAD");
			health += 10;
			//failure state here
		}
		
	}
	
	void Teleport(){
		//TODO
		float distance = 3.0f;
		Vector2 TeleportLoc = Vector2.one;
		Vector2 sizeBox = new Vector2(0.5f, 0.5f);
		// check if distance is valid
		// decrease distance until valid spot found 
		while (distance > 0.0f){
			switch (facing) {
				case 1:
					TeleportLoc = new Vector2(transform.position.x + distance, transform.position.y);
					break;
				case 2:
					TeleportLoc = new Vector2(transform.position.x - distance, transform.position.y);
					break;
				case 3:
					TeleportLoc = new Vector2(transform.position.x, transform.position.y + distance);
					break;
				case 4:
					TeleportLoc = new Vector2(transform.position.x, transform.position.y - distance);
					break;
			}
			Vector3 vec3 = new Vector3(TeleportLoc.x, TeleportLoc.y, 0);
			
			Collider2D col = Physics2D.OverlapBox(TeleportLoc, sizeBox, 0);
			if (col == null){
				gameObject.transform.position = vec3;
				print("Distance: " + distance);
				break;
			} else {
				ContactFilter2D filt = new ContactFilter2D();
				Collider2D[] res;
				res = new Collider2D[5];
				int a = col.OverlapCollider(filt, res);
				//if (col.tag){
				print("Num of Collision: " + a);
				distance -= 0.3f;
			}
		}
	}
	
	void ApplyDamage(float damage){
		//take damage
		health = health - damage;
		print("Current Health: " + health);
	}

	void PowerUp(int status){
		// Check what powerUp was picked up
		switch (status){
			case 1:
				multiShot = true;
				break;
		}
	}

	void FinalCollisionCheck(Rigidbody2D body){
		Vector2 moveDir = new Vector2(body.velocity.x, body.velocity.y);
		var bottomRight = new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.max.y);
		var topLeft = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y);

		bottomRight += moveDir;
		topLeft += moveDir;

		if (Physics2D.OverlapArea(topLeft, bottomRight, 11))
		{
			body.velocity = new Vector2(0, 0);
			print("stopped");
			print(topLeft);
			print(bottomRight);
		}
	}
}
