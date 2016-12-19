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

	[SerializeField]
	GameObject restartUI;

	[SerializeField]
	ReStartScript restartScript;

    [SerializeField]
    Light EndLight;

    GameObject mainCamera;
    GameObject endCamera;
    GameObject zoey;

    AudioSource endAudio;

	// Use this for initialization
	void Start () {
		battery = null;
		batteryCount = -1;
		health = maxHealth;
		restartUI.SetActive (false);
		restartScript.active = false;
        endCamera = GameObject.Find("EndCamera");
        mainCamera = GameObject.Find("Main Camera");
        endCamera.SetActive(false);        
        endAudio = GameObject.Find("ExitRoom").GetComponent<AudioSource>();
        foreach (Transform child in transform)
        {
            if(child.gameObject.name == "Zoey")
            {
                zoey = child.gameObject;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {		
		if (battery != null && Input.GetKeyDown ("joystick button 0")) {			
			if (battery.CompareTag ("Battery")) {
				battery.SetActive (false);
				AddBattery ();
			} else if (battery.CompareTag ("BatteryHolder") && (batteryCount > -1 || battery.GetComponent<MeshRenderer>().enabled)) {
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
			healthImage.color = new Color (healthImage.color.r, healthImage.color.g, healthImage.color.b, 1f - (health / maxHealth));
			if (health < maxHealth) {
				health += healthRegenRate;
				if (health > maxHealth) {
					health = maxHealth;
				}
			}
		} else {			
			restartUI.SetActive (true);
			restartScript.active = true;
			healthImage.color = new Color (healthImage.color.r, healthImage.color.g, healthImage.color.b, 0f);
			GetComponent<PlayerController> ().stop = true;
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
		} else if (collision.gameObject.CompareTag ("BatteryHolder") && batteryCount >= -1) {
			battery = collision.gameObject;
		}else if (collision.gameObject.CompareTag("End"))
        {
            StartCoroutine(EndRoutine());
        }
	}

	void OnTriggerExit(Collider collision){
		battery = null;        
    }

    IEnumerator EndRoutine()
    {
        
        GetComponent<PlayerController>().stop = true;
        EndLight.enabled = false;
        yield return new WaitForSeconds(1f);
        mainCamera.SetActive(false);
        endCamera.SetActive(true);
        zoey.SetActive(false);
        endAudio.Play();
        yield return new WaitForSeconds(endAudio.clip.length);
        restartUI.SetActive(true);
        restartScript.active = true;
        restartScript.SetText("You cannot escape the darkness\n Play Again?");
        healthImage.color = new Color(healthImage.color.r, healthImage.color.g, healthImage.color.b, 0f);
        yield return 0;
    }
}
