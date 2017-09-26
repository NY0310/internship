using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinA : MonoBehaviour {

    Image image;
    public Image image2;

	void Start () {
        image = GetComponent<Image>();
	}

    void SetA(float a)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
    }
    void SetA2(float a)
    {
        image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, a);
    }

    bool tapped = false;
    public void Tapped()
    {
        tapped = true;
    }

    float count = 0f;
    float endCount = 0f;
    float finishCount = 0f;
    const float START_TIME = 4f;
    const float END_TIME = 0.5f;
    const float FINISH_TIME = 0.5f;

	void Update () {
        count += Time.deltaTime;
        float newA=0f;
        float newA2 = 1f;
        if (count <= START_TIME && !tapped)
        {
            newA2 = count / START_TIME;
            newA = newA2 * 0.6f;
        }
        else if (!tapped)
        {
            newA = Mathf.Abs( Mathf.Cos((count - START_TIME)) * 0.4f)+0.2f;
        }
        else if (endCount < END_TIME)
        {
            endCount += Time.deltaTime;
            newA2 = Mathf.Max(1f - endCount / END_TIME, 0f);
            newA = 1f;
            image.rectTransform.localScale = new Vector3(1f+endCount*5f, 1f -  Mathf.Sin(endCount/END_TIME), 1f);
        }
        else
        {
            finishCount += Time.deltaTime;
            newA2 = 0f;
            newA = 1f - finishCount / FINISH_TIME;
        }

        SetA(newA);
        SetA2(newA2);

	}
}
