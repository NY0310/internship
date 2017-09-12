using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hameln : Player {

    // Use this for initialization
    public override void Start()
    {
        HP = 900;
        Attack = 500;
        MaxSkillPoint = 3;
        Recovery = 300;
        attackData.droptype = Drop.DROPTYPE.Tryangle;
    }
 

    public override void Skil()
    {
    }
}
