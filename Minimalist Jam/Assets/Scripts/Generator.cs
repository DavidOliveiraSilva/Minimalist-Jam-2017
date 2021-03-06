﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
	public GameObject particle;
	public float interval = 1;
	private float last = 0;
	public float minAngle = 0;
	public float maxAngle = 0;
	public float speed = 10;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - last > interval) {
			GameObject p = Instantiate<GameObject> (particle);
			p.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
			float angle = Random.Range (minAngle, maxAngle);
			p.GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed*Mathf.Cos(angle), speed*Mathf.Sin(angle));
			p.GetComponent<TrailRenderer> ().Clear ();
			last = Time.time;
		}
	}
}
