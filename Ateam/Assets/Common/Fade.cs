using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Imageのa値を使ってフェードイン、フェードアウトを関数呼び出しだけで行うクラス
/// </summary>
public class Fade : MonoBehaviour {

    public delegate void FinishedEvent(); // フェードイン、アウト終了時に呼ばれる関数
    FinishedEvent finishedEvent;

    Image image;

    float remainingTime;    // フェードイン、アウトの残り時間
    float addAmout;         // 現在のフェードイン、アウトでの1フレームあたりの、a値の増加量
    bool doing = false;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 外部から呼び出す、フェードイン開始関数
    /// </summary>
    /// <param name="time">何秒かけてフェードインするか</param>
    /// <param name="finishedEvent">終了時に呼ばれる関数</param>
    public void In(float time, FinishedEvent finishedEvent=null)
    {
        image.raycastTarget = false;
        this.finishedEvent = finishedEvent;
        remainingTime = time;
        addAmout = -image.color.a / time;
        doing = true;
    }

    /// <summary>
    /// 外部から呼び出す、フェードアウト開始関数
    /// </summary>
    /// <param name="time">何秒かけてフェードアウトするか</param>
    /// <param name="finishedEvent">終了時に呼ばれる関数</param>
    public void Out(float time, FinishedEvent finishedEvent=null)
    {
        image.raycastTarget = true;
        this.finishedEvent = finishedEvent;
        remainingTime = time;
        addAmout = (1.0f - image.color.a) / time;
        doing = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (doing)
        {
            UpdateAlpha();
            UpdateRemaining();
        }
    }

    /// <summary>
    /// 実際にフェードイン、アウトをさせる更新処理
    /// </summary>
    void UpdateAlpha()
    {
        float newAlpha = image.color.a + addAmout * Time.deltaTime;
        newAlpha = Mathf.Clamp(newAlpha, 0.0f, 1.0f);
        image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
    }

    /// <summary>
    /// 残り時間を更新、0になっていたら終了処理
    /// </summary>
    void UpdateRemaining()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0.0f)
        {
            if(finishedEvent != null)
                finishedEvent();
            doing = false;
        }
    }
}
