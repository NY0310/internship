using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyB : CrossEnemy
{
    public override void Start()
    {
        ////hpゲージ作成
        //HpPrefab = Instantiate(HpPrefab);
        //Vector3 pos = transform.position;
        //HpPrefab.GetComponent<HpBar>().transform.position = new Vector3(pos.x, pos.y + INTERVAL_POS, pos.z);
        //初期化
        HP = 10000;
        MAXHP = HP;
        DropType = Drop.DROPTYPE.Cross;
        Attack = 70;
        HpBatInitialize();

    }
}

