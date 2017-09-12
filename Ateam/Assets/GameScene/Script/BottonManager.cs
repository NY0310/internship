using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottonManager : MonoBehaviour {

    // 各ボタンのプレハブ
    [SerializeField]
    public GameObject CircleBotton;
    [SerializeField]
    public GameObject CrossBotton;
    [SerializeField]
    public GameObject TryangleBotton;

    List<GameObject> BottonList = new List<GameObject>();


    //押しているボタン(MAXは何も触っていないものとする)
    Drop.DROPTYPE PushBotton = Drop.DROPTYPE.MAX;
    public Drop.DROPTYPE _PushBotton
    {
        get { return PushBotton; }
        set { PushBotton = value; }
    }


    // Use this for initialization
    void Start () {
        //各ボタンの生成
        CircleBotton = Instantiate(CircleBotton);
        BottonList.Add(CircleBotton);
        CrossBotton = Instantiate(CrossBotton);
        BottonList.Add(CrossBotton);
        TryangleBotton = Instantiate(TryangleBotton);
        BottonList.Add(TryangleBotton);
        PushBotton = Drop.DROPTYPE.MAX;
    }

    // Update is called once per frame
    void Update () {

        Drop.DROPTYPE cnt = (Drop.DROPTYPE)0;
        PushBotton = Drop.DROPTYPE.MAX;

        foreach (var list in BottonList)
        {
            if (list.GetComponent<Botton>()._IsCollision)
            {
                if (cnt == Drop.DROPTYPE.Circle)
                {
                    PushBotton = Drop.DROPTYPE.Circle;

                }
                else if (cnt == Drop.DROPTYPE.Cross)
                {
                    PushBotton = Drop.DROPTYPE.Cross;
                }
                else if(cnt == Drop.DROPTYPE.Tryangle)
                {
                    PushBotton = Drop.DROPTYPE.Tryangle;
                }
            }
          

            
            cnt++;
        }
	}
}
