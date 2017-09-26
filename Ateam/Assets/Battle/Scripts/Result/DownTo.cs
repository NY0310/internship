using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownTo : MonoBehaviour {

    RectTransform rect;
    Vector3 defaultPos;

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
        defaultPos = rect.position;
        rect.position = new Vector3(defaultPos.x, 2800f, defaultPos.z);
	}
	
	// Update is called once per frame
	void Update () {
        float newY = rect.position.y + (defaultPos.y - rect.position.y) * Time.deltaTime*5;
        rect.position = new Vector3(defaultPos.x, newY , defaultPos.z);
    }
}
