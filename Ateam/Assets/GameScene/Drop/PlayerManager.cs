using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    public GameObject CirclePlayerPrefab;
    public GameObject CrossPlayerPrefab;
    public GameObject TryanglePlayerPrefab;

    //キャラクタのリスト
    List<GameObject> PlayerList = new List<GameObject>();

    //キャラクタの合計体力
    int ToatalHP;
    //キャラクタの合計体力プロパティ
    public int _ToatalHP
    {
        get { return _ToatalHP; }
        set { _ToatalHP = value; }

    }

    //合計攻撃データの受け渡し用の構造体
    struct ToatalData
    {
        public int Attack;
        public int Recovery;
    }


    //}
    //合計回復
    int ToatalRecovery;

    GameObject DropManager;

    // Use this for initialization
    void Start()
    {
        DropManager = GameObject.Find("DropManager");

        ////初期化
        //ToatalAttack = 0;
        //プレハブ生成
        //HPの合計値を計算
        ToatalHP = 0;
        GameObject DropPrefab;
        DropPrefab = Instantiate(CirclePlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Circle;
        PlayerList.Add(DropPrefab);
        ToatalHP += DropPrefab.GetComponent<Player>()._HP;

        DropPrefab = Instantiate(CrossPlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Cross;
        PlayerList.Add(DropPrefab);
        ToatalHP += DropPrefab.GetComponent<Player>()._HP;


        DropPrefab = Instantiate(TryanglePlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Tryangle;
        PlayerList.Add(DropPrefab);
        ToatalHP += DropPrefab.GetComponent<Player>()._HP;



    }



    GameObject ButtlegameObject;

    // Update is called once per frame
    void Update () {
        ButtlegameObject = GameObject.Find("FightManager");
        if (ButtlegameObject.GetComponent<FightManager>()._IsTouch == true)
        {
            Ifneded();
        }
            
    }

    /// <summary>
    /// ビンゴが揃ったらその属性に応じたキャラに攻撃を追加
    /// </summary>
    void Ifneded()
    {
        Drop.DROPTYPE droptype = DropManager.GetComponent<DropManager>().IfNeeded();
        if (droptype != Drop.DROPTYPE.MAX)
        {
            foreach (GameObject List in PlayerList)
            {
                if (List.GetComponent<Player>()._DropType == droptype)
                {
                    List.GetComponent<Player>().DamageAdd(Player.AttackLevel.LevelTree);
                }
            }
        }
    }

    /// <summary>
    /// キャラクタの合計攻撃力を求める
    /// </summary>
    /// <returns>合計攻撃力を返す</returns>
    ToatalData ToatalAttack()
    {
        ToatalData toataldata = new ToatalData();
        foreach (GameObject List in PlayerList)
        {
            if (List.GetComponent<Player>()._AttackType == Player.AttackType.Attack)
            {
                toataldata.Attack += List.GetComponent<Player>()._Attack;
            }
            else
            {
                toataldata.Recovery += List.GetComponent<Player>()._Attack;
            }
        }
        return toataldata;
    }


    /// <summary>
    /// HPからダメージをうける
    /// </summary>
    /// <param name="damage"></param>
    void HitDamage(int damage)
    {
        ToatalHP -= damage;
    }

}
