using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FooterSceneChange : MonoBehaviour {

    public string SceneName;
    static string currentSceneName = "";

    public static void FirstLoad(string sceneName)
    {
        currentSceneName = sceneName;
        SceneManager.LoadScene("Footer", LoadSceneMode.Single);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void ClickedEvent()
    {
        if (currentSceneName == SceneName) return;
        if (currentSceneName != "")
            SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        currentSceneName = SceneName;
    }
}
