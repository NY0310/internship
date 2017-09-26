using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dropの動き、入力の受け付けを決めるなど、DropLaneの基本となる動き。
/// スキルなどの効果用の処理が入ってない状態のもの
/// </summary>
public class tDropLaneBase : MonoBehaviour {

    public GameObject DropCross;
    public GameObject DropCirce;
    public GameObject DropTryangle;
    public GameObject DropAll;

    public GameObject[] Lanes = new GameObject[3];
    
    const float SPACE_BETWEEN_DROPS = 2.3f;     // DropとDropの縦の間隔
    const float DROP_START_Z        = -2.75f;   // 一番上のドロップの位置

    public const int LANE_NUM = 3;
    public const int DROP_NUM_ON_LANE = 4;
    public const int UNDER_DROP = DROP_NUM_ON_LANE - 1;

    tDrop[,] drops = new tDrop[LANE_NUM, DROP_NUM_ON_LANE];
    public tDrop[,] Drops { get{ return drops;  } }

    void Start()
    {
        // 初回だけ、最初から３つ揃っている自体を防ぐために生成しておく
        MakeDrop(tDrop.Type.Circle, 0, 0);
        MakeDrop(tDrop.Type.Cross, 1, 0);
        MakeDrop(tDrop.Type.Tryangle, 2, 0);
    }

    /// ////////////////////////////////////////////    更新処理    /////////////////////////////////////////////////////////
    void Update()
    {
        DropPositionUpdate();
    }

    void DropPositionUpdate()
    {
        for (int L = 0; L < LANE_NUM; L++)
        {
            for (int D = DROP_NUM_ON_LANE - 2; D >= 0; D--) // 一番下の要素から判定することで、１フレームで全要素を正しく落下させる
            {
                if (drops[L, D + 1] == null)                // 下が空いていたら（ドロップが存在してない場合）
                    MoveDownDrop(L, D);                     // 落下する（位置を詰める）
            }
            MakeTopDrop_IfDoesNotExist(L);                  // 一番上が空いてたらランダム生成する
        }
    }

    /// <summary>
    /// 存在するDropなら、落下させる
    /// </summary>
    /// <param name="L">レーン</param>
    /// <param name="D">ドロップ</param>
    void MoveDownDrop(int L, int D)
    {
        if (drops[L, D] == null) return;
        drops[L, D + 1] = drops[L, D];  // 下の要素に移す
        drops[L, D + 1].MoveToTargetZ(GetTargetZ(D+1));    // 実際に座標も移動させる
        drops[L, D] = null; // 元の場所は空白にしておく
    }

    float GetTargetZ(int D)
    {
        return DROP_START_Z + SPACE_BETWEEN_DROPS * D;
    }

    /// <summary>
    /// 一番上の段にDropが無い場合、ランダムに生成する
    /// </summary>
    /// <param name="L">レーン</param>
    void MakeTopDrop_IfDoesNotExist(int L)
    {
        // 生成アルゴリズム。現在のレーンに少ないドロップ程出やすくなっていく。
        if (drops[L, 0] == null)
        {
            int[] typeNum = new int[3]{0,0,0}; // 自然生成は３種類のみなので３つ固定
            int count = 0;
            for (int D = 1; D < DROP_NUM_ON_LANE; D++)
            {
                if (drops[L, D] == null) continue;
                if (drops[L, D].type == tDrop.Type.Circle) typeNum[0]++;
                if (drops[L, D].type == tDrop.Type.Cross) typeNum[1]++;
                if (drops[L, D].type == tDrop.Type.Tryangle) typeNum[2]++;
                count++;
            }
            float[] rate = new float[3];
            float sumRate = 0;
            for (int i = 0; i < 3; i++)
            {
                rate[i] = count - typeNum[i];
                rate[i] = Mathf.Pow(3,rate[i]);
                sumRate += rate[i];
            }
            float pickUpRate = Random.Range(0, sumRate);
            float sum = 0;
            int pickUp= Random.Range(0, 3);
            for (int i = 0; i < 3; i++)
            {
                sum += rate[i];
                if (sum >= pickUpRate)
                {
                    pickUp = i;
                    break;
                }
            }

            tDrop.Type type;
            switch (pickUp)
            {
                case 0:
                    type = tDrop.Type.Circle;
                    break;
                case 1:
                    type = tDrop.Type.Cross;
                    break;
                case 2:
                    type = tDrop.Type.Tryangle;
                    break;
                default:
                    Debug.Log("DropLane.cs/Update(), ランダム値により存在しないDropTypeを生成しようとしました");
                    type = tDrop.Type.Circle;
                    break;
            }
            MakeDrop(type, L, 0);
        }
    }

