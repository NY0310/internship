using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    //制限時間
    public int TimeLimit;
    //現在の時間
    private int NowTime;
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


    public bool _IsTouch
    {
        get { return IsTouch; }
        set { IsTouch = value; }

    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if (MoveList.Count == 0)
        {
            IsTouch = true;
        }
        else
        {
            IsTouch = false;
        }

    }



    ///ゲーム作成
    //ドロップ作成
    //プレイヤ作成
    //敵作成
    
}
