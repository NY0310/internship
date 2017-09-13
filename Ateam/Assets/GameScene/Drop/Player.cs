using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 
    //ドロップマネージャ
    GameObject DropManager;
    //バトルマネージャ
    GameObject ButtlegameObject;
    //HPゲージ
    GameObject HpBarPrefab;

    //合計攻撃力
    int ToatalAttack;
    [SerializeField]
    //攻撃力
    public int Attack = 10;
    //攻撃力のプロパティ
    public int _Attack
    {
        get { return ToatalAttack; }
        set { ToatalAttack = value; }
    }

    [SerializeField]
    //体力
    public int HP = 100;
    //HPのプロパティ
    public int _HP
    {
        get { return HP; }
        set { HP = value; }
    }

    //ドロップ属性
    Drop.DROPTYPE DropType;
    //ドロップ属性のプロパティ
    public Drop.DROPTYPE _DropType
    {
        get { return DropType; }
        set { DropType = value; }
    }

    //攻撃タイプの種類
    public enum AttackType
    {
        Attack,
        Recovery
    }

    //攻撃タイプ
    AttackType attackType;
    //攻撃タイプのプロパティ
    public AttackType _AttackType
    {
        get { return attackType; }
        set { attackType = value; }
    }

    //攻撃レベル
    public enum AttackLevel
    {
        LevelOne,
        LevelTwo,
        LevelTree
    }

    /// <summary>
    /// 攻撃(ターゲットドロップの中に)
    /// </summary>
    public void DropJudgment()
    {
        //プレイヤのキャラクタ属性と同一のターゲットドロップの数を求めてダメージ計算関数に渡す
        DamageAdd((AttackLevel)DropManager.GetComponent<DropManager>().TargetDelete(DropType));
    }   

    // Use this for initialization
    void Start () {
        DropManager = GameObject.Find("DropManager");
        ButtlegameObject = GameObject.Find("FightManager");
    }

    // Update is called once per frame
    public bool IsUpdate()
    { 
       if (Collision())
       {
          DropJudgment();
            return true;
       }


       return false;
    }

    /// <summary>
    /// ダメージ計算をして合計攻撃力に追加   
    /// </summary>
    /// <param name="level">攻撃レベル</param>
    public void DamageAdd(AttackLevel level)
    {
        ToatalAttack += (int)level  * Attack;
    }


    /// <summary>
    /// タッチされたかどうか
    /// </summary>
    /// <returns>真(押された)偽(押されてない)</returns>
    public bool Collision()
    {
        ////タッチ情報取得
        //if (Input.touchCount > 0)
        //{
        //     Touch touch = Input.GetTouch(0);
        //    //タッチ座標
        //    Vector3 touchpos = new Vector3(touch.position.x,touch.position.y,0);
        //    if (transform.position.x <= touchpos.x && transform.position.x + transform.localScale.x >= touchpos.x)
        //    {
        //        if (transform.position.y <= touchpos.y && transform.position.y + transform.localScale.y >= touchpos.y)
        //        {
        //            return true;
        //        }
        //    }
        //}
        float size = 1.0f;
        //タッチ情報取得
        if (Input.GetMouseButtonDown(0))
        {
            //タッチ座標
            Vector3 touchpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //マウス位置座標をスクリーン座標からワールド座標に変換する
            Vector3 screenToWorldPosition = Camera.main.ScreenToWorldPoint(touchpos);
       

            if (transform.position.x - size / 2 <= screenToWorldPosition.x && transform.localPosition.x + size / 2>= screenToWorldPosition.x)
            {
                if (transform.localPosition.y - size / 2 <= screenToWorldPosition.y && transform.localPosition.y + size / 2>= screenToWorldPosition.y)
                {
                    return true;
                }
            }

        }
        return false;
    }
}


//public class CirclePlayer : Player
//{
//    // Use this for initialization
//    void Start()
//    {
//        droptype = Drop.DROPTYPE.Circle;
//    }

//}

//public class CrossPlayer : Player
//{
//    // Use this for initialization
//    void Start()
//    {
//        droptype = Drop.DROPTYPE.Cross;
//    }
//}


//public class TryanglePlayer : Player
//{
//    // Use this for initialization
//    void Start()
//    {
//        droptype = Drop.DROPTYPE.Tryangle;
//    }
//}
