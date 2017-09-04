﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ドロップをレーンごとに管理するクラス
/// </summary>
public class DropRane : MonoBehaviour{


    //ターゲットドロップの座標
    [SerializeField]
    public Vector2 TargetPosition;
    //ドロップの縦間隔
    [SerializeField]
    public Vector2 INTERVAL_SIZE;
    //列に入るドロップの最大値
    [SerializeField]
    public int MAX_DROP = 4;

    //移動プレハブ
    [SerializeField]
    public GameObject MovePregab;

    //各種ドロップのプレハブ
    [SerializeField]
    public GameObject CircleDropPrefab;
    [SerializeField]
    public GameObject CrossDropPrefab;
    [SerializeField]
    public GameObject TryangleDropPrefab;

    //レーンに入ってるドロップリスト
    List<GameObject> DropList = new List<GameObject>();
    //どのレーンか
    LANEKIND LaneKind;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="lanekind">このレーンの種類</param>
    /// <param name="droptype">ドロップの種類</param>
    public void Init(LANEKIND lanekind, Drop.DROPTYPE droptype)
    {
        //レーンを保存
        LaneKind = lanekind;

        Vector2 Pos = TargetPosition;
         Pos.x += (float)lanekind * INTERVAL_SIZE.x;
        for (int i = 0; i < MAX_DROP; i++)
        {
            GameObject inst;
            if (i == 0)
            {
                inst = Create(droptype);
            }
            else
            {
                inst = Create();
            }
            //座標設定
            inst.transform.position = new Vector3(Pos.x, Pos.y, transform.position.z);
            Pos.y += INTERVAL_SIZE.y;

        }
    }

    /// <summary>
    /// 指定されたドロップの種類に応じてプレハブ生成
    /// </summary>
    /// <param name="droptype">ドロップの種類</param>
    /// <returns>ドロップのGameObject</returns>
    public GameObject DropCreate(Drop.DROPTYPE droptype)
    {
        GameObject DropPrefab;
        switch (droptype)
        {
            case Drop.DROPTYPE.Circle:
                DropPrefab = Instantiate(CircleDropPrefab);
                DropPrefab.GetComponent<Drop>()._DropType = droptype;
                return DropPrefab;
            case Drop.DROPTYPE.Cross:
                DropPrefab = Instantiate(CrossDropPrefab);
                DropPrefab.GetComponent<Drop>()._DropType = droptype;
                return DropPrefab;
            case Drop.DROPTYPE.Tryangle:
                DropPrefab = Instantiate(TryangleDropPrefab);
                DropPrefab.GetComponent<Drop>()._DropType = droptype;
                return DropPrefab;
            default:
                return null;
        }
    }




    /// <summary>
    /// 指定したドロップを生成するメゾットを呼びだしリストに格納
    /// </summary>
    /// <param name="droptype">ドロップの種類</param>
    /// <returns>ドロップのGameObject</returns>
    public GameObject Create(Drop.DROPTYPE droptype)
    {
        GameObject inst;
        inst = DropCreate(droptype);
        inst.transform.Translate(TargetPosition.x + (float)LaneKind * INTERVAL_SIZE.x, TargetPosition.y + INTERVAL_SIZE.y * (MAX_DROP - 1), transform.position.z);
        DropList.Add(inst);
        return inst;
    }


    /// <summary>
    /// ランダムで指定したドロップを生成するメゾットを呼びだしリストに格納
    /// </summary>
    /// <returns>ドロップのGameObject</returns>
    public GameObject Create()
    {
        GameObject inst;
        inst = DropCreate((Drop.DROPTYPE)Random.Range((float)Drop.DROPTYPE.Circle, (float)Drop.DROPTYPE.Tryangle + 1));
        inst.transform.Translate(TargetPosition.x + (float)LaneKind * INTERVAL_SIZE.x, TargetPosition.y + INTERVAL_SIZE.y  * (MAX_DROP), transform.position.z);
        DropList.Add(inst);
        return inst;
    }

    /// <summary>
    /// ターゲットドロップの確定削除
    /// </summary>
    public void TargetDelete()
    {
            GameObject ButtlegameObject = GameObject.Find("FightManager");

            if (ButtlegameObject.GetComponent<FightManager>()._IsTouch == true)
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
        if (DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._DropType == droptype)
        {
            TargetDelete();
            return true;
        }
        return false;
    }


    /// <summary>
    /// 全てのドロップを一段落とす
    /// </summary>
    public void AllDropDown()
    {
        // int loopcnt = 0;
        bool MoveFlag = false;
        if (DropList[(int)DropNumber.FIRST].transform.position.y == TargetPosition.y)
        {
            Create();

            Destroy(DropList[(int)DropNumber.FIRST]);
            DropList.RemoveAt((int)DropNumber.FIRST);

        }
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
    /// ターゲットドロップの種類プロパティ
    /// </summary>
    public Drop.DROPTYPE _TargetDrop
    {
        get { return DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._DropType; }
        set { DropList[(int)DropNumber.FIRST].GetComponent<Drop>()._DropType = value; }
    }


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
