﻿using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rigidBody2D;
	public float jumpSpeed;
	public bool jumpDown, hasJumped, holdJump;

	void Start () {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		if (!Input.GetButton ("Jump") && holdJump && (status.onGround || status.onPlatform))
			holdJump = false;
		if (Input.GetButton ("Jump") && Input.GetAxis ("Vertical") < -0.3f && status.onPlatform)
			jumpDown = true;
		else if (Input.GetButton ("Jump") && !holdJump && (status.onGround || status.onPlatform)) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed);
			hasJumped = true;
			holdJump = true;
		}
		else if (!Input.GetButton ("Jump") && hasJumped && rigidBody2D.velocity.y > jumpSpeed / 1.8f)
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed / 1.8f);
	}
}