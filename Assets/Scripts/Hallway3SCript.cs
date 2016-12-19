using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway3SCript : MonoBehaviour {

	[System.Serializable]
	struct Pattern{
		public int[] elements;
	}

	[SerializeField]
	Pattern[] pattern;

	[SerializeField]
	Light[] lights;

	[SerializeField]
	GameObject[] killBoxes;

	[SerializeField]
	AudioClip clip;

	int count;
	bool reverse;
	bool wait;

	// Use this for initialization
	void Start () {
		count = -1;
		reverse = false;
		wait = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!wait) {
			StartCoroutine (Sleep ());
		}
	}

	IEnumerator Sleep(){
		wait = true;
		yield return new WaitForSeconds (4f);
		if (!reverse) {
			count++;
			if (count == pattern.Length - 1) {
				reverse = true;
			}
		} else {
			count--;
			if (count == 0) {
				reverse = false;
			}
		}
		foreach (Light light in lights) {
			light.enabled = false;
		}
		foreach (GameObject killbox in killBoxes) {
			killbox.SetActive (true);
		}
		foreach (int i in pattern[count].elements) {
			lights [i-1].enabled = true;
			lights [i - 1].GetComponent<AudioSource> ().PlayOneShot (clip);
			killBoxes [i - 1].SetActive (false);
		}
		wait = false;
	}
}
