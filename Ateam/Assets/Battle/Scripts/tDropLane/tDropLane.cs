using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tDropLane : MonoBehaviour {

    public GameObject DropCross;
    public GameObject DropCirce;
    public GameObject DropTryangle;

    public GameObject[] Lanes = new GameObject[3];
    
    const float SPACE_BETWEEN_DROPS = 2.3f;
    const float DROP_START_Z = -2.75f;

    const int LANE_NUM = 3;
    const int DROP_NUM_ON_LANE = 4;

    tDrop[,] Drops = new tDrop[LANE_NUM, DROP_NUM_ON_LANE];

    void Update()
    {
        for(int L=0; L < LANE_NUM; L++)
        {
            for (int D = DROP_NUM_ON_LANE-2; D >= 0; D--)
            {
                if (Drops[L, D+1] == null)
                {
                    if (Drops[L, D] == null) continue;
                    Drops[L, D + 1] = Drops[L, D];
                    Drops[L, D+ 1].MoveToTargetZ(DROP_START_Z + SPACE_BETWEEN_DROPS*(D+1));
                    Drops[L, D] = null;
                }
            }
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
    }

    public void DestroyUnderDrop(tDrop.Type type)
    {

        for (int L = 0; L < LANE_NUM; L++)
        {
            for (int D = DROP_NUM_ON_LANE - 2; D >= 0; D--)
            {
                if (Drops[L, D] == null) return;
                if (Drops[L, D].Moving) return;
            }
        }

        for (int L = 0; L < LANE_NUM; L++)
        {
            if (Drops[L, DROP_NUM_ON_LANE-1].type == type)
            {
                Destroy(Drops[L, DROP_NUM_ON_LANE-1].gameObject);
                Drops[L, DROP_NUM_ON_LANE-1] = null;
            }
        }
    }
}
