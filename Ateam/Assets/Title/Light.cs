using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Light : MonoBehaviour {

    Image image;

    float scale;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        Init();
	}

    float count = 0f;
    void Init()
    {
        scale = Random.Range(0.01f, 1f);
        transform.localScale = new Vector3(scale,scale,1f);
        transform.localPosition = new Vector3(Random.Range(-500f,500f), Random.Range(-50f, 50f), 1f);
        //transform.localPosition = new Vector3(0f, 0f, 1f);
        count = Random.Range(0f, 100f);
    }

	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
        scale -= Time.deltaTime / 10f;
        transform.position = new Vector3(transform.position.x + Mathf.Sin(count)*scale*2f, transform.position.y + scale*2f, transform.position.z);
        image.color = new Color(image.color.r, image.color.g, image.color.b, scale + Random.Range(0f, 0.7f));
        transform.localScale = new Vector3(scale + Random.Range(0f, 0.25f), scale+Random.Range(0f, 0.25f), 1f);
        if (scale <= 0)
        {
            Init();
        }
	}
}
