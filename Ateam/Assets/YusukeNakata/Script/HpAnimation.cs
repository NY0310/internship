using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpAnimation : MonoBehaviour
{
    HpBar Hp;
    float NewHp;
    int NowTime;
    // Use this for initialization
    void Start () {
		
	}


    public void Initialze(HpBar HpObject, float NewHp)
    {
        Hp = HpObject;
        this.NewHp = NewHp;
    }
	
	// Update is called once per frame
	void Update () {
        if (Hp == null)
        {
            Destroy(this.gameObject);

        }

        if (Hp._OldHp != NewHp)
        {
            NowTime++;
            float time = NowTime / 60.0f;

            Hp._Slider = Hp.gameObject.GetComponentInChildren<Slider>();
            Hp._Slider.value = Larp(Hp._OldHp, NewHp, time);
            Slider a = Hp._Slider;
            //線形補間完了
            if (time >= 1)
            {
                NowTime = 0;
                Hp._OldHp = NewHp;
                Destroy(this.gameObject);
            }
        }

    }


    float Larp(float start, float goral, float time)
    {
        return (1 - time) * start + goral * time;

    }
}
