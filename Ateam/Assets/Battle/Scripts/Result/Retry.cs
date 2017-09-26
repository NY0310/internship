using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour {

    public void ClickedEvent()
    {
        SceneLoader.ChangeScene(SceneLoader.MakeQueue("Battle"), 0f, 0f);
    }
}
