using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /// <summary>
    /// 攻撃に属性
    /// </summary>

    
    //攻撃力
    int Attack;

    //体力
    int HP;


    //HPのプロパティ
    public int _HP
    {
        get { return HP; }
        set { HP = value; }
    }

    //ドロップ属性
    protected Drop.DROPTYPE DropType;
    //ドロップ属性のプロパティ
    public Drop.DROPTYPE _DropType
    {
        get { return DropType; }
        set { DropType = value; }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// HPからダメージをうける
    /// </summary>
    /// <param name="damage"></param>
    void HitDamage(int damage)
    {
        HP -= damage;
    }

   
}
