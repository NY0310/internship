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
        EnemyDelete();

        //if (EnemyList.Count == 0)
        //{
        //    int dag = 0;
        //}
        //else if (EnemyList.Count == 1)
        //{
        //    int ad = 0;
        //}
        //else
        //{
        //    int adgaga = 0;
        //}
        //List<Player.AttackData> data = new List<Player.AttackData>();
        //Player.AttackData a;
        //a.ToatalAttack = 10;
        //a.droptype = Drop.DROPTYPE.Circle;
        //data.Add(a);
        //HitDamage(data);
    }

  

    /// <summary>
    /// 体力がない敵は削除
    /// </summary>
    void EnemyDelete()
    {
        //int cnt = 0;
        //foreach (var list in EnemyList)
        //{
        //    if (list._HP == 0)
        //    {
        //        Destroy(list);
        //        EnemyList.RemoveAt(cnt);
        //    }
        //    cnt++;
        //}
    }

    /// <summary>
    /// プレイヤのダメージを受ける   
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="droptype"></param>
    public void HitDamage(List<Player.AttackData> AttackDataList)
    {
        foreach (var list in AttackDataList)
        {
            if (EnemyList.Count != 0)
            {
                EnemyList[0].HitDamage(list.ToatalAttack, list.droptype);
                if (EnemyList[0]._HP == 0)
                {
                    Destroy(EnemyList[0]);
                    EnemyList.RemoveAt(0);
                }
                break;
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
