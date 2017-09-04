using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    //スタート座標
    Vector3 StartPos;
    //ゴール座標
    Vector3 GoralPos;
    //目標時間
    float TotalTime;
    //経過時間
    float Time;
    //対象ゲームオブジェクト
    GameObject gameobject;
    //バトルマネージャのゲームオブジェクト
    GameObject ButtlegameObject;


    // Use this for initialization
    void Start () {
        this.Time = 0;
        //タッチ不可にする
        ButtlegameObject = GameObject.Find("FightManager");
        //ButtlegameObject.GetComponent<FightManager>()._IsTouch = false;
        ButtlegameObject.GetComponent<FightManager>()._MoveList.Add(this.gameobject);
    }

    
    /// <summary>
    /// 指定された座標に移動
    /// </summary>
    /// <param name="gameobject">移動させるオブジェクト</param>
    /// <param name="gorlpos">移動したい座標</param>
    /// <param name="time">時間</param>
    public void MoveTo( GameObject gameobject, Vector3 gorlpos,float time)
    {
        this.gameobject = gameobject;
        StartPos = gameobject.transform.position;
        GoralPos = gorlpos;
        TotalTime = time;
    }

    /// <summary>
    /// 指定された座標分だけ移動
    /// </summary>
    /// <param name="gameobject">移動させるオブジェクト</param>
    /// <param name="gorlpos">移動量</param>
    /// <param name="time">時間</param>
    public void MoveBy(GameObject gameobject, Vector3 gorlpos, float time)
    {

        this.gameobject = gameobject;
        StartPos = gameobject.transform.position;
        GoralPos = gameobject.transform.position + gorlpos;
        TotalTime = time;
    }

    // Update is called once per frame
    void Update () {
        
            this.Time++;
            float time = this.Time  - TotalTime  / TotalTime;


        Vector3 pos = Lerp(StartPos, GoralPos, time);
            if (gameobject != null)
            {
                //算出した座標を適用する
                gameobject.transform.position = pos;
            }
        else
        {
            ButtlegameObject.GetComponent<FightManager>()._MoveList.RemoveAt(0);
            Destroy(this.gameObject);
        }

        if (time > 1.0f)
            {
                //タッチ可にする
                ButtlegameObject = GameObject.Find("FightManager");
            ButtlegameObject.GetComponent<FightManager>()._MoveList.RemoveAt(0);
            Destroy(this.gameObject);
             }
      

    }

    /// <summary>
    /// 線形補間
    /// </summary>
    /// <param name="StartPosition">スタート座標</param>
    /// <param name="GorlPosition">ゴール座標</param>
    /// <param name="Time">時間</param>
    /// <returns>算出座標</returns>
    Vector3 Lerp(Vector3 StartPosition, Vector3 GorlPosition , float Time)
    {
        return (1 - Time) * StartPosition + GorlPosition * Time;
    }


}
