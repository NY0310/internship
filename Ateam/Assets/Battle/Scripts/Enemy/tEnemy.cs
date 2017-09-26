using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tEnemy : MonoBehaviour {

    public delegate void LockOnEvent(tEnemy enemy);
    LockOnEvent lockOnEvent;

    public HP hp;
    public float MaxHP;
    public tDrop.Type type;
    public float attackPower;
    public Image image;

    public EnemyAttackEffect CircleEffect;
    public EnemyAttackEffect CrossEffect;
    public EnemyAttackEffect TryangleEffect;

    void Awake()
    {
        hp.Init(MaxHP);
    }

    BattleManager battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    float attackDelay;

    public void Attack()
    {
        attackDelay = Random.Range(0.4f,1.4f);
    }

    public void Init(LockOnEvent lockOnEvent)
    {
        this.lockOnEvent = lockOnEvent;
    }
    public void ClickedEvent()
    {
        lockOnEvent(this);
    }

    public float Damaged(float power, tDrop.Type damagedType)
    {
        float scale = 1f;
        if (damagedType == type || damagedType == tDrop.Type.All)
        {
            scale = 3f;
            power *= 2f;
        }
        return hp.Damaged(power, damagedType, transform.position + new Vector3(Random.Range(-100f,100f),Random.Range(-100f,100f) - 50f,0f), scale);
    }

    float attackFinishDelay;

    void Update()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
            if (attackDelay <= 0)
            {
                SEPlayer.Play(SE.Name.ENEMY_ATTACK, 0.7f);
                switch (type)
                {
                    case tDrop.Type.Circle:
                        CircleEffect.Effect(4);
                        break;
                    case tDrop.Type.Cross:
                        CrossEffect.Effect(4);
                        break;
                    case tDrop.Type.Tryangle:
                        TryangleEffect.Effect(4);
                        break;
                }
                attackFinishDelay = 1.5f;
            }
        }

        if (attackFinishDelay > 0)
        {
            attackFinishDelay -= Time.deltaTime;
            if (attackFinishDelay <= 0)
            {
                battleManager.PlayerDamaged(attackPower, type);
            }
        }

        if (hp.IsDie()){
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
            if (image.color.a < 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
