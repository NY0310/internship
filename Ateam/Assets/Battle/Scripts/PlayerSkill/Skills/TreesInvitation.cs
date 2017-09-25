using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesInvitation : MonoBehaviour {

    BattleManager battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void Activate()
    {
        battleManager.AttackUp(1,1.5f);
    }
}
