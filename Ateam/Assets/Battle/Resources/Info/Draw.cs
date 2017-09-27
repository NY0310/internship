using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour {

    Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.color = new Color(0f,0f,0f,0f);
	}

    public void Clicked()
    {
        image.color = new Color(1f, 1f, 1f, 1f);
    }

    public void Removed()
    {
        image.color = new Color(0f, 0f, 0f, 0f);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
