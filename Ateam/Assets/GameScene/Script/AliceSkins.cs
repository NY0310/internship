﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceSkins : Player {

	// Use this for initialization
	public override void Awake() {
        Attack = 500;
        _HP = 900;
        MaxSkillPoint = 3;
        Recovery = 300;
        attackData.droptype = Drop.DROPTYPE.Circle;
        Initialize();

    }
	

    public override void Skil()
    {
        throw new NotImplementedException();
    }

}
