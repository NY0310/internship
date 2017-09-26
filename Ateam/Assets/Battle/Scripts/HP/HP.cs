using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {
    float max;
    float lowerHp; // 緑
    float upperHp; // 赤
    float hp;

    public GameObject Bar;
    public GameObject RedBar;
    public DamageView TryangleDamage;
    public DamageView CrossDamage;
    public DamageView CircleDamage;
    public DamageView AllDamage;

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

    public float Damaged(float power, tDrop.Type type, Vector3 pos, float scale)
    {
        upperHp = hp;
        hp -= power;
        hp = Mathf.Clamp(hp,0f,max);
        lowerHp = hp;

        switch (type)
        {
            case tDrop.Type.Circle:
                CircleDamage.View(power.ToString("#######"),scale);
                CircleDamage.transform.position = pos;
                break;
            case tDrop.Type.Cross:
                CrossDamage.View(power.ToString("#######"),scale);
                CrossDamage.transform.position = pos;
                break;
            case tDrop.Type.Tryangle:
                TryangleDamage.View(power.ToString("#######"),scale);
                TryangleDamage.transform.position = pos;
                break;
            case tDrop.Type.All:
                AllDamage.View(power.ToString("#######"),scale*1.5f);
                AllDamage.transform.position = pos;
                break;
        }

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
        upperHp += (hp - upperHp) * 0.035f;
        Bar.transform.localScale = new Vector3(lowerHp/max, 1f, 1f);
        RedBar.transform.localScale = new Vector3(upperHp/max, 1f, 1f);
    }

}
