using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ユーザーからの入力処理と
/// それに伴うゲーム全体の進行を行う
/// </summary>
public class BattleManager : MonoBehaviour {

    public tDropLaneBase dropLane;
    public HP PlayerHP;
    public PlayerAttackRemaining playerAttackRemaining;
    public tEnemyManager enemyManager;
    bool inputed=false;

    public GameObject enemy_1;// 仮
    float attackPowerBase = 1f; // 仮変数
    float[] attackPower= new float[4]; // 仮変数

    int wave=0;

    enum State
    {
        WAITING_USER_INPUT,
        PLAYER_ATTACK,
        WAITING_ALL_PLAYER_ATTACKS_END,
        ENEMY_ATTACK,
        NEXT_WAVE
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
        Camera.main.GetComponent<CameraController>().MoveTo(new Vector3(0f, 9.68f, 1.62f),0.075f);
        Camera.main.GetComponent<CameraController>().RotateTo(new Vector3(30f,0f,0f), 0.055f);

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
            if(destroyNum==3) playerAttackRemaining.Recover(1f);
            attackPower[(int)type] += Mathf.Pow(2.3f,destroyNum)*attackPowerBase;
        }
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
                    PlayerHP.Damaged( enemyManager.GetAttackPower() );
                    enemyAttacked = true;
                }
                break;
            case State.NEXT_WAVE:
                break;
        }
    }

    void CheckUnderDropsAreAllSame()
    {
        int type = dropLane.DestroyIfUnderDropsAreAllSame();
        if (type!=-1)  // 3つ同時消しした時の処理
        {
            playerAttackRemaining.Recover(1f);
            PlayerHP.Recovery(40f);
            attackPower[type] += Mathf.Pow(2.3f, 3) * attackPowerBase;
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
                    enemyManager.Damaged(attackPower);
                    ChangeState(State.WAITING_ALL_PLAYER_ATTACKS_END);
                }
                break;
            case State.WAITING_ALL_PLAYER_ATTACKS_END:
                playerAttackEffectRemainingTime -= Time.deltaTime;
                if (playerAttackEffectRemainingTime <= 0)
                {
                    if (enemyManager.AllDie)
                    {
                        // ここで、次の敵データが無い時クリアになるように
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
                    ChangeState(State.WAITING_USER_INPUT);
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
                    }
                    else
                    {
                        SceneManager.LoadScene("ResultSuccess",LoadSceneMode.Additive);
                        
                    }
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
                playerAttackEffectRemainingTime = 2.5f;
                this.state = State.WAITING_ALL_PLAYER_ATTACKS_END;
                break;
            case State.ENEMY_ATTACK:
                enemyAttacked = false;
                enemyAttackRemainingTime = 0.5f;
                this.state = State.ENEMY_ATTACK;
                break;
            case State.NEXT_WAVE:
                nextWaveRemainingTime = 1f;
                this.state = State.NEXT_WAVE;
                break;
        }
    }

}
