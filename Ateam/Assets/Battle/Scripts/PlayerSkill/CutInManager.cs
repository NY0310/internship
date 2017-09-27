using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutInManager : MonoBehaviour {

    public GameObject CutInImage;
    Image cutInImage;
    Image image;

    float remainingTime=0f;

    Vector3 defaultCutInImagePos;

    float moveSpeed = 0f;

    public void Activate(Sprite sprite, float time)
    {
        moveSpeed = 10f;
        remainingTime = time;
        cutInImage.sprite = sprite;
        image.raycastTarget = true;
        cutInImage.transform.position = defaultCutInImagePos + new Vector3(-74f,-35f,0f);
        cutInImage.color = new Color(1f, 1f, 1f, 0f);
        // cutInImage.rectTransform.localScale = new Vector3(0.5f, 1f, 1f);
    }

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.color = new Color(0f,0f,0f,0f);
        cutInImage = CutInImage.GetComponent<Image>();
        defaultCutInImagePos = cutInImage.transform.position;
	}

    bool cutIning = false;

	// Update is called once per frame
	void Update () {
        if (remainingTime > 0)
        {
            cutInImage.transform.position += new Vector3(0.8f, 0.4f, 0f);
            remainingTime -= Time.deltaTime;
            if(remainingTime > 2.8f)
            {
                image.color += new Color(0f, 0f, 0f, Time.deltaTime * 5f * 0.7f);
            }
            if (remainingTime >= 2.9f)
            {
                moveSpeed *= 0.7f;
                cutInImage.transform.position += new Vector3(moveSpeed, moveSpeed/2f, 0f);
                cutInImage.color = new Color(1f, 1f, 1f, cutInImage.color.a + Time.deltaTime*10f);
            }
            else if(remainingTime <= 2.5f)
            {
                //moveSpeed *= 1.04f;
                //cutInImage.transform.position += new Vector3(moveSpeed, moveSpeed / 2f, 0f);
                //cutInImage.rectTransform.localScale = new Vector3((remainingTime-1.5f)*10f, (1.6f - remainingTime) * 5f + 1f, 1f);
                cutInImage.color = new Color(1f, 1f, 1f, cutInImage.color.a - Time.deltaTime);
                image.color -= new Color(0f, 0f, 0f, Time.deltaTime*0.7f);
            }
            if (remainingTime <= 1.5f && !cutIning)
            {
                cutInImage.color = new Color(1f, 1f, 1f, 0f);
                cutIning = true;
                image.color = new Color(0f, 0f, 0f, 0f);
                cutInImage.color = new Color(1f, 1f, 1f, 0f);
            }
            if (remainingTime <= 0)
            {
                cutIning = false;
                image.raycastTarget = false;
            }
        }
	}
}
