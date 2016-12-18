using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

	[SerializeField]
	float killRate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider collider){
		print ("TriggerStay " + collider);
		if(collider.gameObject.CompareTag("Player")){
			collider.gameObject.GetComponent<PlayerManager> ().health -= killRate;
		}
	}
}
