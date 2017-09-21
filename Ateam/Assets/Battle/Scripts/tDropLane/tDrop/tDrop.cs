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
    
    float targetZ;
    const float MOVE_SPEED = 1f;

    public void MoveToTargetZ(float z)
    {
        targetZ = z;
        moving = true;
    }

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
