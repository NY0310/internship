using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    Slider Slider;
    public Slider _Slider
    {
        get { return Slider; }
        set { Slider = value; }

    }

    HPState HpState;

    float Hp  = 1;
    public float _Hp
    {
        get { return Hp; }
        set { Hp = value; }

    }

    float OldHp;

    public float _OldHp
    {
        get { return OldHp; }
        set { OldHp = value; }
    }

    //float StartTime;
    //public float _StartTime
    //{
    //    get { return StartTime; }
    //    set { StartTime = value; }
    //}


    const int AnimationTime = 2;
    public int _AnimationTime
    {
        get { return AnimationTime; }
    }


    void Start()
    {
        // スライダーを取得する
        Slider = GameObject.Find("Slider").GetComponent<Slider>();
        HpState = HPWait.GetInstance();
        Hp = 1;
        OldHp = Hp;
    }


    void Update()
    {
 
        HpState.Execute(this);
 
    }



    //現在の状態を変更する
    public void ChangeState(HPState hpstate)
    {
        HpState = hpstate;
    }

    //void SetPosition(Vector3 position)
    //{
        
    //}


}



public abstract class HPState
{

    //純粋仮想関数を宣言
    public abstract void Execute(HpBar hpbar);

    //
    // public ~ButtleState();

}


public class Animation : HPState
{
    static HPState buttleState;
    int NowTime = 0;

    //Standクラスのインスタンスを取得する
    public static HPState GetInstance(HpBar hpbar)
    {
        if (buttleState == null)
        {
            buttleState = new Animation();
        }
        return buttleState;
    }

    public override void Execute(HpBar hpbar)
    {
        NowTime++;
        float time = NowTime / 60.0f;
        hpbar._Slider.value =  Larp(hpbar._OldHp, hpbar._Hp, time);
        //線形補間完了
        if (time >= 1)
        {
            hpbar._OldHp = hpbar._Hp;
            hpbar.ChangeState(HPWait.GetInstance());
        }
    }

    float Larp(float start, float goral, float time)
    {
        return (1 - time) * start + goral * time;

    }

}



public class HPWait : HPState
{
    static HPState buttleState;

    //Standクラスのインスタンスを取得する
    public static HPState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new HPWait();
        }

        return buttleState;
    }

    public override void Execute(HpBar hpbar)
    {
        if (hpbar._OldHp != hpbar._Hp)
            hpbar.ChangeState( Animation.GetInstance(hpbar));
    }


}
