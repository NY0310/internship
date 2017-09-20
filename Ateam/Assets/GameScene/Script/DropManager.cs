using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		
	}

    const int MAX_RANE = 3;
    List<GameObject> DropRaneList = new List<GameObject>();
    /// <summary>
    /// 全てのレーンを総括するクラス
    /// </summary>
    /// <summary>
    /// 全てのレーンを生成
    /// </summary>
    /// 


    // Use this for initialization
    void Start()
    {
        Init();
    }

    public GameObject DropRanePrefab;

    /// <summary>
    /// 初期化処理
    /// </summary>
     public void Init()
     {

            //前回のドロップ種類
            Drop.DROPTYPE OldDropType = Drop.DROPTYPE.MAX; 
        
            Drop.DROPTYPE DropType = Drop.DROPTYPE.MAX;
         for (int i = 0; i < MAX_RANE; i++)
         {
            DropType = ((Drop.DROPTYPE)Random.Range((float)Drop.DROPTYPE.Circle, (float)Drop.DROPTYPE.Tryangle + 1));

            while (OldDropType == DropType)
            {
              DropType = ((Drop.DROPTYPE)Random.Range((float)Drop.DROPTYPE.Circle, (float)Drop.DROPTYPE.Tryangle + 1));
                
            }
              GameObject inst;
              inst = Instantiate(DropRanePrefab);
              DropRaneList.Add(inst);
              DropRaneList[i].GetComponent<DropRane>().Init((DropRane.LANEKIND)i, DropType);
              OldDropType = DropType;

         }
    }



     /// <summary>
     /// 全てのレーンにドロップを生成
     /// </summary>
     public void Create()
     {
         for (int i = 0; i < MAX_RANE; i++)
         {
         DropRaneList[i].GetComponent<DropRane>().Create();
         }
     }

     /// <summary>
     /// 全てのレーンのターゲットドロップ確定削除
     /// </summary>
     public void TargetDelete()
     {
         for (int i = 0; i < MAX_RANE; i++)
         {
              DropRaneList[i].GetComponent<DropRane>().TargetDelete();
         }
     }

    /// <summary>
    /// 全てのレーンはターゲットドロップが引数と同じ種類なら削除
    /// </summary>
    /// <param name="droptype"></param>
    /// <returns>成功した回数</returns>
    public int TargetSearch(Drop.DROPTYPE droptype)
    {
        //成功した回数
        int success = 0;
        foreach (var list in DropRaneList)
        {
            if (list.GetComponent<DropRane>().TargetDropSearch(droptype))
                success++;
        }
        

        return success;

    }


    /// <summary>
    /// 全てのレーンはターゲットドロップが引数と同じ種類なら削除
    /// </summary>
    /// <param name="droptype"></param>
    /// <returns>成功した回数</returns>
    public int TargetDelete(Drop.DROPTYPE droptype)
     {
         //成功した回数
         int successTimes = 0;
         bool IsSuccess;
         for (int i = 0; i < MAX_RANE; i++)
         {
             IsSuccess = DropRaneList[i].GetComponent<DropRane>().TargetDelete(droptype);

         if (IsSuccess)
           successTimes++;
           IsSuccess = false;
         }

         return successTimes;

     }

    ///// <summary>
    ///// 全てのレーンのドロップを一段落とす
    ///// </summary>
    //public void AllDropDown()
    //{
    //    for (int i = 0; i < MAX_RANE; i++)
    //    {
    //          DropRaneList[i].GetComponent<DropRane>().AllDropDown();
    //    }
    //}


    public struct IfNeededData
    {
        public  bool MooveFlag;
        public  Drop.DROPTYPE Droptype;
    }
    /// <summary>
    /// 全てのレーンのターゲットドロップが同じ種類なら全削除する
    /// </summary>
    /// <returns>全削除したドロップ種類</returns>
    public IfNeededData IfNeeded()
     {
        IfNeededData IfNeededData;


        Drop.DROPTYPE droptype = Drop.DROPTYPE.MAX;
        foreach (var list in DropRaneList)
        {
            droptype = list.GetComponent<DropRane>()._TargetDrop;
            if (droptype != Drop.DROPTYPE.Rainbow)
            {
                break;
            }
        }

      //   Drop.DROPTYPE droptype = DropRaneList[(int)DropRane.LANEKIND.LANE1].GetComponent<DropRane>()._TargetDrop;
        Drop.DROPTYPE _droptype;
         for (int i = 0; i < MAX_RANE; i++)
         {
             _droptype = DropRaneList[i].GetComponent<DropRane>()._TargetDrop;

           
             if ((droptype == _droptype) || (_droptype == Drop.DROPTYPE.Rainbow))
            {
                 //全て同じドロップなら
                 if (i == MAX_RANE - 1)
                 {
                     //全てのターゲットドロップ削除
                     for (int j = 0; j < MAX_RANE; j++)
                     {
                        DropRaneList[j].GetComponent<DropRane>().TargetDelete();
                     }
                     IfNeededData.MooveFlag = DropRaneList[(int)DropRane.DropNumber.FIRST].GetComponent<DropRane>()._MoveFlag;
                     IfNeededData.Droptype = droptype;
                     return IfNeededData;
                 }

            }
             else
            {
                break;
            }
           
         }
        IfNeededData.MooveFlag = false;
        IfNeededData.Droptype = Drop.DROPTYPE.MAX;
         return IfNeededData;
     }

    /// <summary>
    /// シンデレラのスキル(UglyMagic)
    /// </summary>
    public void UglyMagic()
    {
        int loopCnt =(int)Drop.DROPTYPE.MAX;
        foreach (var list in DropRaneList)
        {
            //List<GameObject> droplist = list.GetComponent<DropRane>()._DropList;
            ////Vector3 Position =  droplist[(int)DropRane.DropNumber.FOURTH - loopCnt].GetComponent<Drop>().transform.position;
            //Destroy(droplist[(int)DropRane.DropNumber.FOURTH - loopCnt]);
            //droplist.RemoveAt((int)DropRane.DropNumber.FOURTH - loopCnt);
            GameObject drop = list.GetComponent<DropRane>().Create(Drop.DROPTYPE.Rainbow , loopCnt);
            //drop.transform.position = Position;
            loopCnt--;
        }
    }


    /// <summary>
    /// 月夜の竹取(かぐや姫)
    /// </summary>
    public void MoonlightBambooTaking()
    {
        DropRaneList[(int)DropRane.LANEKIND.LANE2].GetComponent<DropRane>().MoonlightBambooTaking();
    }



}
