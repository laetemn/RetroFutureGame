﻿using UnityEngine;
using System.Collections;

public class Phase2 : MonoBehaviour {
	public float knockForce;
	float moveSpeed = 5;
	public int health;
	public Sprite normal, kickPunching;
	public bool stunned, blued, walksRight = true;
	Rigidbody2D rb2D;
	public int deltaX, damage;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		InvokeRepeating ("KickPunching", 3f, 5f);
		ResetDeltaX ();
	}

	void Update() {
		if(Mathf.Abs(deltaX - transform.position.x) >= 20) {
			if(Random.Range(0,2) == 0) {
				walksRight = true;
				ResetDeltaX ();
			}
			else {
				walksRight = false;
				ResetDeltaX ();
			}
		}
	}

	void FixedUpdate() {
		if (walksRight) {
			rb2D.velocity = new Vector2 (moveSpeed, rb2D.velocity.y);
		} else {
			rb2D.velocity = new Vector2 (-1 * moveSpeed, rb2D.velocity.y);
		}
	}

	public void ResetDeltaX() {
		deltaX = (int) transform.position.x;
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch (col.gameObject.tag) {
		case "Char":
			if (!stunned) {
				KickPunching ();
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (5, gameObject, 5f);
				//Should also knock the player away
				walksRight = false;
				ResetDeltaX ();
			}
			break;

		case "PickupableItem":
			if (col.gameObject.GetComponent<PickUpableItem> ().GetItemType () == "Rock") {
				//Maybe play grunt
				Debug.Log ("Threw rock at boss!");
				if (Random.Range (0, 2) == 0) 
					Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
				else
					Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
				Destroy (col.gameObject);
				KickPunching ();
			}
			break;

		case "Wall":
			walksRight = true;
			ResetDeltaX ();
			break;

		case "SecretTrigger":
			walksRight = false;
			ResetDeltaX ();
			break;
		}
	}

	public void Stunned(float time) {
		Debug.Log ("Boss is stunned!");
		//Stuns the boss a few seconds and makes it drop a few pick-Ups
		stunned = true;
		for (int i = 0; i < 3; i++) {
			Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
			Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
		}
		Invoke ("UnStunned", time);
	}

	void UnStunned() {
		Debug.Log ("Boss is UnStunned");
		stunned = false;
	}

	void Charge() {
		//FinalBoss is charging, play relevant things
		Debug.Log("FinalBoss is charging");
		moveSpeed += 5;
		Invoke ("StopCharge", 1f);
	}

	void StopCharge() {
		Debug.Log ("FinalBoss stopped rushing");
		//FinalBoss stops charging, play relevant things
		damage -= 2;
		moveSpeed -= 5;
	}

	void KickPunching() {
		if (!stunned) {
			GetComponent<SpriteRenderer> ().sprite = kickPunching;
			this.gameObject.transform.GetChild (1).gameObject.SetActive (true);
			Invoke ("FinishKickPunching", 1f);
		}
	}

	void FinishKickPunching() {
		GetComponent<SpriteRenderer> ().sprite = normal;
		this.gameObject.transform.GetChild (1).gameObject.SetActive (false);	
	}

	public void GetHurt(int damage) {
		health -= damage;
		if (health <= 0) {
			Defeated ();
		}
	}

	void Defeated() {
		//Now phase 3 starts
		for (int i = 0; i < 5; i++) {
			Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
			Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
		}
	}
}