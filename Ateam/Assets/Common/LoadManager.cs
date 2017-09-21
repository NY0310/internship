using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

    public Fade fade;

    bool loading = false;

    Queue<string> unloadList = new Queue<string>();
    Queue<string> loadList = new Queue<string>();
    List<string> ignorList = new List<string>();
    List<AsyncOperation> async = new List<AsyncOperation>();
    bool isAdd = true;

    void Start()
    {
        Init();
    }
    void Init()
    {
        ignorList.Clear();
        ignorList.Add("Common");
        isAdd = true;
        loading = false;
        async.Clear();
    }

    void StartFade()
    {
        fade.In(0.3f, StartLoad);
    }

    void StartLoad()
    {
        loading = true;
        // 重いロードの時は、ここに特定の画像を描画開始する処理とか書いたりする

        if (!isAdd)
        {
            unloadList.Clear();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                string name = SceneManager.GetSceneAt(i).name;
                foreach (string ignor in ignorList)
                {
                    if (name == ignor)
                    {
                        break;
                    }
                }
                unloadList.Enqueue(name);
            }
        }
        while (unloadList.Count > 0)
        {
            SceneManager.UnloadSceneAsync(unloadList.Dequeue());
        }
        //unloadが終わってから↓を呼び出すように
        while (loadList.Count > 0)
        {
            SceneManager.LoadSceneAsync(loadList.Dequeue(), LoadSceneMode.Additive);
        }
    }

    void Update()
    {
        //ここでロードの経過を観察、キューに何かが入れば随時更新するようにする。その時、isAddフラグが折れるかどうかも
    }

}
