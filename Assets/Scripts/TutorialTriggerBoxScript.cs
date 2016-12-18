using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerBoxScript : MonoBehaviour {

	[SerializeField]
	Light spotLight;

	[SerializeField]
	GameObject killBox;

	static bool done;

	// Use this for initialization
	void Start () {
		done = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.CompareTag("Player")){
			if (!done) {
				done = true;
				GameManager.tutorialStarter ();
			}
		}
	}

	void OnTriggerExit(Collider collider){
		spotLight.gameObject.SetActive(false);
		if (killBox != null) {
			killBox.SetActive (true);
		}
	}
}
