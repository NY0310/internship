﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEST_TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Queue<string> loadList = new Queue<string>();
            loadList.Enqueue("Home");
            loadList.Enqueue("Footer");
            SceneLoader.ChangeScene(SceneLoader.MakeQueue("Home","Footer"), 2.3f, 1.3f);
        }
	}
}
