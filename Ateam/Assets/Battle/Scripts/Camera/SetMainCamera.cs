using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Canvas>().worldCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
	}
}
