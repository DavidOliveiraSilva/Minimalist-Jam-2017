using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private TrailRenderer tr;
	public GameObject ripple;
	public float speed = 6;
	public bool grounded = false;
	public float jumpSpeed = 10;
	private bool dashing = false;
	public float dashingTime = 0.5f;
	private float dashElapsedTime = 0;
	public float dashingSpeed = 30;
	public int direction = 1;
	public float gravityScaleD = 5;
	private bool walled = false;
	public int health = 100;
	public int energy = 100;
	public int maxEnergy = 300;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		tr = gameObject.GetComponent<TrailRenderer> ();
		tr.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (dashing) {
			dashElapsedTime -= Time.deltaTime;
			if (dashElapsedTime <= 0) {
				dashing = false;
				tr.enabled = false;
				rb.gravityScale = gravityScaleD;
			}
			return;
		}
		float amnt = Input.GetAxis ("Horizontal");
		if (amnt > 0) {
			direction = 1;
			sr.flipX = false;
		} else if (amnt < 0) {
			direction = -1;
			sr.flipX = true;
		}

		rb.velocity = new Vector2 (amnt * speed, rb.velocity.y);

		if (grounded) {
			if (Input.GetButton ("Jump")) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
			}
		}
		if (Input.GetButton ("Fire1")) {
			dashing = true;
			tr.enabled = true;
			tr.Clear ();
			rb.velocity = new Vector2 (dashingSpeed * direction, 0);
			rb.gravityScale = 0;
			dashElapsedTime = dashingTime;
		}

	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "particle") {
			energy += coll.gameObject.GetComponent<Particle> ().energy;
			if (energy > maxEnergy) {
				health -= coll.gameObject.GetComponent<Particle> ().energy;
				energy = 100;
			}
			GameObject re = Instantiate (ripple);
			re.transform.position = coll.gameObject.transform.position;
			re.GetComponent<ParticleSystem> ().Emit (3);
			Destroy (re.gameObject, 2);
			coll.gameObject.GetComponent<Particle> ().End ();


		}
	}
	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "ground") {
			grounded = true;
		}
		if (coll.gameObject.tag == "wall") {
			walled = true;
		}
	}
	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "ground") {
			grounded = false;
		}
		if (coll.gameObject.tag == "wall") {
			walled = false;
		}
	}
}
