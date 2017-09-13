using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unown : Player {

    // Use this for initialization
    public override void Awake()
    {
        HP = 800;
        Attack = 400;
        MaxSkillPoint = 3;
        Recovery = 400;
        attackData.droptype = Drop.DROPTYPE.Tryangle;
    }


    public override void Skil()
    {
    }
}
