using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AliceSkins : Player {
    
    GameObject buttleManager;
	// Use this for initialization
	public override void Awake() {
       // Attack = 500;
        //_HP = 900;
       // MaxSkillPoint = 8;
        Recovery = 300;
        attackData.droptype = Drop.DROPTYPE.Circle;
        InitAttack = attackData.ToatalAttack;
        InitRecovery = Recovery;
        Initialize();

    }
	

    public override void Skil()
    {
        buttleManager =  GameObject.Find("ButtleManager");
        buttleManager.GetComponent<ButtleManager>()._NowTime = 5;

    }

}
