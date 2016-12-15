using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

	[SerializeField]
	GameObject UI;

	// Use this for initialization
	void Start () {
		UI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Player")) {
			UI.SetActive (true);
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.gameObject.CompareTag ("Player")) {
			UI.SetActive (false);
		}
	}
}
