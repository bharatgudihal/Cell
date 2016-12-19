using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ReStartScript : MonoBehaviour {

    public GameObject restartPanel;    
    public EventSystem es;
    private GameObject storeSelected;
	string selectColorText = "#FF0000FF";
	int selectedButton = 0;
	Color selectColor;

	[SerializeField]
	GameObject[] buttons;

    [SerializeField]
    Text text;

	public bool active;

    // Use this for initialization
    void Start () {        
        storeSelected = es.firstSelectedGameObject;
		ColorUtility.TryParseHtmlString (selectColorText,out selectColor);
    }
	
	// Update is called once per frame
	void Update () {
		if (active) {
			if (Input.GetAxis ("Horizontal") < 0) {
				if (selectedButton == 1) {
					buttons [selectedButton].GetComponent<Text> ().color = Color.black;
					selectedButton = 0;

					buttons [selectedButton].GetComponent<Text> ().color = selectColor;
				}
			} else if (Input.GetAxis ("Horizontal") > 0) {
				if (selectedButton == 0) {
					buttons [selectedButton].GetComponent<Text> ().color = Color.black;
					selectedButton = 1;
					buttons [selectedButton].GetComponent<Text> ().color = selectColor;
				}
			}
			if (Input.GetKeyDown ("joystick button 0")) {
				if (selectedButton == 0) {
					ReStartLevel ();
                    gameObject.SetActive(false);
				} else {
					QuitToMain ();
                    gameObject.SetActive(false);
                }
			}
		}
	}
    public void ReStartLevel()
    {
		active = false;
		gameObject.SetActive (false);
        SceneManager.LoadScene(1);//load the current scene
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene(0);//load the start menu
    }

    public void SetText(string message)
    {
        text.text = message;
    }
}
