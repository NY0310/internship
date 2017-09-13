using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class ButtleManager : MonoBehaviour {
    //バトルの状態
    ButtleState ButtleState;


    [SerializeField]
    //制限時間
    public int TimeLimit;
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
    [SerializeField]
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

<<<<<<< HEAD:Ateam/Assets/GameScene/Script/ButtleManager.cs
    //敵マネージャー
   // [SerializeField]
    GameObject EnemyManager;
    public GameObject _EnemyManager
    {
        get { return EnemyManager; }
        set { EnemyManager = value; }
    }


    //  public GameObject Enemy;
=======
   public GameObject Enemy;
>>>>>>> 79b1bdedd5ba0dc0f494bc86da2f06dd612cface:Ateam/Assets/GameScene/Drop/ButtleManager.cs
    //敵
    public GameObject _Enemy
    {
        get { return Enemy; }
        set { Enemy = value; }

    }

    public bool _IsTouch
    {
        get { return IsTouch; }
        set { IsTouch = value; }

    }
    // Use this for initialization
    void Start () {
        PlayerManager =  GameObject.Find("PlayerManager");
        //Enemy = GameObject.Find("Enemy");
        Enemy = Instantiate(Enemy);
        ButtleState = Wait.GetInstance();
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

    public override void Execute(ButtleManager buttlemanager)
    {

        ////プレイヤが押されたとき状態をドロップ操作へ
        if (buttlemanager._PlayerManager.GetComponent<PlayerManager>().IsUpdate())
            buttlemanager.ChangeState(DropOperation.GetInstance());
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
    AttackState AttackState = EnemyAttack.GetInstance();
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

        buttlemanager.ChangeState(Wait.GetInstance());


    }

 
}

public abstract class AttackState
{
    //純粋仮想関数を宣言
    public abstract void Execute(Attack attack, ButtleManager buttlemanager);

}



/// <summary>
/// プレイヤの攻撃
/// </summary>
public class PlayerAttack : AttackState
{
    static AttackState buttleState;

    //クラスのインスタンスを取得する
    public static AttackState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new PlayerAttack();
        }


        return buttleState;
    }

    public override void Execute(Attack attack, ButtleManager buttlemanager)
    {
<<<<<<< HEAD:Ateam/Assets/GameScene/Script/ButtleManager.cs
        buttlemanager._EnemyManager = GameObject.Find("EnemyManager");
        //プレイヤの攻撃データを取得
        var AttackData = buttlemanager._PlayerManager.GetComponent<PlayerManager>().GetAttackList();
=======
          //プレイヤの攻撃データを取得
        PlayerManager.ToatalData ToatalData = buttlemanager._PlayerManager.GetComponent<PlayerManager>().ToatalAttack();
>>>>>>> 79b1bdedd5ba0dc0f494bc86da2f06dd612cface:Ateam/Assets/GameScene/Drop/ButtleManager.cs
        //敵へ攻撃
        buttlemanager._Enemy.GetComponent<Enemy>().HitDamage(ToatalData.Attack);
        //回復状態に移行
        attack._AttackState = Recovery.GetInstance();

    }

}


/// <summary>
/// 回復
/// </summary>
public class Recovery : AttackState
{
    static AttackState buttleState;

    //クラスのインスタンスを取得する
    public static AttackState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new Recovery();
        }


        return buttleState;
    }

    public override void Execute(Attack attack, ButtleManager buttlemanager)
    {
        //プレイヤの攻撃データを取得
        PlayerManager.ToatalData ToatalData = buttlemanager._PlayerManager.GetComponent<PlayerManager>().ToatalAttack();
        //プレイヤの回復
      //  buttlemanager._PlayerManager.GetComponent<PlayerManager>().Recovery(ToatalData.Recovery);

        //敵の攻撃状態に移行
        attack._AttackState = EnemyAttack.GetInstance();

    }

}



/// <summary>
/// 敵の攻撃
/// </summary>
public class EnemyAttack : AttackState
{
    static AttackState buttleState;

    //クラスのインスタンスを取得する
    public static AttackState GetInstance()
    {
        if (buttleState == null)
        {
            buttleState = new EnemyAttack();
        }


        return buttleState;
    }

    public override void Execute(Attack attack, ButtleManager buttlemanager)
    {
        buttlemanager._EnemyManager = GameObject.Find("EnemyManager");

        //敵からプレイヤへ攻撃
        buttlemanager._PlayerManager.GetComponent<PlayerManager>().
        HitDamage(buttlemanager._Enemy.GetComponent<Enemy>()._Attack);
        //プレイヤの攻撃に移行
        attack._AttackState = PlayerAttack.GetInstance();

    }

}

