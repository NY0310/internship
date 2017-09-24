using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour {
    float max;
    float lowerHp; // 緑
    float upperHp; // 赤
    float hp;

    public GameObject Bar;
    public GameObject RedBar;

    public bool IsDie()
    {
        return hp <= 0;
    }

    public void Init(float max)
    {
        this.max = max;
        hp = max;
        upperHp = hp;
    }

    public float Damaged(float power)
    {
        upperHp = hp;
        hp -= power;
        hp = Mathf.Clamp(hp,0f,max);
        lowerHp = hp;
        return Mathf.Max(power - upperHp, 0f);
    }
    public void Recovery(float power)
    {
        lowerHp = hp;
        hp += power;
        hp = Mathf.Clamp(hp, 0f, max);
        upperHp = hp;
    }

    void Update()
    {
        if (max == 0) return;
        lowerHp += (hp - lowerHp) * 0.05f;
        upperHp += (hp - upperHp) * 0.05f;
        Bar.transform.localScale = new Vector3(lowerHp/max, 1f, 1f);
        RedBar.transform.localScale = new Vector3(upperHp/max, 1f, 1f);
    }

}
