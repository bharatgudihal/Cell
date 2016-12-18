using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

	bool start = false;

	[SerializeField]
	PlayerController player;

	[SerializeField]
	GameObject[] lights;

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
