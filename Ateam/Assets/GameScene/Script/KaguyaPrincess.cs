using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaguyaPrincess : Player {

	// Use this for initialization
	public override void Awake() {
        Attack = 300;
        HP = 1100;
        Recovery = 500;
        //MaxSkillPoint = 1;
        attackData.droptype = Drop.DROPTYPE.Tryangle;
        DropManager = GameObject.Find("DropManager").GetComponent<DropManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void Skil()
    {
        DropManager.MoonlightBambooTaking();
    }

}
