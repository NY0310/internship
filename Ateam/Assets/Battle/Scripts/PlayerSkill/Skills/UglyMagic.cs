using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UglyMagic : MonoBehaviour {

    BattleManager battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void Activate()
    {
        battleManager.dropLane.MakeDrop(tDrop.Type.All, 0, 0);
        battleManager.dropLane.MakeDrop(tDrop.Type.All, 1, 1);
        battleManager.dropLane.MakeDrop(tDrop.Type.All, 2, 2);
    }
}
