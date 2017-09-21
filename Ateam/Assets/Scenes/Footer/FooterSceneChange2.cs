using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FooterSceneChange2 : MonoBehaviour {

    public string SceneName;
    static string currentSceneName = "";

    public void ClickedEvent()
    {
        if (currentSceneName == SceneName) return;
        if (currentSceneName != "")
            SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        currentSceneName = SceneName;
    }
}
