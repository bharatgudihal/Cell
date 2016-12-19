using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway2Script : MonoBehaviour {

	[SerializeField]
	Light[] lights;

	[SerializeField]
	GameObject[] killBoxes;

	[SerializeField]
	AudioClip[] clips;

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
				if (lights [4].enabled) {
					lights [4].GetComponent<AudioSource> ().PlayOneShot (clips [1]);
				}
				lights [0].enabled = true;
				lights [1].enabled = false;
				lights [2].enabled = false;
				lights [3].enabled = false;
				lights [4].enabled = false;
				killBoxes[0].SetActive(false);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(true);
				if (lights [0].enabled)
				lights [0].GetComponent<AudioSource> ().PlayOneShot (clips [0]);
				StartCoroutine (Sleep ());
				break;
			case 1:
				if (lights [0].enabled)
				lights [0].GetComponent<AudioSource> ().PlayOneShot (clips [1]);
				lights [0].enabled = false;
				lights [1].enabled = true;
				lights [2].enabled = false;
				lights [3].enabled = false;
				lights [4].enabled = false;
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(false);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(true);
				if (lights [1].enabled)
				lights [1].GetComponent<AudioSource> ().PlayOneShot (clips [0]);
				StartCoroutine (Sleep ());
				break;
			case 2:
				if (lights [1].enabled)
				lights [1].GetComponent<AudioSource> ().PlayOneShot (clips [1]);
				lights [0].enabled = false;
				lights [1].enabled = false;
				lights [2].enabled = true;
				lights [3].enabled = false;
				lights [4].enabled = false;
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(false);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(true);
				if (lights [2].enabled)
				lights [2].GetComponent<AudioSource> ().PlayOneShot (clips [0]);
				StartCoroutine (Sleep ());
				break;
			case 3:
				if (lights [2].enabled)
				lights [2].GetComponent<AudioSource> ().PlayOneShot (clips [1]);
				lights [0].enabled = false;
				lights [1].enabled = false;
				lights [2].enabled = false;
				lights [3].enabled = true;
				lights [4].enabled = false;
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(false);
				killBoxes[4].SetActive(true);
				if (lights [3].enabled)
				lights [3].GetComponent<AudioSource> ().PlayOneShot (clips [0]);
				StartCoroutine (Sleep ());
				break;
			case 4:
				if (lights [3].enabled)
				lights [3].GetComponent<AudioSource> ().PlayOneShot (clips [1]);
				lights [0].enabled = false;
				lights [1].enabled = false;
				lights [2].enabled = false;
				lights [3].enabled = false;
				lights [4].enabled = true;
				killBoxes[0].SetActive(true);
				killBoxes[1].SetActive(true);
				killBoxes[2].SetActive(true);
				killBoxes[3].SetActive(true);
				killBoxes[4].SetActive(false);
				if (lights [4].enabled)
				lights [4].GetComponent<AudioSource> ().PlayOneShot (clips [0]);
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
