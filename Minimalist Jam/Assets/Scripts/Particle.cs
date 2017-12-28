using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {
	public int energy;
	private SpriteRenderer sr;
	private ParticleSystem ps1;
	private ParticleSystem ps2;
	private TrailRenderer tr;
	private Rigidbody2D rb;
	public float endingDelay = 1;
	public bool ending = false;
	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer> ();
		ps1 = gameObject.GetComponent<ParticleSystem> ();
		ps2 = gameObject.GetComponentInChildren<ParticleSystem> ();
		tr = gameObject.GetComponent<TrailRenderer> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D d){
		if (d.gameObject.tag == "ParticleDestroyer") {
			Destroy (gameObject);
		}
	}
	public void End(){
		Destroy (gameObject, endingDelay);
		rb.velocity = new Vector2 (0, 0);
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, 0);
		ps1.Stop ();
		ps2.Stop ();
		//tr.enabled = false;
	}
}
