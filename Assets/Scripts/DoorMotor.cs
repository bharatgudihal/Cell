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
    public bool canUse = false;

    public float doorSpeed=500f;
	
	
    public void Update()
    {

        
        if(Input.GetKeyDown(KeyCode.Q)&&canUse)
        {
            activate = true;
            
        }
        if (!doorOpen && activate)
        {
            rDoor.transform.position = Vector3.Lerp(rDoor.transform.position, rDoorOpen.transform.position, doorSpeed * Time.deltaTime);
            lDoor.transform.position = Vector3.Lerp(lDoor.transform.position, lDoorOpen.transform.position, doorSpeed * Time.deltaTime);
            if (lDoor.transform.position == lDoorOpen.transform.position)
            {
                
                activate = false;
                doorOpen = true;
            }      
        }
        if(doorOpen && activate)
        {
            
            rDoor.transform.position = Vector3.Lerp(rDoor.transform.position, rDoorClose.transform.position, doorSpeed * Time.deltaTime);
            lDoor.transform.position = Vector3.Lerp(lDoor.transform.position, lDoorClose.transform.position, doorSpeed * Time.deltaTime);
            if (lDoor.transform.position == lDoorClose.transform.position)
            {
                
                activate = false;
                doorOpen = false;
            }

        }

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
