using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

	[SerializeField]
	GameObject outLine;

	[SerializeField]
	GameObject door;

	bool batteryPlaced;

	// Use this for initialization
	void Start () {
		outLine.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.CompareTag ("BatteryHolder") && gameObject.GetComponent<MeshRenderer> ().enabled && door != null && !batteryPlaced) {
			batteryPlaced = true;
            door.GetComponent<DoorMotor>().SwitchToActive();
		} else if (gameObject.CompareTag ("BatteryHolder") && !gameObject.GetComponent<MeshRenderer> ().enabled && door != null && batteryPlaced){
			batteryPlaced = false;
			door.GetComponent<DoorMotor> ().SwitchToInActive ();
		}
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
