using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
  

    

    int SkillPoint = 0;

    GameObject DropManager;
    //
    GameObject ButtlegameObject;





    /// <summary>
    /// 攻撃(ターゲットドロップの中に)
    /// </summary>
    protected void Attack()
    {
        if (DropManager.GetComponent<DropManager>().TargetDelete(DropType) > 1)
        {

        } 
    }   

    // Use this for initialization
    void Start () {
        DropManager = GameObject.Find("DropManager");
    }

    // Update is called once per frame
    void Update () {

       

        //タッチ不可にする
        ButtlegameObject = GameObject.Find("FightManager");
        if (ButtlegameObject.GetComponent<FightManager>()._IsTouch == true)
        {
            if (Collision())
            {
                Attack();
            }
            if (DropManager.GetComponent<DropManager>().IfNeeded())
            {
                SkillPoint++;
            }

        }

     
    }


    /// <summary>
    /// タッチされたかどうか
    /// </summary>
    /// <returns>真(押された)偽(押されてない)</returns>
    protected bool Collision()
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
}


//public class CirclePlayer : Player
//{
//    // Use this for initialization
//    void Start()
//    {
//        droptype = Drop.DROPTYPE.Circle;
//    }

//}

//public class CrossPlayer : Player
//{
//    // Use this for initialization
//    void Start()
//    {
//        droptype = Drop.DROPTYPE.Cross;
//    }
//}


//public class TryanglePlayer : Player
//{
//    // Use this for initialization
//    void Start()
//    {
//        droptype = Drop.DROPTYPE.Tryangle;
//    }
//}
