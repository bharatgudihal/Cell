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

	void Start(){
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
	
    public void Update()
    {

        
		if((Input.GetKey(KeyCode.Q)||Input.GetKey("joystick button 0")) && canUse && enabled)
        {
            activate = true;
            
        }
        if (!doorOpen && activate)
        {			
			if (frontAllow != null) {			
				frontAllow.SetActive (false);
			}
			if (frontDeny != null) {
				frontDeny.SetActive (false);
			}
			if (backAllow != null) {
				backAllow.SetActive (false);
			}
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
			if (enabled) {
				frontAllow.SetActive (true);
				backAllow.SetActive (true);
			} else {
				frontDeny.SetActive (true);
			}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canUse = false;
			if (enabled) {
				frontAllow.SetActive (false);
				backAllow.SetActive (false);
			} else {
				frontDeny.SetActive (false);
			}
        }
    }

	public void SwitchToActive(){
		enabled = true;
		if (frontDeny.activeSelf) {
			backAllow.SetActive (true);
			frontAllow.SetActive (true);
			frontDeny.SetActive (false);
		}
	}

}
