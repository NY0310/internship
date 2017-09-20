using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : CircleEnemy
{
    public override void Start()
    {

        //初期化
        HP = 10000;
        MAXHP = HP;
        DropType = Drop.DROPTYPE.Circle;
        Attack = 100;
        HpBatInitialize();
    }



}


