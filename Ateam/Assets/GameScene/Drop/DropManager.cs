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

        
         for (int i = 0; i < MAX_RANE; i++)
         {
            //前回のドロップ種類
            Drop.DROPTYPE OldDropType = Drop.DROPTYPE.MAX; 
            Drop.DROPTYPE DropType = Drop.DROPTYPE.MAX;
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

     /// <summary>
     /// 全てのレーンのターゲットドロップが同じ種類なら全削除する
     /// </summary>
     /// <returns>全削除したかどうか</returns>
     public bool IfNeeded()
     {
         Drop.DROPTYPE droptype = DropRaneList[(int)DropRane.LANEKIND.LANE1].GetComponent<DropRane>()._TargetDrop;
        Drop.DROPTYPE _droptype;
         for (int i = 0; i < MAX_RANE; i++)
         {
             _droptype = DropRaneList[i].GetComponent<DropRane>()._TargetDrop;


             if (droptype != _droptype)
             {
                 break;
             }
            else
            {
                 //全て同じドロップなら
                 if (i == MAX_RANE - 1)
                 {
                     //全てのターゲットドロップ削除
                     for (int j = 0; j < MAX_RANE; j++)
                     {
                        DropRaneList[j].GetComponent<DropRane>().TargetDelete();
                     }
                     return true;
                 }

            }
             droptype = _droptype;

         }

         return false;
     }

}
