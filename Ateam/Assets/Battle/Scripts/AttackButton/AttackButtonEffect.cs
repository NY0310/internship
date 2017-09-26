using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonEffect : MonoBehaviour {

    float addScale;
    Image image;

    public Text text;

    Vector3 defaultTextPos;
    void Start()
    {
        image = GetComponent<Image>();
        defaultTextPos = text.gameObject.transform.position;
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

    bool attack = false;
    public void Attack()
    {
        attack = true;
        text.gameObject.transform.localPosition = new Vector3(0f, Random.Range(800f,600f), 0f);
        attackCount = 0.5f;
    }

    float attackCount = 0f;

	// Update is called once per frame
	void Update () {
        addScale /= 1.05f;
        transform.localScale = new Vector3(transform.localScale.x+addScale, transform.localScale.y+addScale, 1f);
        image.color = new Color(1f,1f,1f, Mathf.Max( image.color.a - Time.deltaTime*1.5f, 0f));

        displayedPower += (power - displayedPower) * 0.1f;
        text.text = displayedPower.ToString("#######");

        if (attack)
        {
            text.gameObject.transform.localPosition = new Vector3(0f, text.gameObject.transform.localPosition.y+1f, 0f);
            text.color = new Color(1f, 1f, 1f, Mathf.Max(text.color.a - 0.01f, 0f));
            attackCount -= Time.deltaTime;
            if (attackCount <= 0)
            {
                Vector3 scale = text.gameObject.transform.localScale;
                text.gameObject.transform.localScale = new Vector3(scale.x + 0.05f, scale.y, scale.z);
                text.color = new Color(1f, 1f, 1f, Mathf.Max(text.color.a - 0.05f, 0f));

                if (text.color.a == 0)
                {
                    text.color = new Color(1f, 1f, 1f, 1f);
                    attack = false;
                    text.text = "";
                    power = 0f;
                    displayedPower = 0f;
                    text.gameObject.transform.localScale = new Vector3(1f, scale.y, scale.z);
                    text.gameObject.transform.position = defaultTextPos;
                }
            }
        }
	}
}
