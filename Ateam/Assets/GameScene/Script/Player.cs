using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
   [SerializeField]
    //ドロップマネージャ
    GameObject DropManager;
 //   [SerializeField]


    ////合計攻撃力
    //int ToatalAttack;
    //[SerializeField]
    //攻撃力
    public int Attack;
    ////攻撃力のプロパティ
    //public int _Attack
    //{
    //    get { return ToatalAttack; }
    //    set { ToatalAttack = value; }
    //}

    [SerializeField]
    //体力
    public int HP;
    //HPのプロパティ
    public int _HP
    {
        get { return HP; }
        set { HP = value; }
    }

    ////ドロップ属性
    //protected Drop.DROPTYPE DropType;
    ////ドロップ属性のプロパティ
    //public Drop.DROPTYPE _DropType
    //{
    //    get { return DropType; }
    //    set { DropType = value; }
    //}

    protected int Recovery;
    public int _Recovery
    {
        get { return Recovery; }
        set { Recovery = value; }
    }
    ////攻撃タイプの種類
    //public enum AttackType
    //{
    //    Attack,
    //    Recovery
    //}

    ////攻撃タイプ
    //protected AttackType attackType;
    ////攻撃タイプのプロパティ
    //public AttackType _AttackType
    //{
    //    get { return attackType; }
    //    set { attackType = value; }
    //}

    //攻撃レベル
    public enum AttackLevel
    {
        LevelOne,
        LevelTwo,
        LevelTree
    }

    //攻撃データの受け渡し用の構造体
    public struct AttackData
    {
        public int ToatalAttack;
        //public AttackType AttackType;
        public Drop.DROPTYPE droptype;
    }

    protected  AttackData attackData;
    //攻撃データの受け渡し用の構造体のプロパティ
    public AttackData _attackData
    {
        get { return attackData; }
        set { attackData = value; }
    }



    //スキル発動に必要なポイント
    protected int MaxSkillPoint;
    //スキルポイント
    protected int SkillPoint;



    // Use this for initialization
    public abstract void Start();

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
       this.DropManager = GameObject.Find("DropManager");
        attackData.ToatalAttack = 0;
    }

    // Update is called once per frame
    public bool IsUpdate()
    { 
       if (Collision())
       {
            if (MaxSkillPoint == SkillPoint)
            {
              SkillPoint = 0;
              Skil();
            }
            return true;
       }
      //  BottanManager = GameObject.Find("BottonManager");

      //  var a = BottanManager.GetComponent<BottonManager>()._PushBotton;
      // //押されたボタンが同一の場合攻撃ドロップ審査関数を呼びだす
      //if ( BottanManager.GetComponent<BottonManager>()._PushBotton ==attackData.droptype)
      //  {
      //      DropJudgment();
      //  }
       return false;
    }

    /// <summary>
    /// 攻撃(ターゲットドロップの中に)
    /// </summary>
    //public void DropJudgment()
    //{
    //    //     BottanManager = GameObject.Find("BottonManager");
    //    //DropManager a = DropManager.GetComponent<DropManager>();
    //    //a.IfNeeded();
    //    this.DropManager = GameObject.Find("DropManager");

    //    //プレイヤのキャラクタ属性と同一のターゲットドロップの数を求めてダメージ計算関数に渡す
    //    DamageAdd((AttackLevel)DropManager.GetComponent<DropManager>().TargetDelete(attackData.droptype));
    //}
       
    /// <summary>
    /// ダメージ計算をして合計攻撃力に追加   
    /// </summary>
    /// <param name="level">攻撃レベル</param>
    public void DamageAdd(AttackLevel level)
    {
        //if (level < AttackLevel.LevelTree + 1)
        //{
        //    attackData.ToatalAttack += (int)level * Attack;
        //}
        //else
        //{
        //    attackData.ToatalAttack += 5 * Attack;

        //}
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
    void SkillChage()
    {
        SkillPoint++;
    }

    //スキル発動
    public abstract void Skil();

}

