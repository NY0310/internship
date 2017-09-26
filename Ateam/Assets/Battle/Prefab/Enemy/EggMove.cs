using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggMove : MonoBehaviour {

    Image image;
    float defaultY;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        defaultY = image.transform.localPosition.y;
	}

    float count = 0f;

	// Update is called once per frame
	void Update () {
        count += Time.deltaTime* Random.Range(0.5f,1.5f);
        image.transform.rotation = Quaternion.Euler( new Vector3( 0f, 0f, Mathf.Max( Mathf.Sin(-0.75f+count*5f)-0.9f, 0f)*70f ));
        image.transform.localPosition = new Vector3(image.transform.localPosition.x, defaultY + Mathf.Max(Mathf.Sin(count * 5f) - 0.9f, 0f)*235f, image.transform.localPosition.z);
    }
}
