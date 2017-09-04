using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    public GameObject CirclePlayerPrefab;
    public GameObject CrossPlayerPrefab;
    public GameObject TryanglePlayerPrefab;

    //キャラクタの合計体力
    int ToatalHP;
    //キャラクタの合計体力プロパティ
    public int _ToatalHP
    {
        get { return _ToatalHP; }
        set { _ToatalHP = value; }

    }

    //キャラクタの合計攻撃
    int ToatalAttack;
    //キャラクタの合計攻撃プロパティ
    public int _ToatalAttack
    {
        get { return ToatalAttack; }
        set { ToatalAttack = value; }

    }

    // Use this for initialization
    void Start () {
        //プレハブ生成
        //HPの合計値を計算
        ToatalHP = 0;
        GameObject DropPrefab;
        DropPrefab = Instantiate(CirclePlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Circle;
        ToatalHP += DropPrefab.GetComponent<Player>()._HP;

        DropPrefab = Instantiate(CrossPlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Cross;
        ToatalHP += DropPrefab.GetComponent<Player>()._HP;


        DropPrefab = Instantiate(TryanglePlayerPrefab);
        DropPrefab.GetComponent<Player>()._DropType = Drop.DROPTYPE.Tryangle;
        ToatalHP += DropPrefab.GetComponent<Player>()._HP;



    }




    // Update is called once per frame
    void Update () {
		
	}
}
