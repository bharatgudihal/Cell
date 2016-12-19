using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotor : MonoBehaviour {

    public GameObject lDoor;
    public GameObject rDoor;

    public Transform rDoorOpen;//this is the a local transform for where the door 
    public Transform rDoorClose;

    public Transform lDoorOpen;//local transform for the girl to 
    public Transform lDoorClose;

    public bool doorOpen;
    public bool activate;
    public bool canUse;

	[SerializeField]
	float closeAfter;

	[SerializeField]
	bool isEnabled;

    public float doorSpeed=500f;

	[SerializeField]
	GameObject frontAllow;
	[SerializeField]
	GameObject frontDeny;
	[SerializeField]
	GameObject backAllow;

	Vector3 vel;
	bool hide;

	int batteryCount;

	[SerializeField]
	int numberOfBatteriesRequired;

	[SerializeField]
	AudioClip[] clips;

	AudioSource source;

	void Start(){
		HideAllIcons ();
		batteryCount = 0;
		source = GetComponent<AudioSource> ();
	}

	void HideAllIcons(){
		if (frontAllow != null) {			
			frontAllow.SetActive (false);
		}
		if (frontDeny != null) {
			frontDeny.SetActive (false);
		}
		if (backAllow != null) {
			backAllow.SetActive (false);
		}
	}

	void ShowIcons(){
		if (isEnabled) {
			frontAllow.SetActive (true);
			backAllow.SetActive (true);
		} else {
			frontDeny.SetActive (true);
		}
	}
	
    public void Update()
    {
        if ((Input.GetKey (KeyCode.Q) || Input.GetKeyDown ("joystick button 0")) && canUse && isEnabled) {
			activate = true;
			if (!doorOpen) {
				source.PlayOneShot (clips [0]);
			} else {
				source.PlayOneShot (clips [1]);
			}
		} else if ((Input.GetKey (KeyCode.Q) || Input.GetKeyDown ("joystick button 0")) && canUse && !isEnabled) {
			source.PlayOneShot (clips [2]);
		}
        if (!doorOpen && activate)
        {
			
			HideAllIcons ();
			rDoor.transform.position = Vector3.SmoothDamp(rDoor.transform.position, rDoorOpen.transform.position, ref vel, doorSpeed * Time.deltaTime);
			lDoor.transform.position = Vector3.SmoothDamp(lDoor.transform.position, lDoorOpen.transform.position, ref vel, doorSpeed * Time.deltaTime);
            if (lDoor.transform.position == lDoorOpen.transform.position)
            {
                
                activate = false;
                doorOpen = true;
				StartCoroutine (CloseDoor ());
            }      
        }
        if(doorOpen && activate)
        {			
			rDoor.transform.position = Vector3.SmoothDamp(rDoor.transform.position, rDoorClose.transform.position, ref vel, doorSpeed * Time.deltaTime);
			lDoor.transform.position = Vector3.SmoothDamp(lDoor.transform.position, lDoorClose.transform.position, ref vel, doorSpeed * Time.deltaTime);
            if (lDoor.transform.position == lDoorClose.transform.position)
            {
				if (canUse) {
					ShowIcons ();
				}
				hide = false;               
                activate = false;
                doorOpen = false;
            }

        }

    }

	IEnumerator CloseDoor(){
		yield return new WaitForSeconds (closeAfter);
		activate = true;
		source.PlayOneShot (clips [1]);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canUse = true;
			if (!doorOpen) {
				ShowIcons ();
			} else {
				HideAllIcons ();
			}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canUse = false;
			HideAllIcons ();
        }
    }

	public void SwitchToActive(){
		source.PlayOneShot (clips [3]);
		batteryCount++;
		if (numberOfBatteriesRequired == batteryCount) {
			isEnabled = true;
			if (frontDeny.activeSelf) {
				backAllow.SetActive (true);
				frontAllow.SetActive (true);
				frontDeny.SetActive (false);
			}
		}
	}

	public void SwitchToInActive(){
		source.PlayOneShot (clips [2]);
		batteryCount--;
		isEnabled = false;
		if (frontAllow.activeSelf) {
			backAllow.SetActive (false);
			frontAllow.SetActive (false);
			frontDeny.SetActive (true);
		}
	}

}
