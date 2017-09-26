using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ユーザーからの入力処理と
/// それに伴うゲーム全体の進行を行う
/// </summary>

public class UpParm
{
    public UpParm() { }
    public UpParm(int turnNum, float rate)
    {
        this.turnNum = turnNum;
        this.rate = rate;
    }
    public int turnNum;
    public float rate;
}
public class BattleManager : MonoBehaviour {

    public tDropLaneBase dropLane;
    public HP PlayerHP;
    public PlayerAttackRemaining playerAttackRemaining;
    public tEnemyManager enemyManager;

    public AttackButtonEffect TryangleEffect;
    public AttackButtonEffect CircleEffect;
    public AttackButtonEffect CrossEffect;
    public AttackEffect TryangleAttackEffect;
    public AttackEffect CircleAttackEffect;
    public AttackEffect CrossAttackEffect;
    bool inputed=false;

    public GameObject Players;
    List<PlayerSkillManager> playerSkill;

    public GameObject enemy_1;// 仮
    float attackPowerBase = 30f; // 仮変数
    float[] attackPower= new float[4]; // 仮変数

    int wave=0;

    List<UpParm> attackUp = new List<UpParm>();
    List<UpParm> damagedUp = new List<UpParm>();

    enum State
    {
        WAITING_USER_INPUT,
        PLAYER_ATTACK,
        WAITING_ALL_PLAYER_ATTACKS_END,
        ENEMY_DAMAGING,
        ENEMY_ATTACK,
        NEXT_WAVE,
        CLEAR,
        GAME_OVER
    }
    State state;
    public bool IsPlayerTurn()
    {
        return ( state == State.PLAYER_ATTACK || state == State.WAITING_USER_INPUT);
    }
    public bool IsEnemyTurn()
    {
        return (state == State.ENEMY_ATTACK);
    }


    const float PLAYER_ATTACK_TIME = 7.0f;


    ///　/////　　　仮　　　/////////////
    void Start()
    {
        Camera.main.GetComponent<CameraController>().MoveTo(new Vector3(0f, 9f, 1.62f),0.075f);
        Camera.main.GetComponent<CameraController>().RotateTo(new Vector3(30f,0f,0f), 0.055f);

        playerSkill = new List<PlayerSkillManager>();
        foreach (var skill in Players.GetComponentsInChildren<PlayerSkillManager>())
        {
            playerSkill.Add(skill);
        }

        PlayerHP.Init(500f);
        foreach (var enemy in CurrentStageData.Data.enemyList[0].List)
        {
            enemyManager.SpawnEnemy(enemy);
        }
    }

    void ButtonEffect(tDrop.Type type, int level, float power)
    {
        switch (type)
        {
            case tDrop.Type.Tryangle:
                TryangleEffect.Effect(level);
                TryangleEffect.AddPower(power);
                break;
            case tDrop.Type.Cross:
                CrossEffect.Effect(level);
                CrossEffect.AddPower(power);
                break;
            case tDrop.Type.Circle:
                CircleEffect.Effect(level);
                CircleEffect.AddPower(power);
                break;
            case tDrop.Type.All:
                TryangleEffect.Effect(level);
                CrossEffect.Effect(level);
                CircleEffect.Effect(level);
                break;
        }
    }

    /// /////////////////////////////////////////////////////    ユーザー入力処理    /////////////////////////////////////////////

    /// <summary>
    /// なぜTap～という関数を３つも作ったか
    /// →UnityのOnClickでenumが使えなかったから。
    /// intで渡してもよかったが、変換したり、値チェックするより
    /// こっちが楽だと判断したため
    /// </summary>
    public void TapCross()
    {
        UserInput(tDrop.Type.Cross);
    }
    public void TapCircle()
    {
        UserInput(tDrop.Type.Circle);
    }
    public void TapTryangle()
    {
        UserInput(tDrop.Type.Tryangle);
    }
    public void UserInput(tDrop.Type type)
    {
        if (inputed == true) return;
        inputed = true;
        if (state == State.WAITING_USER_INPUT)
        {
            ChangeState(State.PLAYER_ATTACK);
        }
        if (state == State.PLAYER_ATTACK)
        {
            int destroyNum = dropLane.DestroyUnderDrop(type);
            if (destroyNum == 0) return;
            float addPower = (Mathf.Pow(2.3f, destroyNum-1)) * attackPowerBase;
            attackPower[(int)type] += addPower;
            ButtonEffect(type,destroyNum,addPower);
        }
    }


