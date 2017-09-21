using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : TryangleEnemy
{
    public override void Start()
    {
        //初期化
        HP = 1000;
        MAXHP = HP;
        DropType = Drop.DROPTYPE.Tryangle;
        Attack = 30;
        HpBatInitialize();

    }
}