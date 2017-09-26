using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageView : MonoBehaviour {
    Text text;
    int defaultFontSize;
    void Start()
    {
        text = GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        defaultFontSize = text.fontSize;
    }

    public void View(string str, float scale)
    {
        text.text = str;
        text.fontSize = (int)( defaultFontSize * scale);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a -0.02f);
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);    
    }

}
