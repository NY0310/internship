using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtleAnimation : MonoBehaviour {
    [SerializeField]
    GameObject HpAnimation;
    [SerializeField]
    GameObject EffectAnimation;

    //削除するか
    bool IsDestroy;
    public bool _IsDestroy
    {
        get { return IsDestroy; }
        set { IsDestroy = value; }

    }


    //エフェクト
    bool IsHpAnimation;

    // Use this for initialization
    void Start () {
        //
        EffectAnimation = Instantiate(EffectAnimation);
        
    }

    HpBar Hp;
    float NewHp;
    Vector2 StartPosition;
    Vector2 GorlPosition;
    public void Inisialize(HpBar Hp, float NewHp, Vector2 StartPosition, Vector2 GorlPosition)
    {
        this.Hp = Hp;
        this.NewHp = NewHp;
        this.StartPosition = StartPosition;
        this.GorlPosition = GorlPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if ((EffectAnimation == null)&&(!IsHpAnimation))
        {
            HpAnimation = Instantiate(HpAnimation);
            HpAnimation.GetComponent<HpAnimation>().Initialze(Hp, NewHp);
            IsHpAnimation = true;
        }


        if (HpAnimation == null)
        {
            IsDestroy = true;
        }
    }
}


