using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinderella : Player {

	// Use this for initialization
	public override void Awake() {
        HP = 1300;
        Attack = 300;
        Recovery = 500;
       // MaxSkillPoint = 5;
        InitRecovery = Recovery;
        InitAttack = attackData.ToatalAttack;
        attackData.droptype = Drop.DROPTYPE.Cross;
        DropManager = GameObject.Find("DropManager").GetComponent<DropManager>();

    }

    //// Update is called once per frame
    //void Update () {

    //}

    public override void Skil()
    {
        DropManager.GetComponent<DropManager>().UglyMagic();
    }
}
