﻿using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {
	Rigidbody2D rigidBody2D;
	PlayerStatus status;
	InputManager input;

	// Use this for initialization
	void Start() {
		rigidBody2D = GetComponent<Rigidbody2D>();
		status = GetComponent<PlayerStatus>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void FixedUpdate() {
		//This could later be changed to so that when an upgrade is obtained, this part of the script is enabled for the player.
		if(status.InAir() && !status.isSmall && rigidBody2D.velocity.y <= -1f && input.GetKey("float")) {
			rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, -1f);
			status.isFloating = true;
		}
		else if(status.isFloating)
			status.isFloating = false;
	}
}