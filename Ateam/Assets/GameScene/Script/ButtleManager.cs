using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtleManager : MonoBehaviour {
    [SerializeField]
    GameObject Stage;
    public GameObject _Stage
    {
        get { return Stage; }
    }
    //バトルの状態
    ButtleState ButtleState;
    [SerializeField]
    //ボタンマネージャ
    GameObject BottanManager;
    public GameObject _BottanManager
    {
        get { return BottanManager; }
        set { BottanManager = value; }
    }
    [SerializeField]
    //バトルアニメーション
    GameObject ButtleAnimation;
    List<ButtleAnimation> ButtleAnimationList = new List<ButtleAnimation>();
    public List<ButtleAnimation> _ButtleAnimationList
    {
        get { return ButtleAnimationList; }
        set { ButtleAnimationList = value; }
    }
    [SerializeField]
    //制限時間
    int TimeLimit;
    //現在の時間のプロパティ
    public int _TimeLimit
    {
        get { return TimeLimit; }
        set { TimeLimit = value; }
    }

    //現在の時間
    private int NowTime;
    //現在の時間のプロパティ
    public int _NowTime
    {
        get { return NowTime; }
        set { NowTime = value; }
    }
    //タッチできるか
    public bool IsTouch = true;
    //動作のリスト
    List<GameObject> MoveList = new List<GameObject>();
    //動作のリストプロパティ
    public List<GameObject> _MoveList
    {
        get { return MoveList; }
        set { MoveList = value; }

    }

    //プレイヤマネージャ
    GameObject PlayerManager;
    //プレイヤマネージャのプロパティ
    public GameObject _PlayerManager
    {
        get { return PlayerManager; }
        set { PlayerManager = value; }

    }

    //敵マネージャー
   [SerializeField]
    GameObject EnemyManager;
    public GameObject _EnemyManager
    {
        get { return EnemyManager; }
        set { EnemyManager = value; }
    }


    //  public GameObject Enemy;
    //敵
    //public GameObject _Enemy
    //{
    //    get { return Enemy; }
    //    set { Enemy = value; }

    //}

    public bool _IsTouch
    {
        get { return IsTouch; }
        set { IsTouch = value; }

    }
    // Use this for initialization
    void Start () {
        PlayerManager =  GameObject.Find("PlayerManager");
        //Enemy = GameObject.Find("Enemy");
       // Enemy = Instantiate(Enemy);
        ButtleState = EnemyCreate.GetInstance();
        //ステージ生成
        Stage = Instantiate(Stage);
        EnemyManager = Instantiate(EnemyManager);
        //  BottanManager = GameObject.Find("BottonManager");
    }

    // Update is called once per frame
    void Update () {
     

        //状態に応じて実行
        ButtleState.Execute(this);
        //タッチ可能か調べる
        CanTouch(ref IsTouch);
    }

    /// <summary>
    /// タッチ可能か調べる
    /// </summary>
    /// <param name="touch">タッチ可能かを格納する変数</param>
    /// <returns>タッチ可能か</returns>
    bool CanTouch(ref bool touch)
    {
        //ドロップが一つも動いていないとき
        if (MoveList.Count == 0)
        {
            touch = true;
           return true;
        }
        else
        {
            touch = false;
            return false;
        }
        

    }

    //現在の状態を変更する
    public void ChangeState(ButtleState buttlestate)
    {
        ButtleState = buttlestate;
        
    }

    /// <summary>
    /// バトル用アニメーションクラス破棄(破棄フラグが立っているなら)
    /// </summary>
    public void ButtleAnimationDestroy()
    {
        foreach (var item in ButtleAnimationList)
        {
            if(item == null)
                break;
            if (item._IsDestroy)
            {
                Destroy(item.gameObject);
                ButtleAnimationList.RemoveAt(0);
                break;
            }
        }

    }



      


    /// <summary>
    /// バトル用アニメーションクラス作成、初期化
    /// </summary>
    /// <param name="Hp">アニメーションするHP</param>
    /// <param name="NewHp">アニメーション後HP</param>
    /// <param name="StartPosition">エフェクトの初期座標</param>
    /// <param name="GorlPosition">エフェクト後座標</param>
    public void ButtleAnimationCreate(HpBar Hp, float NewHp, Vector2 StartPosition, Vector2 GorlPosition)
    {
        ButtleAnimation buttleAnimation = Instantiate(ButtleAnimation).GetComponent<ButtleAnimation>();
        ButtleAnimationList.Add(buttleAnimation);
        buttleAnimation.Inisialize(Hp, NewHp, StartPosition, GorlPosition);
       
    }
}

