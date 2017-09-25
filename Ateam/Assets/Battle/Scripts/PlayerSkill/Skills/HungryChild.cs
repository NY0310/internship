using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryChild : MonoBehaviour {

    BattleManager battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void Activate()
    {
        battleManager.DamagedUp(1, 0f);
    }
}
