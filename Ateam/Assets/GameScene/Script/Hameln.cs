using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hameln : Player {
    GameObject PlayerManager;
    // Use this for initialization
    public override void Awake()
    {
        HP = 900;
        Attack = 500;
        MaxSkillPoint = 4;
        Recovery = 300;
        attackData.droptype = Drop.DROPTYPE.Cross;
        PlayerManager = GameObject.Find("PlayerManager");
    }


    public override void Skil()
    {
        PlayerManager.GetComponent<PlayerManager>().BigTreeInvitation();
    }
}
