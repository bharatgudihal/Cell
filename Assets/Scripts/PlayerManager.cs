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
		batteryCount = 0;
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (battery != null && Input.GetKey ("joystick button 0")) {
			battery.SetActive (false);
			AddBattery ();
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
			
		}
	}

	void AddBattery(){
		UIBatteries [batteryCount].SetActive (true);
		batteryCount++;
		if (batteryCount > UIBatteries.Length-1) {
			batteryCount = UIBatteries.Length - 1;
		}
		battery = null;
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
