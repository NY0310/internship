using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tEnemyManager : MonoBehaviour {

    List<tEnemy> enemyList = new List<tEnemy>();
    bool allDie = false;
    public bool AllDie { get{ return allDie; } }

    // 攻撃の仮仕様：　ターゲットが居る場合のみ、そいつ単体攻撃。ない場合は単体攻撃なしで、余った攻撃力を均等に分散する
    tEnemy targeted = null;
    public void LockOnEvent(tEnemy enemy)
    {
        targeted = enemy;
    }

    public void SpawnEnemy( GameObject enemy )
    {
        var spawnedEnemy = Instantiate(enemy, this.transform).GetComponent<tEnemy>();
        enemyList.Add(spawnedEnemy.GetComponent<tEnemy>());
        spawnedEnemy.Init( LockOnEvent);
        allDie = false;
    }

    public void AttackEffect()
    {
        foreach (var enemy in enemyList)
        {
            enemy.Attack();
        }
    }

    public float GetAttackPower()
    {
        float sum=0;
        foreach (var enemy in enemyList)
        {
            sum += enemy.attackPower;
        }
        return sum;
    }

    public void Damaged(float power, tDrop.Type type)
    {
        SEPlayer.Play( SE.Name.DAMAGED, 0.42f);
        int num = enemyList.Count;
        if (targeted != null)
        {
            power = targeted.Damaged(power, type);
            if (targeted.hp.IsDie()) num--;
        }

        if (num > 0 && power > 0)
        {
            foreach (var enemy in enemyList)
            {
                if (targeted == enemy) continue;
                enemy.Damaged(power / num, type);
            }
        }
    }

    public bool CheckAllDie()
    {
        allDie = true;
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (enemyList[i] == null)
            {
                enemyList.Remove(enemyList[i]);
            }
            else if (enemyList[i].hp.IsDie())
            {
                enemyList.Remove(enemyList[i]);
            }
            else
            {
                allDie = false;
            }
        }
        return allDie;
    }

    public float TargetX()
    {
        if (targeted == null)
        {
            return Screen.width/2f;
        }
        return targeted.transform.position.x;
    }

    public float TargetRange()
    {
        if (targeted != null || enemyList.Count == 1)
        {
            return 50f;
        }
        return 200f;
    }

	void Update () {
        allDie = true;
        for(int i=enemyList.Count-1; i>=0; i--)
        {
            if (enemyList[i] == null)
            {
                enemyList.Remove(enemyList[i]);
            }
            else if (enemyList[i].hp.IsDie())
            {
                enemyList.Remove(enemyList[i]);
            }
            else
            {
                allDie = false;
            }
        }
	}
}