    public void AttackUp(int turnNum, float rate)
    {
        attackUp.Add(new UpParm(turnNum, rate));
    }
    public void DamagedUp(int turnNum, float rate)
    {
        damagedUp.Add(new UpParm(turnNum, rate));
    }

    /// ///////////////////////////////////////////////////    更新処理    //////////////////////////////////////////////////////

    public void PlayerDamaged(float power, tDrop.Type type)
    {
        float rate = 1f;
        foreach (var damaged in damagedUp)
        {
            rate *= damaged.rate;
        }
        SEPlayer.Play(SE.Name.DAMAGED, 0.42f);
        Vector3 pos = PlayerHP.transform.position;
        PlayerHP.Damaged(power * rate, type, new Vector3(pos.x + Random.Range(200f,500f), pos.y, pos.z), 3f);
    }

    bool enemyAttacked = false; // 仮
    void Update()
    {
        inputed = false;
        UpdateState();
        switch (state)
        {
            case State.WAITING_USER_INPUT:
                CheckUnderDropsAreAllSame();
                break;
            case State.PLAYER_ATTACK:
                CheckUnderDropsAreAllSame();
                break;
            case State.WAITING_ALL_PLAYER_ATTACKS_END:
                CheckUnderDropsAreAllSame();
                break;
            case State.ENEMY_DAMAGING:
                break;
            case State.ENEMY_ATTACK:
                break;
            case State.NEXT_WAVE:
                break;
            case State.CLEAR:
                break;
            case State.GAME_OVER:
                break;
        }
    }

    void CheckUnderDropsAreAllSame()
    {
        int type = dropLane.DestroyIfUnderDropsAreAllSame();
        if (type!=-1)  // 3つ同時消しした時の処理
        {
            playerAttackRemaining.Recover(0.4f);
            PlayerHP.Recovery(10f);
            float addPower = Mathf.Pow(2.3f, 2) * attackPowerBase;

            if (type == (int)tDrop.Type.All)
            {
                attackPower[0] += addPower;
                attackPower[1] += addPower;
                attackPower[2] += addPower;
            }
            else
            {
                attackPower[type] += addPower;
            }

            foreach (var skill in playerSkill)
            {
                if ((int)skill.type == type || type == (int)tDrop.Type.All)
                {
                    skill.Charge(1f);
                }
            }
            ButtonEffect((tDrop.Type)type,3,addPower);
        }
    }

    float enemyAttackRemainingTime; // 仮
    float playerAttackEffectRemainingTime; // 仮。プレイヤーの攻撃演出が実装されたら消える
    float nextWaveRemainingTime;
    float enemyDamagingRemainingTime;

    float tryangleAttackTiming;
    float circleAttackTiming;
    float crossAttackTiming;
    bool tryangleAttackStartEffected = false;
    bool circleAttackStartEffected = false;
    bool crossAttackStartEffected = false;
    bool enemyDamagedTryangle = false;
    bool enemyDamagedCircle = false;
    bool enemyDamagedCross = false;

    float GetPower(int i)
    {
        float rate = 1f;
        foreach (var attack in attackUp)
        {
            rate *= attack.rate;
        }
        return attackPower[i] * rate;
    }

