using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ReStartScript : MonoBehaviour {

    public GameObject restartPanel;
    bool panelActive;
    public EventSystem es;
    private GameObject storeSelected;

    // Use this for initialization
    void Start () {
        restartPanel.SetActive(panelActive);
        storeSelected = es.firstSelectedGameObject;
    }
	
	// Update is called once per frame
	void Update () {
	    
        restartPanel.SetActive(panelActive);//show the panel only when panel active is true
        
        if (panelActive)// if true make sure the selected button is correct
        {
            if (es.currentSelectedGameObject != storeSelected)
            {
                if (es.currentSelectedGameObject == null)//if its empty 
                {
                    es.SetSelectedGameObject(storeSelected);//assign the original button
                }
                else
                {
                    storeSelected = es.currentSelectedGameObject;// set the current button
                }

            }
        }
    }
    public void ReStartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//load the current scene
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenu");//load the start menu
    }
    public void RestartPanel(bool foo)
    {
        panelActive = foo;//turn off an on the RestartPanel
    }
}
