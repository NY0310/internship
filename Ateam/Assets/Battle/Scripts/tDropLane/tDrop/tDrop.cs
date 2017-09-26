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
    const float MOVE_SPEED = 15f;

    float destroyCount = 0f;
    const float DESTROY_TIME = 0.3f;

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

    public bool isDestroying = false;

    float startCount = 0.3f;

    Vector3 defaultScale;
    void Start()
    {
        defaultScale = transform.localScale;
    }

    /// <summary>
    /// 移動処理と、それに伴うフラグ処理
    /// </summary>
    void Update()
    {
        if (startCount > 0)
        {
            startCount -= Time.deltaTime;
            transform.localScale = defaultScale *( 1f + startCount/0.3f) ;
        }

        if (moving)
        {
            float newZ = transform.localPosition.z + MOVE_SPEED * Time.deltaTime;
            if (newZ >= targetZ)
            {
                moving = false;
                newZ = targetZ;
            }
            transform.localPosition = new Vector3(0f, 0f, newZ);
        }
        if (isDestroying)
        {
            destroyCount -= Time.deltaTime;
            Vector3 rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, Mathf.Sin(destroyCount*2.8f) * 360f));
            transform.position += new Vector3(0f,-destroyCount/15f,0f);
            if (destroyCount <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }



    public void DoDestroy()
    {
        destroyCount = DESTROY_TIME;
        isDestroying = true;
    }

}
