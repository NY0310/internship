using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵クラス
/// </summary>
public class Enemy : MonoBehaviour
{
    //[SerializeField]
    //public GameObject enemy;
    [SerializeField]
    public GameObject HpPrefab;


    int HP = 300;
    int Attack =200;
    public int _Attack
    {
        get { return Attack; }
        set { Attack = value; }

    }

    //GameObject ButtlegameObject;


    // Use this for initialization
    void Start () {
        HpPrefab = Instantiate(HpPrefab);
        HpPrefab.GetComponent<HpBar>().transform.position = new Vector3(0, 2, 0);

    }

    // Update is called once per frame
    void Update () {
		
	}


    /// <summary>
    /// HPからダメージをうける
    /// </summary>
    /// <param name="damage"></param>
    public void HitDamage(int damage)
    {
        HP -= damage;
    }


}
