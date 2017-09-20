using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unown : Player {

    GameObject PlayerManager;
    // Use this for initialization
    public override void Awake()
    {
        HP = 800;
        Attack = 400;
        //MaxSkillPoint = 4;
        Recovery = 400;
        attackData.droptype = Drop.DROPTYPE.Tryangle;
        InitRecovery = Recovery;
        InitAttack = Attack;
        PlayerManager = GameObject.Find("PlayerManager");
    }


    public override void Skil()
    {
        PlayerManager.GetComponent<PlayerManager>().adfadf();
    }
}
