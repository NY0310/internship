using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ドロップをレーンごとに管理するクラス
/// </summary>
public class DropRane : MonoBehaviour
{

    [SerializeField]
    BottonManager buttanManager;

    //ターゲットドロップの座標
    [SerializeField]
    Vector3 TargetPosition;
    //ドロップの縦間隔
    [SerializeField]
    Vector3 INTERVAL_SIZE;
    //列に入るドロップの最大値
    [SerializeField]
    int MAX_DROP = 4;

    //移動プレハブ
    [SerializeField]
    GameObject MovePregab;

    //各種ドロップのプレハブ
    [SerializeField]
    GameObject CircleDropPrefab;
    [SerializeField]
    GameObject CrossDropPrefab;
    [SerializeField]
    GameObject TryangleDropPrefab;
    [SerializeField]
    GameObject RainbowDrop;
    [SerializeField]
    GameObject CircleRestraintDrop;
    [SerializeField]
    GameObject CrossRestraintDrop;
    [SerializeField]
    GameObject TryangleRestraintDrop;
    [SerializeField]
    GameObject DarknessDrop;
    [SerializeField]
    GameObject CircleRebellionDrop;
    [SerializeField]
    GameObject CrossRebellionDrop;
    [SerializeField]
    GameObject TryangleRebellionDrop;


    //レーンに入ってるドロップリスト
    List<GameObject> DropList = new List<GameObject>();
    public List<GameObject> _DropList
    {
        get { return DropList; }
        set { _DropList = value; }

    }

    //どのレーンか
    LANEKIND LaneKind;

    GameObject ButtlegameObject;


