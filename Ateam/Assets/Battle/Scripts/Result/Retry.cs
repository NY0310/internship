using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour {

    public void ClickedEvent()
    {
        SceneLoader.ChangeScene(SceneLoader.MakeQueue("Battle"), BGM.Name.BATTLE, 1.5f, 1.5f);
    }
}
