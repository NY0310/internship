using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ドロップクラス(基底)
/// </summary>
public  class Drop : MonoBehaviour {

    
    //ドロップ種類
    public enum DROPTYPE
    {
        Circle,
        Cross,
        Tryangle,
        MAX,
        Rainbow
    }

    //状態異常
    public enum Abnormality
    {
        Normal,
        Restraint,
        Darkness,
        Rebellion
    }


    DROPTYPE DropType;

    public DROPTYPE _DropType
    {
        get { return DropType; }
        set { DropType = value; }
    }

    Abnormality abnormality;
    public Abnormality _Abnormality
    {
        get { return abnormality; }
        set { abnormality = value; }
    }

    GameObject MoveGameObject;

    public GameObject _MoveGameObject
    {
        get { return MoveGameObject; }
        set { MoveGameObject = value; }
    }


    bool IsDeleate;


    public bool _IsDeleate
    {
        get { return IsDeleate; }
        set { IsDeleate = value; }

    }


}


