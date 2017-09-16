using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    //[SerializeField]
    //public GameObject Enemy1;
    //[SerializeField]
    //public GameObject Enemy2;
    //[SerializeField]
    //public GameObject Enemy3;


    //敵のリスト
    List<Enemy> EnemyList = new List<Enemy>();
    public List<Enemy> _EnemyList
    {
        get { return EnemyList; }
    }

    //////攻撃回数
    //int TargetCnt = 0;
    //
    public struct HpAnimationData
    {
        public HpBar Hp;
        public float NewHp;
    }
    HpAnimationData hpAnimationData;


    // Use this for initialization
    void Start () {
        ////初期化
  
        //プレハブ生成
        //EnemyCreate(Enemy1);
        //EnemyCreate(Enemy2);
        //EnemyCreate(Enemy3);
    }

    /// <summary>
    /// 敵生成
    /// </summary>
    /// <param name="gameObject"></param>
    public void EnemyCreate(List<GameObject> gameObject)
    {
        //敵リストをクリア
        EnemyList.Clear();

        GameObject Enemy;
        foreach (var item in gameObject)
        {
            Enemy = Instantiate(item);
            EnemyList.Add(Enemy.GetComponent<Enemy>());
        }


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
    public List<HpAnimationData> HitDamage(List<Player.AttackData> AttackDataList)
    {
        List<HpAnimationData> HpAnimationDataList = new List<HpAnimationData>();

        foreach (var list in AttackDataList)
        {
            if (EnemyList != null)
            {
                foreach (var item in EnemyList)
                {
                    if (item._HP != 0)
                    {
                        hpAnimationData.NewHp = item.HitDamage(list.ToatalAttack, list.droptype);
                        hpAnimationData.Hp = item._HpPrefab.GetComponent<HpBar>();
                        if (list.ToatalAttack > 0) 
                        {
                            HpAnimationDataList.Add(hpAnimationData);
                        }
                        break;

                    }
                }
            }

        }
        return HpAnimationDataList;
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

    /// <summary>
    /// HPのアニメーションが全て終了しているか
    /// </summary>
    /// <returns>終了しているか</returns>
    //public bool GetIsMove()
    //{
    //    foreach (var item in EnemyList)
    //    {
    //        if (item.GetComponent<Enemy>()._HpPrefab.GetComponent<HpBar>()._IsNowStop)
    //            TargetCnt--;
    //    }

    //    if (TargetCnt == 0)
    //    {
    //        return true;

    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    

    //void a()
    //{

    //    TargetCnt--;
    //    foreach (var item in EnemyList)
    //    {

    //    }
    //}
}
