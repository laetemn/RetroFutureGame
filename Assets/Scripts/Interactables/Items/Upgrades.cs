﻿using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string upgradeType;
	public GameObject upgradeLocation;
	[Header("Tutorial")]
	public GameObject manager;
	public float timeToDisplay;
	public bool stringIsHideKey;
	public string tutorial;
	[TextArea]
	public string tutorialText;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Player") {
			//Play correct music and animation depending on what switch-option is chosen
			switch(upgradeType) {

				case "Float":
					col.gameObject.GetComponent<Float>().enabled = true;
					break;

				case "SecondJump":
					col.gameObject.GetComponent<PlayerJump>().gotSecondJump = true;
					break;

				case "WallJump":
					col.gameObject.GetComponent<WallJump>().enabled = true;
					break;

				case "Stomp":
					col.gameObject.GetComponent<Stomp>().enabled = true;
					break;

				case "Laser":
					upgradeLocation.SetActive(true);
					break;

				case "Health":
					col.gameObject.GetComponent<PlayerHealth>().IncreaseMaxHealth();
					col.gameObject.GetComponent<PlayerInventory>().collectItem();
					break;

				case "Speed":
					col.gameObject.GetComponent<PlayerMovement>().moveSpeed++;
					col.gameObject.GetComponent<PlayerInventory>().collectItem();
					break;

				case "Energy":
					col.gameObject.GetComponent<PlayerEnergy>().IncreaseMaxEnergy();
					col.gameObject.GetComponent<PlayerInventory>().collectItem();
					break;

				case "MegaPunch":
					upgradeLocation.GetComponent<PlayerPunch>().megaAquired = true;
					break;
			}
			if(GetComponent<AudioPlayer>().audioClips.Length > 0)
				GetComponent<AudioPlayer>().PlayDetached(0, 1, 1, 1);
			manager.GetComponent<TutorialManager>().DisplayTutorial(tutorial, tutorialText, stringIsHideKey, timeToDisplay); ;
			Destroy(gameObject);
		}
	}
}