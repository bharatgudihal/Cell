using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

    public GameObject spotLight;
    bool canUse;
    bool lightOn;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)&&canUse)
        {
            lightOn = !lightOn;
            spotLight.SetActive(lightOn);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
