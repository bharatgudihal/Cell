using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway2Script : MonoBehaviour {

	[SerializeField]
	Light[] lights;

	[SerializeField]
	GameObject[] killBoxes;

	int count;
	bool waiting;
	bool reverse;

	// Use this for initialization
	void Start () {
		count = 0;
		reverse = false;
		waiting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!waiting) {
			switch (count) {
			case 0:
				lights [0].gameObject.SetActive (true);
				lights [1].gameObject.SetActive (false);
				lights [2].gameObject.SetActive (false);
				lights [3].gameObject.SetActive (false);
				lights [4].gameObject.SetActive (false);
				killBoxes[0].SetActive(false);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(true);
				StartCoroutine (Sleep ());
				break;
			case 1:
				lights [0].gameObject.SetActive (false);
				lights [1].gameObject.SetActive (true);
				lights [2].gameObject.SetActive (false);
				lights [3].gameObject.SetActive (false);
				lights [4].gameObject.SetActive (false);
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(false);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(true);
				StartCoroutine (Sleep ());
				break;
			case 2:
				lights [0].gameObject.SetActive (false);
				lights [1].gameObject.SetActive (false);
				lights [2].gameObject.SetActive (true);
				lights [3].gameObject.SetActive (false);
				lights [4].gameObject.SetActive (false);
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(false);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(true);
				StartCoroutine (Sleep ());
				break;
			case 3:
				lights [0].gameObject.SetActive (false);
				lights [1].gameObject.SetActive (false);
				lights [2].gameObject.SetActive (false);
				lights [3].gameObject.SetActive (true);
				lights [4].gameObject.SetActive (false);
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(false);
				killBoxes[4].SetActive(true);
				StartCoroutine (Sleep ());
				break;
			case 4:
				lights [0].gameObject.SetActive (false);
				lights [1].gameObject.SetActive (false);
				lights [2].gameObject.SetActive (false);
				lights [3].gameObject.SetActive (false);
				lights [4].gameObject.SetActive (true);
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(false);
				StartCoroutine (Sleep ());
				break;
			default:
				StartCoroutine (Sleep ());
				break;
			}
		}
	}

	IEnumerator Sleep(){
		waiting = true;
		yield return new WaitForSeconds (1.5f);
		if (!reverse) {
			count++;
			if (count == 4) {
				reverse = true;
			}
		} else {
			count--;
			if (count == 0) {
				reverse = false;
			}
		}
		waiting = false;
	}
}
