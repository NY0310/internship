using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    public GameObject Enemy1;
    [SerializeField]
    public GameObject Enemy2;
    [SerializeField]
    public GameObject Enemy3;


    //敵のリスト
    List<Enemy> EnemyList = new List<Enemy>();

    // Use this for initialization
    void Start () {
        ////初期化
  
        //プレハブ生成
        EnemyCreate(Enemy1);
        EnemyCreate(Enemy2);
        EnemyCreate(Enemy3);
    }

    /// <summary>
    /// 敵生成
    /// </summary>
    /// <param name="gameObject"></param>
    void EnemyCreate(GameObject gameObject)
    {
        GameObject EnemyPrefab;
        //敵生成
        EnemyPrefab = Instantiate(gameObject);
        //リストに追加
        EnemyList.Add(EnemyPrefab.GetComponent<Enemy>());


 ///       int asd =  EnemyList.Count;

    }

    // Update is called once per frame
    void Update () {

        if (EnemyList != null)
        {
            EnemyDelete();

        }



    }

  

    /// <summary>
    /// 体力がない敵は削除
    /// </summary>
    void EnemyDelete()
    {
        foreach (var item in EnemyList)
        {
            if (item._IsDestory == true)
            {
                Destroy(item.gameObject);
                EnemyList.RemoveAt(0);
                break;
            }
           
        }
    }

    /// <summary>
    /// プレイヤのダメージを受ける   
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="droptype"></param>
    public void HitDamage(List<Player.AttackData> AttackDataList)
    {

        int TargetCnt = 0;
        foreach (var list in AttackDataList)
        {
            if (EnemyList != null)
            {
                foreach (var item in EnemyList)
                {
                    if (item._HP != 0)
                    {
                        EnemyList[TargetCnt].HitDamage(list.ToatalAttack, list.droptype);
                        break;

                    }
                }

                
            }

        }
      
        
    }


    /// <summary>
    /// 敵の攻撃リストを取得する
    /// </summary>
    /// <returns></returns>
    public List<int> GetAttack()
    {
        List<int> attacklist = new List<int>();
        foreach (Enemy List in EnemyList)
        {
            attacklist.Add(List._Attack);
        }
        return attacklist;
    }

}
