using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    /// <summary>
    /// タッチされたかどうか
    /// </summary>
    /// <returns>真(押された)偽(押されてない)</returns>
    public static  bool IsCollision(Vector2 localposition)
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


            if (localposition.x - size / 2 <= screenToWorldPosition.x && localposition.x + size / 2 >= screenToWorldPosition.x)
            {
                if (localposition.y - size / 2 <= screenToWorldPosition.y && localposition.y + size / 2 >= screenToWorldPosition.y)
                {
                    return true;
                }
            }

        }
        return false;
    }


}
