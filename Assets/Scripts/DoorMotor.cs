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

	Vector3 vel;
	
    public void Update()
    {

        
		if((Input.GetKey(KeyCode.Q)||Input.GetKey("joystick button 0")) && canUse && enabled)
        {
            activate = true;
            
        }
        if (!doorOpen && activate)
        {
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canUse = false;
        }
    }

}