    void UpdateState()
    {
        switch (state)
        {
            case State.WAITING_USER_INPUT:
                break;
            case State.PLAYER_ATTACK:
                if (playerAttackRemaining.IsFinished())
                {
                    ChangeState(State.WAITING_ALL_PLAYER_ATTACKS_END);
                }
                break;
            case State.WAITING_ALL_PLAYER_ATTACKS_END:
                if( !dropLane.IsThereMovingDrop() )
                    playerAttackEffectRemainingTime -= Time.deltaTime;
                if (playerAttackEffectRemainingTime <= 0)
                {
                    ChangeState(State.ENEMY_DAMAGING);
                }
                break;
            case State.ENEMY_DAMAGING:
                enemyDamagingRemainingTime -= Time.deltaTime;

                if (enemyDamagingRemainingTime <= tryangleAttackTiming && !tryangleAttackStartEffected)
                {
                    SEPlayer.Play( SE.Name.ATTACK, 0.65f);
                    tryangleAttackStartEffected = true;
                    TryangleEffect.Effect(4);
                    TryangleEffect.Attack();
                }
                if (enemyDamagingRemainingTime <= circleAttackTiming && !circleAttackStartEffected)
                {
                    SEPlayer.Play(SE.Name.ATTACK, 0.65f);
                    circleAttackStartEffected = true;
                    CircleEffect.Effect(4);
                    CircleEffect.Attack();
                }
                if (enemyDamagingRemainingTime <= crossAttackTiming && !crossAttackStartEffected)
                {
                    SEPlayer.Play(SE.Name.ATTACK, 0.65f);
                    crossAttackStartEffected = true;
                    CrossEffect.Effect(4);
                    CrossEffect.Attack();
                }

                if (enemyDamagingRemainingTime <= tryangleAttackTiming -1f && !enemyDamagedTryangle)
                {
                    enemyDamagedTryangle = true;
                    TryangleAttackEffect.EffectIn(4,enemyManager.TargetX() + Random.Range(-20f, 20f));
                    enemyManager.Damaged(GetPower(2),tDrop.Type.Tryangle);
                }
                if (enemyDamagingRemainingTime <= circleAttackTiming -1f && !enemyDamagedCircle)
                {
                    enemyDamagedCircle = true;
                    CircleAttackEffect.EffectIn(4, enemyManager.TargetX() + Random.Range(30f, enemyManager.TargetRange()));
                    enemyManager.Damaged(GetPower(1), tDrop.Type.Circle);
                }
                if (enemyDamagingRemainingTime <= crossAttackTiming -1f && !enemyDamagedCross)
                {
                    enemyDamagedCross = true;
                    CrossAttackEffect.EffectIn(4, enemyManager.TargetX() - Random.Range(30f, enemyManager.TargetRange()));
                    enemyManager.Damaged(GetPower(0), tDrop.Type.Cross);
                }

                if (enemyDamagingRemainingTime <= 0)
                {
                    if (enemyManager.CheckAllDie())
                    {
                        ChangeState(State.NEXT_WAVE);
                    }
                    else
                    {
                        ChangeState(State.ENEMY_ATTACK);
                    }
                }
                break;
            case State.ENEMY_ATTACK:
                enemyAttackRemainingTime -= Time.deltaTime;
                if (enemyAttackRemainingTime <= 0)
                {
                    if (PlayerHP.IsDie())
                    {
                        ChangeState(State.GAME_OVER);
                    }
                    else
                    {
                        ChangeState(State.WAITING_USER_INPUT);
                    }
                }
                break;
            case State.NEXT_WAVE:
                nextWaveRemainingTime -= Time.deltaTime;
                if (nextWaveRemainingTime <= 1.0f)
                {
                    enemyManager.transform.localPosition = new Vector3(0f, enemyManager.transform.localPosition.y + ( 600f - enemyManager.transform.localPosition.y)*0.2f, 0f);
                }
                if ( nextWaveRemainingTime <= 0 )
                {
                    enemyManager.transform.localPosition = new Vector3(0f, 600f, 0f);
                    ChangeState(State.WAITING_USER_INPUT);
                }
                break;
        }
    }

