using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTeaParty : MonoBehaviour {

    BattleManager battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void Activate()
    {
        battleManager.playerAttackRemaining.Restart(5f);
    }
}