/// <summary>
/// バトルの状態(stateパターン)
/// </summary>
public abstract class ButtleState
{

    //純粋仮想関数を宣言
    public abstract void Execute(ButtleManager buttlemanager);

    //
   // public ~ButtleState();

}

public class EnemyCreate : ButtleState
{
    static ButtleState buttleState;

    //Standクラスのインスタンスを取得する
    public static ButtleState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new EnemyCreate();
        }

        return buttleState;
    }

    int CreateCnt;

    public override void Execute(ButtleManager buttlemanager)
    {
        if (buttlemanager._Stage.GetComponent<Stage>()._ValueListList.Count >= CreateCnt + 1)
        {
            //敵を作成
            buttlemanager._EnemyManager.GetComponent<EnemyManager>().EnemyCreate
                (buttlemanager._Stage.GetComponent<Stage>()._ValueListList[CreateCnt].List);
            CreateCnt++;
            buttlemanager.ChangeState(Initialize.GetInstance());
        }
      
    }
}


public class Initialize : ButtleState
{
    static ButtleState buttleState;

    //Standクラスのインスタンスを取得する
    public static ButtleState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new Initialize();
        }

        return buttleState;
    }

    public override void Execute(ButtleManager buttlemanager)
    {
        buttlemanager._PlayerManager.GetComponent<PlayerManager>().Reset();
        buttlemanager.ChangeState(Wait.GetInstance());

    }
}

/// <summary>
/// 待機状態
/// </summary>
public class Wait : ButtleState
{
    static ButtleState buttleState;

    //Standクラスのインスタンスを取得する
    public static ButtleState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new Wait();
        }

        return buttleState;
    }
    bool IsFirst = true;

    public override void Execute(ButtleManager buttlemanager)
    {
         GameObject bottan = GameObject.Find("BottonManager");

        Drop.DROPTYPE type = bottan.GetComponent<BottonManager>()._PushBotton;

        if (IsFirst)
        {
            //プレイヤ情報を一部リセット
            buttlemanager._PlayerManager.GetComponent<PlayerManager>().Reset();
            IsFirst = false;
        }
        //プレイヤの更新処理呼び出し
        buttlemanager._PlayerManager.GetComponent<PlayerManager>().IsUpdate();

        ////プレイヤが押されたとき状態をドロップ操作へ
        if (type != Drop.DROPTYPE.MAX)
        {
            buttlemanager.ChangeState(DropOperation.GetInstance());
            IsFirst = true;

        }

    }


}

/// <summary>
/// ドロップ操作状態
/// </summary>
public class DropOperation : ButtleState
{
    static ButtleState buttleState;

    //Standクラスのインスタンスを取得する
    public static ButtleState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new DropOperation();
        }


        return buttleState;
    }

    public override void Execute(ButtleManager buttlemanager)
    {
        //時間更新
        buttlemanager._NowTime++;

        //タイムリミットになったら初期化して状態を攻撃へ
        if (buttlemanager._NowTime / 60 >= buttlemanager._TimeLimit)
        {
            buttlemanager._NowTime = 0;
            buttlemanager.ChangeState(Attack.GetInstance());
        }



        //プレイヤの更新処理呼び出し
        buttlemanager._PlayerManager.GetComponent<PlayerManager>().IsUpdate();
    }

}

