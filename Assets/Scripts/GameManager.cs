using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public delegate void StartTutorial();
	public static event StartTutorial startTutorial;

	static GameManager instance = null;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (instance);
		}
	}

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void tutorialStarter(){
		startTutorial();
	}
}
