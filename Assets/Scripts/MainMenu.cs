using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour {

    public EventSystem es;
    private GameObject storeSelected;


    public void Start()
    {
        storeSelected = es.firstSelectedGameObject;
    }
    private void Update()
    {
        if(es.currentSelectedGameObject != storeSelected)
        {
            if(es.currentSelectedGameObject == null)
            {
                es.SetSelectedGameObject(storeSelected);
            }
            else
            {
                storeSelected = es.currentSelectedGameObject;
            }

        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
