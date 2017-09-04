using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    //制限時間
    public int TimeLimit;
    //現在の時間
    private int NowTime;
    //スキルスタック
    //List<>
    //
    public bool IsTouch = true;

    int TouchCnt = 0;


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
        //if (IsTouch)
        //{
        //    TouchCnt++;
        //}

        //if (TouchCnt > 60)
        //{
        //    TouchCnt = 0;
            IsTouch = true;
        
	}



    ///ゲーム作成
    //ドロップ作成
    //プレイヤ作成
    //敵作成
    
}