    void ChangeState(State state)
    {
        switch (state)
        {
            case State.WAITING_USER_INPUT:
                for (int i=attackUp.Count-1;i>=0;i--)
                {
                    attackUp[i].turnNum--;
                    if (attackUp[i].turnNum <= 0)
                        attackUp.Remove(attackUp[i]);
                }
                for (int i = damagedUp.Count - 1; i >= 0; i--)
                {
                    damagedUp[i].turnNum--;
                    if (damagedUp[i].turnNum <= 0)
                        damagedUp.Remove(damagedUp[i]);
                }
                this.state = State.WAITING_USER_INPUT;
                break;
            case State.PLAYER_ATTACK:
                playerAttackRemaining.Restart(PLAYER_ATTACK_TIME);
                for(int i = 0; i<attackPower.Length; i++) {
                    attackPower[i] = 0f;
                }
                this.state = State.PLAYER_ATTACK;
                break;
            case State.WAITING_ALL_PLAYER_ATTACKS_END:
                playerAttackEffectRemainingTime = 1f;
                this.state = State.WAITING_ALL_PLAYER_ATTACKS_END;
                break;
            case State.ENEMY_DAMAGING:
                enemyDamagingRemainingTime = 3f;
                float one = 0.15f;
                float space = 0.05f;
                int select = Random.Range(0, 6);
                if (select == 0)
                {
                    tryangleAttackTiming = enemyDamagingRemainingTime - Random.Range(0f, one);
                    circleAttackTiming = enemyDamagingRemainingTime - Random.Range(one+space, one*2+space);
                    crossAttackTiming = enemyDamagingRemainingTime - Random.Range(one*2+space*2, one*3+space*2);
                }
                if (select == 1)
                {
                    tryangleAttackTiming = enemyDamagingRemainingTime - Random.Range(0f, one);
                    crossAttackTiming = enemyDamagingRemainingTime - Random.Range(one + space, one * 2 + space);
                    circleAttackTiming = enemyDamagingRemainingTime - Random.Range(one * 2 + space * 2, one * 3 + space * 2);
                }
                if (select == 2)
                {
                    crossAttackTiming = enemyDamagingRemainingTime - Random.Range(0f, one);
                    tryangleAttackTiming = enemyDamagingRemainingTime - Random.Range(one + space, one * 2 + space);
                    circleAttackTiming = enemyDamagingRemainingTime - Random.Range(one * 2 + space * 2, one * 3 + space * 2);
                }
                if (select == 3)
                {
                    crossAttackTiming = enemyDamagingRemainingTime - Random.Range(0f, one);
                    circleAttackTiming = enemyDamagingRemainingTime - Random.Range(one + space, one * 2 + space);
                    tryangleAttackTiming = enemyDamagingRemainingTime - Random.Range(one * 2 + space * 2, one * 3 + space * 2);
                }
                if (select == 4)
                {
                    circleAttackTiming = enemyDamagingRemainingTime - Random.Range(0f, one);
                    tryangleAttackTiming = enemyDamagingRemainingTime - Random.Range(one + space, one * 2 + space);
                    crossAttackTiming = enemyDamagingRemainingTime - Random.Range(one * 2 + space * 2, one * 3 + space * 2);
                }
                if (select == 5)
                {
                    circleAttackTiming = enemyDamagingRemainingTime - Random.Range(0f, one);
                    crossAttackTiming = enemyDamagingRemainingTime - Random.Range(one + space, one * 2 + space);
                    tryangleAttackTiming = enemyDamagingRemainingTime - Random.Range(one * 2 + space * 2, one * 3 + space * 2);
                }
                tryangleAttackStartEffected = false;
                circleAttackStartEffected = false;
                crossAttackStartEffected = false;
                enemyDamagedTryangle = false;
                enemyDamagedCircle = false;
                enemyDamagedCross = false;
                this.state = State.ENEMY_DAMAGING;
                break;
            case State.ENEMY_ATTACK:
                enemyAttacked = false;
                enemyAttackRemainingTime = 3.5f;
                enemyManager.AttackEffect();
                this.state = State.ENEMY_ATTACK;
                break;
            case State.NEXT_WAVE:
                nextWaveRemainingTime = 2f;
                enemyManager.transform.localPosition = new Vector3(0f,1200f,0f);
                this.state = State.NEXT_WAVE;

                wave++;
                if (wave < CurrentStageData.Data.enemyList.Count)
                {
                    foreach (var enemy in CurrentStageData.Data.enemyList[wave].List)
                    {
                        enemyManager.SpawnEnemy(enemy);
                    }
                    if (wave == CurrentStageData.Data.enemyList.Count - 1)
                        BGMPlayer.Play(BGM.Name.BOSS);
                }
                else
                {
                    ChangeState(State.CLEAR);
                }

                break;
            case State.CLEAR:
                BGMPlayer.Play(BGM.Name.CLEAR);
                SceneManager.LoadScene("ResultSuccess", LoadSceneMode.Additive);
                this.state = State.CLEAR;
                break;
            case State.GAME_OVER:
                BGMPlayer.Play(BGM.Name.GAME_OVER);
                SceneManager.LoadScene("ResultFaild", LoadSceneMode.Additive);
                this.state = State.GAME_OVER;
                break;
        }
    }

}
