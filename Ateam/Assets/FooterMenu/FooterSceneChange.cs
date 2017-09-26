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
            SEPlayer.Play( SE.Name.BOOK, 2.8f);
            SceneLoader.ChangeScene(SceneLoader.MakeQueue(SceneName), BGM.Name.MENU, 0f, 0f, SceneLoader.MakeList("Footer"));
        }
        else
        {
            SceneLoader.ChangeScene(SceneLoader.MakeQueue("Footer",SceneName), BGM.Name.MENU, 1.5f, 1.5f, SceneLoader.MakeList("Footer"));
        }
    }
}
