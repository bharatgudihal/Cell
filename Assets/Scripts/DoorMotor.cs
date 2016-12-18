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
	bool enabled;

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

	void Start(){
		HideAllIcons ();
		batteryCount = 0;
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
		if (enabled) {
			frontAllow.SetActive (true);
			backAllow.SetActive (true);
		} else {
			frontDeny.SetActive (true);
		}
	}
	
    public void Update()
    {

        
		if((Input.GetKey(KeyCode.Q)||Input.GetKey("joystick button 0")) && canUse && enabled)
        {
            activate = true;
            
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
		yield return new WaitForSeconds (3f);
		activate = true;
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

	/*void OnTriggerStay(Collider collider){
		if (doorOpen) {
			ShowIcons ();
		}
	}*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canUse = false;
			HideAllIcons ();
        }
    }

	public void SwitchToActive(){
		batteryCount++;
		if (numberOfBatteriesRequired == batteryCount) {
			enabled = true;
			print (frontDeny.activeSelf);
			if (frontDeny.activeSelf) {
				backAllow.SetActive (true);
				frontAllow.SetActive (true);
				frontDeny.SetActive (false);
			}
		}
	}

	public void SwitchToInActive(){
		batteryCount--;
		enabled = false;
		if (frontAllow.activeSelf) {
			backAllow.SetActive (false);
			frontAllow.SetActive (false);
			frontDeny.SetActive (true);
		}
	}

}
