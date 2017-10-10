using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵クラス
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    //[SerializeField]
    //public GameObject enemy;
    [SerializeField]
    GameObject HpPrefab;
    public  GameObject _HpPrefab
    {
        get { return HpPrefab; }
        set { HpPrefab = value; }

    }
    //HPゲージとの縦間隔
    [SerializeField]
    public float INTERVAL_POS;

    //ドロップ属性
    protected Drop.DROPTYPE DropType;
    //ドロップ属性のプロパティ
    public Drop.DROPTYPE _DropType
    {
        get { return DropType; }
        set { DropType = value; }
    }
    //体力
    [SerializeField]
    protected int HP;
    public int _HP
    {
        get { return HP; }
        set { HP = value; }
    }
    //最大体力
    protected int MAXHP;
    //攻撃
    [SerializeField]
    protected int Attack;
    public int _Attack
    {
        get { return Attack; }
        set { Attack = value; }

    }

    //GameObject ButtlegameObject;
    bool IsDestory = false;
    public bool _IsDestory
    {
        get { return IsDestory; }
        set { IsDestory = value; }

    }




    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// HPからダメージをうける
    /// </summary>
    /// <param name="damage"></param>
    public abstract float HitDamage(int damage, Drop.DROPTYPE droptype);


    /// <summary>
    /// HPバー生成、座標調整
    /// </summary>
    protected  void HpBatInitialize()
    {
        //hpゲージ作成
        HpPrefab = Instantiate(HpPrefab);
        Vector3 pos = this.transform.position;
        pos.y += INTERVAL_POS;
        pos *= 25;

        HpPrefab.GetComponentInChildren<Slider>().GetComponent<RectTransform>().anchoredPosition = new Vector3(pos.x, pos.y + INTERVAL_POS, pos.z);
        HpPrefab.GetComponentInChildren<Slider>().GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.5f, 0.5f);



        //自身を登録しHPbarが0二なったら削除してもらう
        HpPrefab.GetComponent<HpBar>()._Enemy = this.gameObject;
    }

    protected float SetHp()
    {
        if (HP > 0)
        {
            HpPrefab.GetComponent<HpBar>()._Hp = (float)HP / (float)MAXHP;

        }
        else
        {
            HpPrefab.GetComponent<HpBar>()._Hp = 0.0f;

        }
        return HpPrefab.GetComponent<HpBar>()._Hp;
    }






}


public abstract class CircleEnemy : Enemy
{
    // Use this for initialization
    public abstract void Start();

    public override float HitDamage(int attack, Drop.DROPTYPE droptype)
    {
        if (droptype == Drop.DROPTYPE.Circle)
        {
            _HP -= attack;
        }
        else if (droptype == Drop.DROPTYPE.Cross)
        {

            _HP -= (int)((float)attack * 1.5f);
        }
        else
        {
            _HP -= (int)((float)attack * 0.5f);
        }
        if (_HP < 0)
        {
            _HP = 0;
        }
        return SetHp();
    }

}

public abstract class CrossEnemy : Enemy
{
    // Use this for initialization
    public abstract void Start();


    public override float HitDamage(int attack, Drop.DROPTYPE droptype)
    {
        if (droptype == Drop.DROPTYPE.Cross)
        {
            _HP -= attack;
        }
        else if (droptype == Drop.DROPTYPE.Tryangle)
        {

            _HP -= (int)((float)attack * 1.5f);
        }
        else
        {
            _HP -= (int)((float)attack * 0.5f);
        }
        if (_HP < 0)
        {
            _HP = 0;
        }
        SetHp();
        return _HP;

    }

}


public abstract class TryangleEnemy: Enemy
{
    // Use this for initialization
    public abstract void Start();

    public override float HitDamage(int attack, Drop.DROPTYPE droptype)
    {
        if (droptype == Drop.DROPTYPE.Tryangle)
        {
            _HP -= attack;
        }
        else if (droptype == Drop.DROPTYPE.Circle)
        {

           _HP -= (int)((float)attack * 1.5f);
        }
        else
        {
            _HP -= (int)((float)attack * 0.5f);
        }
        if (_HP < 0)
        {
            _HP = 0;
        }
        SetHp();
        return _HP;

    }


}