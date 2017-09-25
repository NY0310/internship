using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 0.何か１つでもロードリストに来る(ロードするシーンと、残すシーンを渡す)
/// 1.フェードアウト
/// 1.5 ロード画面の表示
/// 2.アンロード
/// 2.5 アンロードチェック
/// 3.ロード
/// 3.5.全てのロード完了チェック
/// 4.フェードイン
/// </summary>
/// 

public class SceneLoader
{

    public static Queue<string> MakeQueue(params string[] loadList)
    {
        Queue<string> result = new Queue<string>();
        foreach (var load in loadList)
        {
            result.Enqueue(load);
        }
        return result;
    }
    public static List<string> MakeList(params string[] ignorList)
    {
        List<string> result = new List<string>();
        foreach (var ignor in ignorList)
        {
            result.Add(ignor);
        }
        return result;
    }

    public static bool ChangeScene(Queue<string> loadList, float fadeOutTime, float fadeInTime, List<string> ignorList = null)
    {
        var obj = GameObject.Find("Load");
        if (obj == null) return false;
        return obj.GetComponent<LoadManager>().ChangeScene(loadList, fadeOutTime, fadeInTime, ignorList);
    }
}

public class LoadManager : MonoBehaviour {

    public Fade fade;

    bool doing = false;
    bool loading = false;
    Queue<string> loadList = new Queue<string>();
    List<string> ignorList = new List<string>();
    List<AsyncOperation> asyncs = new List<AsyncOperation>();

    float fadeInTime;
    float fadeOutTime;

    void FirstLoad()
    {
        Init();
        Queue<string> firstLoadList = new Queue<string>();
        firstLoadList.Enqueue("Title");
        ChangeScene(firstLoadList, 0.05f, 1.5f );
    }

    /// <summary>
    /// フェードタイムがどちらも0ならフェードしない
    /// </summary>
    /// <param name="loadList"></param>
    /// <param name="fadeOutTime"></param>
    /// <param name="fadeInTime"></param>
    /// <param name="ignorList"></param>
    /// <returns></returns>
    public bool ChangeScene( Queue<string> loadList, float fadeOutTime, float fadeInTime, List<string> ignorList = null )
    {
        if (doing) return false;
        if (ignorList == null) ignorList = new List<string>();
        Init();
        this.loadList = loadList;
        this.ignorList = ignorList;
        this.fadeInTime = fadeInTime;
        this.fadeOutTime = fadeOutTime;
        ignorList.Add("Common");

        if (fadeOutTime != 0 || fadeInTime != 0)
            fade.Out(fadeOutTime, StartLoad);
        else StartLoad();
        doing = true;
        return true;
    }

    void Init()
    {
        ignorList.Clear();
        loadList.Clear();
        loading = false;
        asyncs.Clear();
    }

    void StartLoad()
    {
        loading = true;
        UnLoad();
        Load();
    }

    void UnLoad()
    {
        Queue<string> unloadList = new Queue<string>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            bool isIgnor = false;
            string name = SceneManager.GetSceneAt(i).name;
            foreach (string ignor in ignorList)
            {
                if (name == ignor)
                {
                    isIgnor = true;
                    break;
                }
            }
            if (!isIgnor)
            {
                unloadList.Enqueue(name);
            }
        }
        while (unloadList.Count > 0)
        {
            asyncs.Add(SceneManager.UnloadSceneAsync(unloadList.Dequeue()));
        }
    }

    void Load()
    {
        while (loadList.Count > 0)
        {
            asyncs.Add(SceneManager.LoadSceneAsync(loadList.Dequeue(), LoadSceneMode.Additive));
        }
    }


    float countToFirstLoad = 3f;
    bool firstLoaded = false;

    void Update()
    {
        if (firstLoaded == false)
        {
            countToFirstLoad -= Time.deltaTime;
            if (countToFirstLoad <= 0)
            {
                FirstLoad();
                firstLoaded = true;
            }
        }

        if (loading)
        {
            bool allDone = true;
            foreach (var async in asyncs)
            {
                if (!async.isDone)
                {
                    allDone = false;
                }
            }
            if( allDone )
            {
                loading = false;
                doing = false;
                if (fadeOutTime != 0 || fadeInTime != 0)
                    fade.In(fadeInTime);
            }
        }
    }

}
