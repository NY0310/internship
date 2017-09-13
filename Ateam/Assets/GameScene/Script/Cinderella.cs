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
        MaxSkillPoint = 5;
        attackData.droptype = Drop.DROPTYPE.Cross;
    }
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public override void Skil()
    {
        _DropManager.GetComponent<DropManager>().UglyMagic();
    }
}
