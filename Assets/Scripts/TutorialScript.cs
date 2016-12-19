using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

	bool start = false;

	PlayerController player;

	static GameObject[] lights = new GameObject[5];

	[SerializeField]
	AudioClip[] clips;

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
		lights [0] = GameObject.Find ("TutSpotlight (13)");
		lights [1] = GameObject.Find ("TutSpotlight (12)");
		lights [2] = GameObject.Find ("TutSpotlight (11)");
		lights [3] = GameObject.Find ("TutSpotlight (10)");
		lights [4] = GameObject.Find ("TutSpotlight (9)");
		player = GameObject.Find ("CharacterController").GetComponent<PlayerController> ();
		bodyAnimator = GameObject.Find ("Torso").GetComponent<Animator> ();
		foreach (GameObject go in lights) {
			go.GetComponent<Light>().enabled = false;
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
		lights [0].GetComponent<Light>().enabled = true;
		lights [0].GetComponent<AudioSource> ().PlayOneShot (clips [0]);
		StartCoroutine (TutorialSteps());
	}

	IEnumerator TutorialSteps(){		
		yield return new WaitForSeconds (2f);
		print ("Animation played");
		lights [0].GetComponent<AudioSource> ().PlayOneShot (clips [1]);
		lights [0].GetComponent<Light>().enabled = false;
		yield return new WaitForSeconds (2f);
		lights [0].GetComponent<AudioSource> ().PlayOneShot (clips [0]);
		foreach (GameObject light in lights) {
			light.GetComponent<Light>().enabled = true;
		}
		player.stop = false;
	}
}
