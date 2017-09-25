using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageData : MonoBehaviour {
    public Image backGround;
    public float stageLevel;

    [System.SerializableAttribute]
    public class EnemyData
    {
        public List<GameObject> List = new List<GameObject>();

        public EnemyData(List<GameObject> list)
        {
            List = list;
        }
    }

    public List<EnemyData> enemyList = new List<EnemyData>();

    public GameObject normalBGM;// 仮
    public GameObject bossBGM;// 仮
}
