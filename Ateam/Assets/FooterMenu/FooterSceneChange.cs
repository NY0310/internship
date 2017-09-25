using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FooterSceneChange : MonoBehaviour {

    public string SceneName;

    public void ClickedEvent()
    {
        SceneLoader.ChangeScene(SceneLoader.MakeQueue(SceneName), 0f, 0f, SceneLoader.MakeList("Footer"));
    }
}