/// <summary>
/// 攻撃一連のクラス(Stateパターン)
/// </summary>
public class Attack : ButtleState
{
    static ButtleState buttleState;
    AttackState AttackState = PlayerAttack.GetInstance();
    public AttackState _AttackState
    {
        get { return AttackState; }
        set { AttackState = value; }

    }

    //Standクラスのインスタンスを取得する
    public static ButtleState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new Attack();
        }

        return buttleState;
    }

    public override void Execute(ButtleManager buttlemanager)
    {

        AttackState.Execute(this,buttlemanager);

 
    }

 
}

public abstract class AttackState
{
    //純粋仮想関数を宣言
    public abstract void Execute(Attack attack, ButtleManager buttlemanager);
    //次に待機状態か敵生成状態か
    protected static bool IsEnemyCreate;
}



/// <summary>
/// プレイヤの攻撃
/// </summary>
public class PlayerAttack : AttackState
{
    static AttackState buttleState;
    static bool IsFirst;
    //クラスのインスタンスを取得する
    public static AttackState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new PlayerAttack();
        }

        IsFirst = true;
        return buttleState;
    }

    public override void Execute(Attack attack, ButtleManager buttlemanager)
    {

        if (IsFirst)
        {
           // buttlemanager._EnemyManager = GameObject.Find("EnemyManager");
            //プレイヤの攻撃データを取得
            var AttackData = buttlemanager._PlayerManager.GetComponent<PlayerManager>().GetAttackList();
            //敵へ攻撃
            List<EnemyManager.HpAnimationData> date = buttlemanager._EnemyManager.GetComponent<EnemyManager>().HitDamage(AttackData);

            foreach (var item in date)
            {
            
                    buttlemanager.ButtleAnimationCreate(item.Hp, item.NewHp, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
     
            }
            IsFirst = false;
        }
        buttlemanager.ButtleAnimationDestroy();

        //アニメーションが全て終わったら
        if (buttlemanager._ButtleAnimationList.Count == 0)
        {
            foreach (var item in buttlemanager._EnemyManager.GetComponent<EnemyManager>()._EnemyList)
            {
                IsEnemyCreate = true;

                if (item._HP != 0)
                {
                    IsEnemyCreate = false;
                    break;
                }
            }
          
          
            //敵の攻撃に移行
           attack._AttackState = EnemyAttack.GetInstance();
        }

       
    }

}





/// <summary>
/// 敵の攻撃
/// </summary>
public class EnemyAttack : AttackState
{
    static AttackState buttleState;
    static bool IsFirst;

    //クラスのインスタンスを取得する
    public static AttackState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new EnemyAttack();
        }

        IsFirst = true;
        return buttleState;
    }

    public override void Execute(Attack attack, ButtleManager buttlemanager)
    {
        //  buttlemanager._EnemyManager = GameObject.Find("EnemyManager");

        if (IsFirst)
        {
            //敵からプレイヤへ攻撃
            List<float> NewHpDate = buttlemanager._PlayerManager.GetComponent<PlayerManager>().HitDamage(buttlemanager._EnemyManager.GetComponent<EnemyManager>().GetAttack());
            foreach (var item in NewHpDate)
            {
                buttlemanager.ButtleAnimationCreate(buttlemanager._PlayerManager.GetComponent<PlayerManager>().HpPrefab.GetComponent<HpBar>(), item, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            }
            IsFirst = false;


        }
        buttlemanager.ButtleAnimationDestroy();

        if (buttlemanager._ButtleAnimationList.Count == 0)
        {
            if (IsEnemyCreate)
            {
                buttlemanager.ChangeState(EnemyCreate.GetInstance());
            }
            else
            {
                buttlemanager.ChangeState(Wait.GetInstance());

            }
            //プレイヤの攻撃に移行
            attack._AttackState = PlayerAttack.GetInstance();

        }
    }

}