    /// <summary>
    /// 元のドロップを置き換えてドロップを生成する
    /// </summary>
    public void MakeDrop(tDrop.Type type, int L, int D)
    {
        DestroyDrop(L,D);
        GameObject dropObj;
        switch (type)
        {
            case tDrop.Type.Circle:
                dropObj = Instantiate(DropCirce, Lanes[L].transform);
                break;
            case tDrop.Type.Cross:
                dropObj = Instantiate(DropCross, Lanes[L].transform);
                break;
            case tDrop.Type.Tryangle:
                dropObj = Instantiate(DropTryangle, Lanes[L].transform);
                break;
            case tDrop.Type.All:
                dropObj = Instantiate(DropAll, Lanes[L].transform);
                break;
            default:
                Debug.Log(type.ToString() + "のタイプの生成処理が書かれていません");
                dropObj = Instantiate(DropCirce, Lanes[L].transform);
                break;
        }
        dropObj.transform.localPosition = new Vector3(0f, 0f, GetTargetZ(D));
        drops[L, D] = dropObj.GetComponent<tDrop>();
    }


    /// ///////////////////////////////////////////////    外部から呼び出す処理    ///////////////////////////////////////////////////

    public void DestroyDrop(int L, int D)
    {
        if (drops[L, D] == null) return;
        // ここに消滅エフェクトを追加
        drops[L, D].DoDestroy();
        // drops[L, D] = null;
    }

    /// <summary>
    /// 外部から呼び出される、ユーザー操作によって一番下の段のドロップを破壊する関数
    /// </summary>
    /// <param name="type">破壊するドロップ</param>
    /// <returns>何個消したか</returns>
    public int DestroyUnderDrop(tDrop.Type type)
    {
        if (IsThereMovingDrop()) return 0;
        int count = 0;
        for (int L = 0; L < LANE_NUM; L++)
        {
            // ここの虹色消したとき、他の色どう判定するか決める。intは何を返す？
            if (drops[L, UNDER_DROP].type == type && isExist(L,UNDER_DROP))
            {
                count++;
                DestroyDrop(L, UNDER_DROP);
            }
        }

        if (count == 0)
            SEPlayer.Play(SE.Name.DROP_BREAK1, 0f, count / 7f + 0.7f);
        else if(count == 1)
            SEPlayer.Play(SE.Name.DROP_BREAK1, 0.25f);
        else if(count == 2)
            SEPlayer.Play(SE.Name.DROP_BREAK2, 0.18f);

        return count;
    }

    bool isExist(int L, int D)
    {
        if (drops[L, D] != null)
        {
            if (!drops[L, D].isDestroying)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// BattleManagerから毎フレーム呼び出され、３つ破壊できた場合はtrueを返す
    /// この関数はこのクラスのUpdate内で呼べばかなり処理を簡略化できるが
    /// ３つ消滅時に攻撃処理するBattleManagerへ通知が必要なこと、変なタイミングで発生させないために
    /// 外部呼出しになっている。
    /// 仮　：　intを返しているのは、３つ消せたかどうかを判定しないといけないから。気持ち悪いので、後で直す
    /// </summary>
    /// <returns>3つ破壊したか</returns>
    public int DestroyIfUnderDropsAreAllSame()
    {
        if (IsThereMovingDrop()) return -1;
        tDrop.Type type;
        // 虹色はどう判定する？
        // nullでなければ
        if (isExist(0, UNDER_DROP) && isExist(1, UNDER_DROP) && isExist(2, UNDER_DROP))
        {
            // 3つとも同じなら
            if (IsSame(0, UNDER_DROP, 1, UNDER_DROP) && IsSame(1, UNDER_DROP, 2, UNDER_DROP))
            {
                type = drops[0, tDropLaneBase.UNDER_DROP].type;
                DestroyDrop(0, UNDER_DROP);
                DestroyDrop(1, UNDER_DROP);
                DestroyDrop(2, UNDER_DROP);
                SEPlayer.Play(SE.Name.DROP_BREAK3, 0.3f);
                return (int)type;
            }
            else if (drops[0, UNDER_DROP].type == tDrop.Type.All || drops[1, UNDER_DROP].type == tDrop.Type.All || drops[2, UNDER_DROP].type == tDrop.Type.All)
            {
                DestroyDrop(0, UNDER_DROP);
                DestroyDrop(1, UNDER_DROP);
                DestroyDrop(2, UNDER_DROP);
                SEPlayer.Play(SE.Name.DROP_BREAK3, 0.3f);
                return (int)tDrop.Type.All;
            }
        }
        return -1;
    }

    bool IsSame(int L1, int D1, int L2, int D2)
    {
        return (drops[L1, D1].type == drops[L2, D2].type);
    }

    /// ///////////////////////////////////////////////////    条件判定    //////////////////////////////////////////////

    /// <summary>
    /// 一つでも動いているドロップがあるかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsThereMovingDrop()
    {
        for (int L = 0; L < LANE_NUM; L++)
        {
            for (int D = DROP_NUM_ON_LANE - 2; D >= 0; D--)
            {
                if (drops[L, D] == null) return true;
                if (drops[L, D].Moving) return true;
            }
        }
        return false;
    }
}
