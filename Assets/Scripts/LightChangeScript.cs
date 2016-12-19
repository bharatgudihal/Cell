﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChangeScript : MonoBehaviour {

    public Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
        {
            rend.material.SetColor("_Color", Color.blue);
            rend.material.SetColor("_EmissionColor", Color.blue);

        }
	}
}
