using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

	[SerializeField]
	GameObject outLine;

	// Use this for initialization
	void Start () {
		outLine.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision){
		if (collision.gameObject.CompareTag ("Player")) {
			outLine.SetActive (true);
		}
	}

	void OnTriggerExit(Collider collision){
		if (collision.gameObject.CompareTag ("Player")) {
			outLine.SetActive (false);
		}
	}
}
