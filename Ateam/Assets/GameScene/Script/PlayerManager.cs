using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    [SerializeField]
    public GameObject CirclePlayerPrefab;
    [SerializeField]
    public GameObject CrossPlayerPrefab;
    [SerializeField]
    public GameObject TryanglePlayerPrefab;
    //ボタンマネージャ
    GameObject BottanManager;
    //キャラクタのリスト
    List<GameObject> PlayerList = new List<GameObject>();
    [SerializeField]
    public GameObject HpPrefab;

    //キャラクタの初期合計体力
    int MaxToatalHP;
    //キャラクタの合計体力
    int ToatalHP;
    //キャラクタの合計体力プロパティ
    public int _ToatalHP
    {
        get { return ToatalHP; }
        set { ToatalHP = value; }
    }

   
    //ドロップマネージャー
    GameObject DropManager;
    //  GameObject ButtlegameObject;

   
    // Use this for initialization
    void Start()
    {
        DropManager = GameObject.Find("DropManager");
       // ButtlegameObject = GameObject.Find("FightManager");

        HpPrefab = Instantiate(HpPrefab) as GameObject;
        HpPrefab.GetComponentInChildren<Slider>().GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -90, 0);

        ////初期化
        //ToatalAttack = 0;
        //プレハブ生成
        //HPの合計値を計算
        ToatalHP = 0;
        GameObject DropPrefab;
        DropPrefab = Instantiate(CirclePlayerPrefab);
        PlayerList.Add(DropPrefab);

        DropPrefab = Instantiate(CrossPlayerPrefab);
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　        PlayerList.Add(DropPrefab);


        DropPrefab = Instantiate(TryanglePlayerPrefab);
        //DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Tryangle;
        PlayerList.Add(DropPrefab);
        HPCalculation();
		Setposition ();
    }

    /// <summary>
    /// リセット
    /// </summary>
    public void Reset()
    {
        foreach (var item in PlayerList)
        {
            item.GetComponent<Player>()._ToatalAttack = 0;
            
        }
    }



    /// <summary>
    /// 合計体力算出
    /// </summary>
    void HPCalculation()
    {
       

        foreach (var list in PlayerList)
        {
            //Player ads = list.GetComponent<Player>();
            ToatalHP += list.GetComponent<Player>()._HP;
        }
        MaxToatalHP = ToatalHP;
    }

    /// <summary>
    /// 座標設定
    /// </summary>
	void Setposition()
	{
		float Size = 2;
		int loopCnt = 0;
		foreach (var list in PlayerList) {
			list.GetComponent<Player> ().transform.position = new Vector3 (-4 + (Size *loopCnt), -4, 0);
            loopCnt++;

        }
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


        this.DropManager = GameObject.Find("DropManager");
        BottanManager = GameObject.Find("BottonManager");


        //プレイヤリストから現在押されているボタンの属性と一致しているか
        //true ダメージを加算する
        foreach (var list in PlayerList)
        {
            Player player = list.GetComponent<Player>();

            //if (BottanManager.GetComponent<BottonManager>()._PushBotton != Drop.DROPTYPE.MAX)
            //{
            //    int a = 0;
            //}

            //if (player._attackData.droptype == Drop.DROPTYPE.MAX)  
            //{
            //    int a = 0;

            //}

            if (player._attackData.droptype == BottanManager.GetComponent<BottonManager>()._PushBotton)
            {
                int number = DropManager.GetComponent<DropManager>().TargetSearch(BottanManager.GetComponent<BottonManager>()._PushBotton);
                player.DamageAdd((Player.AttackLevel)number);
            } 
        }

        DropManager.GetComponent<DropManager>().TargetDelete(BottanManager.GetComponent<BottonManager>()._PushBotton);

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
                Player player = List.GetComponent<Player>();
                if (player._attackData.droptype == droptype)
                {
                    List.GetComponent<Player>().DamageAdd(Player.AttackLevel.LevelTree);
                    //揃ったドロップ種と同じキャラの回復値回復する
                    Recovery(player._Recovery);
                    //スキルポイントをためる
                    player._SkillPoint++;
                    SetHpBar();
                }
            }
        }
    }

    /// <summary>
    /// キャラクタの合計攻撃データを取得後渡す
    /// </summary>
    /// <returns>攻撃データを返す</returns>
    public List<Player.AttackData> GetAttackList()
    {
        List<Player.AttackData> attacklist = new List<Player.AttackData>();
        foreach (GameObject List in PlayerList)
        {
            attacklist.Add(List.GetComponent<Player>()._attackData);
        }
        return attacklist;
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
    public List<float> HitDamage(List<int> damage)
    {
        List<float> NewHpDate = new List<float>();
        foreach (var item in damage)
        {
            
            ToatalHP -= item;
            float Hp = SetHpBar();
            if (Hp != HpPrefab.GetComponent<HpBar>()._OldHp)
            {
                NewHpDate.Add(Hp);
            }
        }

        return NewHpDate;
        
    }

    public void Recovery(int recovery)
    {
        if (ToatalHP + recovery >= MaxToatalHP)
        {
            ToatalHP = MaxToatalHP;
        }
        else
        {
            ToatalHP += recovery;
        }
    }

    /// <summary>
    /// HPbarの値設定
    /// </summary>
    float SetHpBar()
    {
        if (ToatalHP > 0)
        {
            HpPrefab.GetComponent<HpBar>()._Hp = (float)ToatalHP / (float)MaxToatalHP;

        }
        else
        {
            HpPrefab.GetComponent<HpBar>()._Hp = 0.0f;

        }
        return HpPrefab.GetComponent<HpBar>()._Hp;
    }

    /// <summary>
    /// ハーメルンのスキル(大樹の誘い)全プレイヤ攻撃力x1.2
    /// </summary>
    public void BigTreeInvitation()
    {
        foreach (var list in PlayerList)
        {
            list.GetComponent<Player>()._Attack *= 1.2f;
        }
    }

}


