using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

	bool start = false;

	PlayerController player;

	static GameObject[] lights = new GameObject[5];

	Animator bodyAnimator;

	void Awake(){
		GameManager.startTutorial += StartTutorial;
	}

	void Destroy(){
		GameManager.startTutorial -= StartTutorial;
	}

	// Use this for initialization
	void Start () {		
		bodyAnimator = GetComponent<Animator> ();
		lights [0] = GameObject.Find ("TutSpotlight (13)").gameObject;
		lights [1] = GameObject.Find ("TutSpotlight (12)").gameObject;
		lights [2] = GameObject.Find ("TutSpotlight (11)").gameObject;
		lights [3] = GameObject.Find ("TutSpotlight (10)").gameObject;
		lights [4] = GameObject.Find ("TutSpotlight (9)").gameObject;
		player = GameObject.Find ("CharacterController").GetComponent<PlayerController> ();
		foreach (GameObject go in lights) {
			go.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			
		}
	}

	void StartTutorial(){
		start = true;
		player.stop = true;
		lights [0].SetActive (true);
		StartCoroutine (TutorialSteps());
	}

	IEnumerator TutorialSteps(){		
		yield return new WaitForSeconds (2f);
		print ("Animation played");
		lights [0].SetActive (false);
		yield return new WaitForSeconds (2f);
		foreach (GameObject light in lights) {
			light.SetActive (true);
		}
		player.stop = false;
	}
}
