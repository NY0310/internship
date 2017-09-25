using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooMusical : MonoBehaviour {

    BattleManager battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void Activate()
    {
        var type = battleManager.dropLane.Drops[1, tDropLaneBase.DROP_NUM_ON_LANE - 1].type;
        battleManager.dropLane.MakeDrop(type, 1, 0);
        battleManager.dropLane.MakeDrop(type, 1, 1);
        battleManager.dropLane.MakeDrop(type, 1, 2);
    }
}
