using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonEffect : MonoBehaviour {

    float addScale;
    Image image;

    public Text text;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void Effect(int level)
    {
        transform.localScale = new Vector3(1f,1f,1f);
        addScale = level*level*0.02f;
        image.color = new Color(1f,1f,1f,1f);
    }

    float power;
    float displayedPower;

    public void AddPower(float power)
    {
        this.power += power;
    }

	// Update is called once per frame
	void Update () {
        addScale /= 1.05f;
        transform.localScale = new Vector3(transform.localScale.x+addScale, transform.localScale.y+addScale, 1f);
        image.color = new Color(1f,1f,1f, Mathf.Max( image.color.a - Time.deltaTime*2f, 0f));

        displayedPower += (power - displayedPower) * 0.1f;
        text.text = displayedPower.ToString("#######");
	}
}
