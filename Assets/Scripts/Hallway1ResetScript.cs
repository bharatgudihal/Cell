using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway1ResetScript : MonoBehaviour {

	[SerializeField]
	Light[] lights;

	[SerializeField]
	GameObject[] killBoxes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.CompareTag ("Player")) {
			foreach (Light light in lights) {
				light.gameObject.SetActive (true);
			}
			foreach (GameObject killbox in killBoxes) {
				killbox.SetActive (false);
			}
		}
	}
}
