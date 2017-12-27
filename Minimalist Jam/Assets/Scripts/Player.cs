using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;
	public float speed = 6;
	public bool grounded = false;
	public float jumpSpeed = 10;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (grounded) {
			float amnt = Input.GetAxis ("Horizontal");
			rb.velocity = new Vector2 (amnt * speed, rb.velocity.y);
			if (Input.GetButton ("Jump")) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
			}
		}

	}
	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "ground") {
			grounded = true;
		}
	}
	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "ground") {
			grounded = false;
		}
	}
}
