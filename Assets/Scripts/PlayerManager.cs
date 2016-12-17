using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	GameObject battery;

	[SerializeField]
	GameObject[] UIBatteries;
	int batteryCount;

	// Use this for initialization
	void Start () {
		battery = null;
		batteryCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (battery != null && Input.GetKey ("joystick button 0")) {
			battery.SetActive (false);
			AddBattery ();
		}
	}

	void AddBattery(){
		UIBatteries [batteryCount].SetActive (true);
		batteryCount++;
		if (batteryCount > UIBatteries.Length-1) {
			batteryCount = UIBatteries.Length - 1;
		}
	}

	void OnTriggerEnter(Collider collision){		
		if (collision.gameObject.CompareTag ("Battery")) {
			battery = collision.gameObject;
		}
	}

	void OnTriggerExit(Collider collision){
		battery = null;
	}
}
