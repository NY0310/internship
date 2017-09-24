using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttackButton : MonoBehaviour {

    public int lanePos;
    public tDropLaneBase lane;
    public BattleManager battleManager;
    public void ClickedEvent()
    {
        tDrop.Type type = lane.Drops[lanePos, tDropLaneBase.DROP_NUM_ON_LANE - 1].type;
        battleManager.UserInput(type);
    }
}