    void Update()
    {
        if (DropList[(int)DropNumber.FIRST].transform.position.y == TargetPosition.y - INTERVAL_SIZE.y / 2)
        {
            Create(Drop.Abnormality.Normal);

            Destroy(DropList[(int)DropNumber.FIRST]);
            DropList.RemoveAt((int)DropNumber.FIRST);

        }
        IsRestraintRelease();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="lanekind">このレーンの種類</param>
    /// <param name="droptype">ドロップの種類</param>
    public void Init(LANEKIND lanekind, Drop.DROPTYPE droptype)
    {
        ButtlegameObject = GameObject.Find("ButtleManager");

        //レーンを保存
        LaneKind = lanekind;

        Vector2 Pos = TargetPosition;
        Pos.x += (float)lanekind * INTERVAL_SIZE.x;
        for (int i = 0; i < MAX_DROP; i++)
        {
            GameObject inst;
            if (i == 0)
            {
                inst = Create(droptype, Drop.Abnormality.Normal);
            }
            else
            {
                inst = Create(Drop.Abnormality.Normal);
            }
            //座標設定
            inst.transform.position = new Vector3(Pos.x, Pos.y, INTERVAL_SIZE.z * i);
            Pos.y += INTERVAL_SIZE.y;

        }
    }

    /// <summary>
    /// 指定されたドロップの種類に応じてプレハブ生成
    /// </summary>
    /// <param name="droptype">ドロップの種類</param>
    /// <returns>ドロップのGameObject</returns>
    public GameObject DropCreate(Drop.DROPTYPE droptype ,Drop.Abnormality Abnormality)
    {
        GameObject DropPrefab = null;



        switch (droptype)
        {
            case Drop.DROPTYPE.Circle:
                switch (Abnormality)
                {
                    case Drop.Abnormality.Normal:
                        DropPrefab = Instantiate(CircleDropPrefab);
                        break;
                    case Drop.Abnormality.Restraint:
                        DropPrefab = Instantiate(CircleRestraintDrop);
                        break;
                    case Drop.Abnormality.Darkness:
                        DropPrefab = Instantiate(DarknessDrop);
                        break;
                    case Drop.Abnormality.Rebellion:
                        DropPrefab = Instantiate(CircleRebellionDrop);
                        break;
                    default:
                        break;
                }
          

                DropPrefab.GetComponent<Drop>()._DropType = droptype;
                return DropPrefab;
            case Drop.DROPTYPE.Cross:
                switch (Abnormality)
                {
                    case Drop.Abnormality.Normal:
                        DropPrefab = Instantiate(CrossDropPrefab);
                        break;
                    case Drop.Abnormality.Restraint:
                        DropPrefab = Instantiate(CrossRestraintDrop);
                        break;
                    case Drop.Abnormality.Darkness:
                        DropPrefab = Instantiate(DarknessDrop);
                        break;
                    case Drop.Abnormality.Rebellion:
                        DropPrefab = Instantiate(CrossRebellionDrop);
                        break;
                    default:
                        break;
                }
    
                DropPrefab.GetComponent<Drop>()._DropType = droptype;
                return DropPrefab;
            case Drop.DROPTYPE.Tryangle:
                switch (Abnormality)
                {
                    case Drop.Abnormality.Normal:
                        DropPrefab = Instantiate(TryangleDropPrefab);
                        break;
                    case Drop.Abnormality.Restraint:
                        DropPrefab = Instantiate(TryangleRestraintDrop);
                        break;
                    case Drop.Abnormality.Darkness:
                        DropPrefab = Instantiate(DarknessDrop);
                        break;
                    case Drop.Abnormality.Rebellion:
                        DropPrefab = Instantiate(TryangleRebellionDrop);
                        break;
                    default:
                        break;
                }
     
                DropPrefab.GetComponent<Drop>()._DropType = droptype;
                return DropPrefab;
            case Drop.DROPTYPE.Rainbow:
                DropPrefab = Instantiate(RainbowDrop);
                DropPrefab.GetComponent<Drop>()._DropType = droptype;
                return DropPrefab;
            default:
                return null;
        }
    }

    /// <summary>
    /// 指定されたドロップの種類に応じてプレハブ生成
    /// </summary>
    /// <param name="droptype">ドロップの種類</param>
    /// <returns>ドロップのGameObject</returns>
    //public GameObject RestraintDropCreate(Drop.DROPTYPE droptype)
    //{
    //    GameObject DropPrefab;

    //    switch (droptype)
    //    {
    //        case Drop.DROPTYPE.Circle:
    //            DropPrefab = Instantiate(CircleRestraintDrop);
    //            DropPrefab.GetComponent<Drop>()._DropType = droptype;
    //            return DropPrefab;
    //        case Drop.DROPTYPE.Cross:
    //            DropPrefab = Instantiate(CrossRestraintDrop);
    //            DropPrefab.GetComponent<Drop>()._DropType = droptype;
    //            return DropPrefab;
    //        case Drop.DROPTYPE.Tryangle:
    //            DropPrefab = Instantiate(TryangleRestraintDrop);
    //            DropPrefab.GetComponent<Drop>()._DropType = droptype;
    //            return DropPrefab;
    //        default:
    //            return null;
    //    }
    //}

    /// <summary>
    /// ターゲットドロップの種類が一致しているか調べる
    /// </summary>
    /// <param name="droptype">一致しているか調べる種類</param>
    /// <returns>一致しているかどうか</returns>
    public bool TargetDropSearch(Drop.DROPTYPE droptype)
    {
        if (DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._DropType == droptype)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// 指定したドロップを生成するメゾットを呼びだしリストに格納
    /// </summary>
    /// <param name="droptype">ドロップの種類</param>
    /// <returns>ドロップのGameObject</returns>
    public GameObject Create(Drop.DROPTYPE droptype , Drop.Abnormality Abnormality)
    {
        GameObject inst;
        inst = DropCreate(droptype , Abnormality);
        //Vector3 position = inst.transform.position;
        Vector3 position = new Vector3(TargetPosition.x + (float)LaneKind * INTERVAL_SIZE.x, TargetPosition.y + INTERVAL_SIZE.y * (MAX_DROP - 1), transform.position.z);
        //position *= 25;
        inst.transform.position = position;
        DropList.Add(inst);
        return inst;
    }


    /// <summary>
    /// ランダムで指定したドロップを生成するメゾットを呼びだし末尾リストに格納
    /// </summary>
    /// <returns>ドロップのGameObject</returns>
    public GameObject Create(Drop.Abnormality Abnormality)
    {
        GameObject inst;
        inst = DropCreate((Drop.DROPTYPE)Random.Range((float)Drop.DROPTYPE.Circle, (float)Drop.DROPTYPE.Tryangle + 1), Abnormality);
        inst.transform.Translate(TargetPosition.x + (float)LaneKind * INTERVAL_SIZE.x, TargetPosition.y + INTERVAL_SIZE.y * (MAX_DROP - 1), 0);
        DropList.Add(inst);
        return inst;
    }


    /// <summary>
    /// 指定したドロップを生成するメゾットを呼びだしリストの指定した要素に格納
    /// </summary>
    /// <param name="droptype">ドロップの種類</param>
    /// <returns>ドロップのGameObject</returns>
    public GameObject Create(Drop.DROPTYPE droptype, int ListNumber , Drop.Abnormality Abnormality)
    {
        GameObject inst;

        Vector3 position = DropList[ListNumber].GetComponent<Drop>().transform.position;
        Destroy(DropList[ListNumber].gameObject);
        // DropList.Remove(DropList[ListNumber]);
        inst = DropCreate(droptype, Abnormality);
        //Vector3 position = new Vector3(TargetPosition.x + (float)LaneKind * INTERVAL_SIZE.x, TargetPosition.y + INTERVAL_SIZE.y * (MAX_DROP - ListNumber - 1), transform.position.z);
        //position *= 25;
        inst.GetComponent<Drop>().transform.position = position;
        DropList[ListNumber] = inst;
        return inst;
    }

    /// <summary>
    /// ランダムなドロップを生成するメゾットを呼びだしリストの指定した要素に格納
    /// </summary>
    /// <param name="droptype">ドロップの種類</param>
    /// <returns>ドロップのGameObject</returns>
    public GameObject Create(int ListNumber, Drop.Abnormality Abnormality)
    {
        GameObject inst;

        Vector3 position = DropList[ListNumber].GetComponent<Drop>().transform.position;
        Destroy(DropList[ListNumber].gameObject);
        inst = DropCreate((Drop.DROPTYPE)Random.Range((float)Drop.DROPTYPE.Circle, (float)Drop.DROPTYPE.Tryangle + 1), Abnormality);
        inst.GetComponent<Drop>().transform.position = position;
        DropList[ListNumber] = inst;
        return inst;
    }


    /// <summary>
    /// ターゲットドロップの確定削除
    /// </summary>
    public void TargetDelete()
    {

        if (ButtlegameObject.GetComponent<ButtleManager>()._IsTouch == true)
        {
            //削除さる前にい
            AllDropDown();

        }
    }


    /// <summary>
    /// ターゲットのドロップが引数と同じ種類なら削除
    /// </summary>
    /// <param name="droptype"></param>
    /// <returns>成功か失敗か</returns>
    public bool TargetDelete(Drop.DROPTYPE droptype)
    {
        Drop.DROPTYPE DropType = DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._DropType;
        Drop.Abnormality Abnormality = DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._Abnormality;
        if ((DropType == droptype)|| (DropType == Drop.DROPTYPE.Rainbow) && (Abnormality != Drop.Abnormality.Rebellion))
        {
            TargetDelete();
            return true;
        }
        else if ((DropType == droptype) || (DropType == Drop.DROPTYPE.Rainbow))
        {
            if (Abnormality == Drop.Abnormality.Rebellion)
            {
                if (DropList[(int)DropNumber.FIRST].GetComponent<RebellionDrop>()._DisplayNumber == 0)
                {
                    TargetDelete();
                    return true;
                }
                else
                {
                    DropList[(int)DropNumber.FIRST].GetComponent<RebellionDrop>()._DisplayNumber--;
                }

            }
        }
         return false;
    }

    bool MoveFlag;
    public bool _MoveFlag
    {
        get { return MoveFlag; }
        set { MoveFlag = value; }

    }
    /// <summary>
    /// 全てのドロップを一段落とす
    /// </summary>
    public void AllDropDown()
    {
        // int loopcnt = 0;
        MoveFlag = false;

        foreach (GameObject dropdata in DropList)
        {
            if (dropdata.GetComponent<Drop>()._MoveGameObject == null && !MoveFlag)
            {
                dropdata.GetComponent<Drop>()._MoveGameObject = Instantiate(MovePregab);
                dropdata.GetComponent<Drop>()._MoveGameObject.GetComponent<Move>().MoveBy(dropdata, new Vector3(0, -INTERVAL_SIZE.y / 2, 0), 5);

            }
            else
            {
                MoveFlag = true;
            }
        }
    }

    /// <summary>
    /// 束縛終了したら元に戻す
    /// </summary>
    void IsRestraintRelease()
    {
        int loopcnt = 0;
        foreach (var item in DropList)
        {
            if (item.GetComponent<Drop>()._Abnormality == Drop.Abnormality.Restraint)
            {
                if (buttanManager._TouchCnt >  item.GetComponent<RestraintDrop>()._MaxRestraint)
                {
                    Create(item.GetComponent<RestraintDrop>()._DropType, loopcnt, Drop.Abnormality.Normal);
                }
            }
            loopcnt++;
        }
    }

    /// <summary>
    /// 月夜の竹取(かぐや姫)
    /// </summary>
    public void MoonlightBambooTaking()
    {
        bool loopFirst = true;
        int loopCnt = 0;
        Drop.DROPTYPE DropType = Drop.DROPTYPE.MAX;
        foreach (var item in DropList)
        {
            if (loopFirst)
            {
                DropType = item.GetComponent<Drop>()._DropType;
                loopFirst = false;
            }
            else
            {
                if (item.GetComponent<Drop>()._DropType != DropType)
                {
                    Vector3 Position = item.transform.position;
                    //Destroy(item);
                    Create(DropType, loopCnt, Drop.Abnormality.Normal);
                    //  NewDrop.transform.position = Position;
                }

            }
            loopCnt++;
        }
    }




    /// <summary>
    /// ターゲットドロップの種類プロパティ
    /// </summary>
    public Drop.DROPTYPE _TargetDrop
    {
        get { return DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._DropType; }
        set { DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._DropType = value; }
    }

    ///// <summary>
    ///// 一番上のドロップの種類プロパティ
    ///// </summary>
    //public Drop.DROPTYPE _BackDrop
    //{
    //    get { return DropList[(int)DropNumber.FOURTH].GetComponent<Drop>()._DropType; }
    //    set { DropList[(int)DropNumber.FOURTH].GetComponent<Drop>()._DropType = value; }
    //}


    //列の種類
    public enum LANEKIND
    {
        LANE1,
        LANE2,
        LANE3
    }

    //列の番号
    public enum DropNumber
    {
        FIRST,
        SECOND,
        THIRD,
        FOURTH,
        MAX
    }


}
