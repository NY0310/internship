using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {


    public GameObject CirclePlayerPrefab;
    public GameObject CrossPlayerPrefab;
    public GameObject TryanglePlayerPrefab;

    //キャラクタのリスト
    List<GameObject> PlayerList = new List<GameObject>();
    [SerializeField]
    public GameObject HpPrefab;
    GameObject g;

    //キャラクタの初期合計体力
    int MaxToatalHP;
    //キャラクタの合計体力
    int ToatalHP  = 500;
    //キャラクタの合計体力プロパティ
    public int _ToatalHP
    {
        get { return ToatalHP; }
        set { ToatalHP = value; }
    }

    //合計攻撃データの受け渡し用の構造体
    public struct ToatalData
    {
        public int Attack;
        public int Recovery;
    }
     
   
    //ドロップマネージャー
    GameObject DropManager;
    GameObject ButtlegameObject;

    //更新処理を行うか
    bool CanUpdata;
    public bool _CanUpdata
    {
        get { return CanUpdata; }
        set { CanUpdata = value; }
    }

    // Use this for initialization
    void Start()
    {
        DropManager = GameObject.Find("DropManager");
        ButtlegameObject = GameObject.Find("FightManager");

        HpPrefab = Instantiate(HpPrefab) as GameObject;
        HpPrefab.GetComponentInChildren<Slider>().GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -90, 0);

        ////初期化
        //ToatalAttack = 0;
        //プレハブ生成
        //HPの合計値を計算
        ToatalHP = 0;
        GameObject DropPrefab;
        DropPrefab = Instantiate(CirclePlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Circle;
        PlayerList.Add(DropPrefab);

        DropPrefab = Instantiate(CrossPlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Cross;
        PlayerList.Add(DropPrefab);


        DropPrefab = Instantiate(TryanglePlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Tryangle;
        PlayerList.Add(DropPrefab);
        aaa();

    }


    void aaa()
    {
        foreach (var list in PlayerList)
        {
            ToatalHP += 100;
        }
        MaxToatalHP = ToatalHP;
    }

    // Update is called once per frame
    public bool IsUpdate()
    {
            bool touchflag = false;
            //if (ButtlegameObject.GetComponent<FightManager>()._IsDropOperation)
           // if (ButtlegameObject.GetComponent<FightManager>()._IsTouch == true)
            
                Ifneded();
                //全てのプレイヤの更新処理を呼びだす
                foreach (var list in PlayerList)
                {
                    if (list.GetComponent<Player>().IsUpdate())
                    {
                        touchflag = true;
                    }
                }

            
  
        

        return touchflag;
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
    public ToatalData ToatalAttack()
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


   // /// <summary>
   // /// プレイヤを一つでもタッチしているか
   // /// </summary>
   // /// <returns>タッチされたか</returns>
   //public　bool IsTouch()
   // {
   //     foreach (var list in PlayerList)
   //     {
   //         if (list.GetComponent<Player>().Collision())
   //             return true;
   //     }
   //     return false;
   // }

    /// <summary>
    /// HPからダメージをうける
    /// </summary>
    /// <param name="damage"></param>
    public void HitDamage(int damage)
    {
        ToatalHP -= damage;
        SetHp();


    }

    public void Recovery(int recovery)
    {
        ToatalHP += recovery;
        SetHp();
    }


    void SetHp()
    {
   
         


        if (ToatalHP >= 0)
        {
            HpPrefab.GetComponent<HpBar>()._Hp = (float)ToatalHP / (float)MaxToatalHP;

        }
        else
        {
            HpPrefab.GetComponent<HpBar>()._Hp = 0.0f;

        }



    }

}


