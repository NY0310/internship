using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoFooterLoader : MonoBehaviour {

    // Use this for initialization
    void Start() {
        for (int i=0; i<SceneManager.sceneCount;i++ ) {
            if (SceneManager.GetSceneAt(i).name == "Footer")
            {
                return;
            }
        }
        SceneManager.LoadScene("Footer",LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
