using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour {

    public void ClickedEvent()
    {
        SceneLoader.ChangeScene(SceneLoader.MakeQueue("Battle") ,1.5f, 1.5f );
    }
}
