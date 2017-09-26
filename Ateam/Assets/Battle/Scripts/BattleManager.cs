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
    bool inputed=false;

    public GameObject Players;
    List<PlayerSkillManager> playerSkill;

    public GameObject enemy_1;// 仮
    float attackPowerBase = 1f; // 仮変数
    float[] attackPower= new float[4]; // 仮変数

    int wave=0;

    List<UpParm> attackUp = new List<UpParm>();
    List<UpParm> damagedUp = new List<UpParm>();

    enum State
    {
        WAITING_USER_INPUT,
        PLAYER_ATTACK,
        WAITING_ALL_PLAYER_ATTACKS_END,
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


    const float PLAYER_ATTACK_TIME = 5.0f;


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
            attackPower[(int)type] += Mathf.Pow(2.3f,destroyNum)*attackPowerBase;
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
            case State.ENEMY_ATTACK:
                CheckUnderDropsAreAllSame();
                if (enemyAttackRemainingTime < 0.8f && !enemyAttacked)
                {
                    float rate = 1f;
                    foreach (var damaged in damagedUp)
                    {
                        rate *= damaged.rate;
                    }
                    SEPlayer.Play(SE.Name.DAMAGED, 0.42f);
                    PlayerHP.Damaged( enemyManager.GetAttackPower()*rate );
                    enemyAttacked = true;
                }
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
            attackPower[type] += Mathf.Pow(2.3f, 3) * attackPowerBase;
            foreach (var skill in playerSkill)
            {
                if ((int)skill.type == type || type == (int)tDrop.Type.All)
                {
                    skill.Charge(1f);
                }
            }
        }
    }

    float enemyAttackRemainingTime; // 仮
    float playerAttackEffectRemainingTime; // 仮。プレイヤーの攻撃演出が実装されたら消える
    float nextWaveRemainingTime;
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
                    float rate = 1f;
                    foreach (var attack in attackUp)
                    {
                        rate *= attack.rate;
                    }
                    enemyManager.Damaged(attackPower, rate);
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
                if ( nextWaveRemainingTime <= 0 )
                {
                    wave++;
                    if (wave < CurrentStageData.Data.enemyList.Count)
                    {
                        foreach (var enemy in CurrentStageData.Data.enemyList[wave].List)
                        {
                            enemyManager.SpawnEnemy(enemy);
                        }
                        if (wave == CurrentStageData.Data.enemyList.Count - 1)
                            BGMPlayer.Play(BGM.Name.BOSS);
                        ChangeState(State.WAITING_USER_INPUT);
                    }
                    else
                    {
                        ChangeState(State.CLEAR);
                    }
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
            case State.ENEMY_ATTACK:
                enemyAttacked = false;
                enemyAttackRemainingTime = 3.5f;
                this.state = State.ENEMY_ATTACK;
                break;
            case State.NEXT_WAVE:
                nextWaveRemainingTime = 1f;
                this.state = State.NEXT_WAVE;
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
