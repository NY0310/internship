using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drop単体の持つ情報を扱う。
/// </summary>
public class tDrop : MonoBehaviour {

    public enum Type
    {
        Cross,
        Circle,
        Tryangle,
        All
    }
    public Type type;

    bool moving=false;
    public bool Moving { get { return moving; } }
    
    float targetZ;  // 移動目的地
    const float MOVE_SPEED = 25f;

    /// <summary>
    /// 外部からこの関数が呼ばれると、自動的に移動を開始する。
    /// 目的地に到達すると、フラグをオフにする
    /// </summary>
    /// <param name="z"></param>
    public void MoveToTargetZ(float z)
    {
        targetZ = z;
        moving = true;
    }

    /// <summary>
    /// 移動処理と、それに伴うフラグ処理
    /// </summary>
    void Update()
    {
        if (moving)
        {
            float newZ = transform.localPosition.z + MOVE_SPEED*Time.deltaTime;
            if (newZ >= targetZ)
            {
                moving = false;
                newZ = targetZ;
            }
            transform.localPosition = new Vector3(0f, 0f, newZ);
        }
    }
}
