using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	GameObject battery;

	[SerializeField]
	GameObject[] UIBatteries;

	[SerializeField]
	Image healthImage;

	int batteryCount;

	public float health;

	[SerializeField]
	float maxHealth;

	[SerializeField]
	float healthRegenRate;

	// Use this for initialization
	void Start () {
		battery = null;
		batteryCount = -1;
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (battery != null && Input.GetKeyDown ("joystick button 0")) {			
			if (battery.CompareTag ("Battery")) {
				battery.SetActive (false);
				AddBattery ();
			}else if(battery.CompareTag ("BatteryHolder")){
				if (battery.GetComponent<MeshRenderer> ().enabled) {
					battery.GetComponent<MeshRenderer> ().enabled = false;
					AddBattery ();
				} else {
					battery.GetComponent<MeshRenderer> ().enabled = true;
					RemoveBattery ();
				}
			}
		}
		if (health > 0f) {
			healthImage.color = new Color(healthImage.color.r,healthImage.color.g,healthImage.color.b,1f - (health / maxHealth));
			if (health < maxHealth) {
				health += healthRegenRate;
				if (health > maxHealth) {
					health = maxHealth;
				}
			}
		} else {
			//TODO:Death screen
		}
	}

	void AddBattery(){
		batteryCount++;
		UIBatteries [batteryCount].SetActive (true);
		if (batteryCount > UIBatteries.Length-1) {
			batteryCount = UIBatteries.Length - 1;
		}
		if (!battery.CompareTag ("BatteryHolder")) {
			battery = null;
		}
	}

	void RemoveBattery(){
		UIBatteries [batteryCount].SetActive (false);
		batteryCount--;
		if (batteryCount < -1) {
			batteryCount = -1;
		}
		if (!battery.CompareTag ("BatteryHolder")) {
			battery = null;
		}
	}

	void OnTriggerEnter(Collider collision){		
		if (collision.gameObject.CompareTag ("Battery")) {
			battery = collision.gameObject;
		} else if (collision.gameObject.CompareTag ("BatteryHolder") && batteryCount >= 0) {
			battery = collision.gameObject;
		}
	}

	void OnTriggerExit(Collider collision){
		battery = null;
	}
}
