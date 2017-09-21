using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dropの動き、入力の受け付けを決める
/// </summary>
public class tDropLane : MonoBehaviour {

    public GameObject DropCross;
    public GameObject DropCirce;
    public GameObject DropTryangle;

    public GameObject[] Lanes = new GameObject[3];
    
    const float SPACE_BETWEEN_DROPS = 2.3f;     // DropとDropの縦の間隔
    const float DROP_START_Z        = -2.75f;   // 一番上のドロップの位置

    const int LANE_NUM = 3;
    const int DROP_NUM_ON_LANE = 4;

    tDrop[,] Drops = new tDrop[LANE_NUM, DROP_NUM_ON_LANE];

    void Update()
    {
        DropPositionUpdate();
    }

    void DropPositionUpdate()
    {
        for (int L = 0; L < LANE_NUM; L++)
        {
            for (int D = DROP_NUM_ON_LANE - 2; D >= 0; D--) // 一番下の要素から判定することで、１フレームで全要素を正しく落下させる
            {
                if (Drops[L, D + 1] == null)                // 下が空いていたら（ドロップが存在してない場合）
                    MoveDownDrop(L, D);                     // 落下する（位置を詰める）
            }
            MakeTopDrop_IfDoesNotExist(L);
        }
    }

    /// <summary>
    /// 存在するDropなら、落下させる
    /// </summary>
    /// <param name="L">レーン</param>
    /// <param name="D">ドロップ</param>
    void MoveDownDrop(int L, int D)
    {
        if (Drops[L, D] == null) return;
        Drops[L, D + 1] = Drops[L, D];  // 下の要素に移す
        Drops[L, D + 1].MoveToTargetZ(DROP_START_Z + SPACE_BETWEEN_DROPS * (D + 1));    // 実際に座標も移動させる
        Drops[L, D] = null; // 元の場所は空白にしておく
    }

    /// <summary>
    /// 一番上の段にDropが無い場合、ランダムに生成する
    /// </summary>
    /// <param name="L">レーン</param>
    void MakeTopDrop_IfDoesNotExist(int L)
    {
        if (Drops[L, 0] == null)
        {
            GameObject useDropType;
            switch (Random.Range(0, 3))
            {
                case 0:
                    useDropType = DropCross;
                    break;
                case 1:
                    useDropType = DropCirce;
                    break;
                case 2:
                    useDropType = DropTryangle;
                    break;
                default:
                    Debug.Log("DropLane.cs/Update(), ランダム値により存在しないDropTypeを生成しようとしました");
                    useDropType = DropCross;
                    break;
            }
            GameObject dropObj = Instantiate(useDropType, Lanes[L].transform);
            dropObj.transform.localPosition = new Vector3(0f, 0f, DROP_START_Z);
            Drops[L, 0] = dropObj.GetComponent<tDrop>();
        }
    }

    /// <summary>
    /// 外部から呼び出される、ユーザー操作によって一番下の段のドロップを破壊する関数
    /// </summary>
    /// <param name="type">破壊するドロップ</param>
    public void DestroyUnderDrop(tDrop.Type type)
    {
        if (IsThereMovingDrop()) return;

        for (int L = 0; L < LANE_NUM; L++)
        {
            if (Drops[L, DROP_NUM_ON_LANE-1].type == type)
            {
                Destroy(Drops[L, DROP_NUM_ON_LANE-1].gameObject);
                Drops[L, DROP_NUM_ON_LANE-1] = null;
            }
        }
    }

    /// <summary>
    /// 一つでも動いているドロップがあるかどうか
    /// </summary>
    /// <returns></returns>
    bool IsThereMovingDrop()
    {
        for (int L = 0; L < LANE_NUM; L++)
        {
            for (int D = DROP_NUM_ON_LANE - 2; D >= 0; D--)
            {
                if (Drops[L, D] == null) return true;
                if (Drops[L, D].Moving) return true;
            }
        }
        return false;
    }
}
