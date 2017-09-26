using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FooterSceneChange : MonoBehaviour {

    public string SceneName;

    public void ClickedEvent()
    {
        bool find = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "Footer")
            {
                find = true;
            }
        }

        if (find)
        {
            SceneLoader.ChangeScene(SceneLoader.MakeQueue(SceneName), 0f, 0f, SceneLoader.MakeList("Footer"));
        }
        else
        {
            SceneLoader.ChangeScene(SceneLoader.MakeQueue("Footer",SceneName), 0f, 0f, SceneLoader.MakeList("Footer"));
        }
    }
}
