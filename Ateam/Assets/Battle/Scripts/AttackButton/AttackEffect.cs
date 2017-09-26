using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour {

    float addScale;
    Image image;

    bool isOut = false;
    bool isIn = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void EffectOut(int level)
    {
        transform.localScale = new Vector3(1f,1f,1f);
        addScale = level*level*0.02f;
        image.color = new Color(1f,1f,1f,0.3f);
        isOut = true;
        isIn = false;
    }


    public void EffectIn(int level)
    {
        transform.localPosition = new Vector3(Random.Range(-300f,300f), transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(level*level, level*level, 1f);
        image.color = new Color(1f, 1f, 1f, 0f);
        isOut = false;
        isIn = true;
    }

    // Update is called once per frame
    void Update () {
        addScale /= 1.0075f;
        if (isOut)
        {
            transform.localScale = new Vector3(transform.localScale.x + addScale, transform.localScale.y + addScale, 1f);
            image.color = new Color(1f, 1f, 1f, Mathf.Max(image.color.a - Time.deltaTime/2f, 0f));
        }
        if (isIn)
        {
            transform.localScale = new Vector3(transform.localScale.x*0.7f, transform.localScale.y*0.7f, 1f);
            image.color = new Color(1f, 1f, 1f, Mathf.Min(image.color.a + Time.deltaTime * 15f, 1f));
            if (transform.localScale.x < 0.01f)
            {
                EffectOut(3);
            }
        }
	}
}
