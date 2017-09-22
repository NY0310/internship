using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーからの入力処理と
/// それに伴うゲーム全体の進行を行う
/// </summary>
public class BattleManager : MonoBehaviour {

    public tDropLaneBase dropLane;
    public HP PlayerHP;
    public PlayerAttackRemaining playerAttackRemaining;
    enum State
    {
        WAIT,
        PLAYER_ATTACK,
        ENEMY_ATTACK
    }
    State state;

    const float PLAYER_ATTACK_TIME = 5.0f;


    ///　/////　　　仮　　　/////////////
    void Start()
    {
        PlayerHP.Init(500f);    
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
    void UserInput(tDrop.Type type)
    {
        if (state == State.WAIT)
        {
            ChangeState(State.PLAYER_ATTACK);
        }
        if (state == State.PLAYER_ATTACK)
        {
            int destroyNum = dropLane.DestroyUnderDrop(type);
            if (destroyNum == 2)
            {
                playerAttackRemaining.Recover(0.3f);
            }
            else if (destroyNum == 3)
            {
                playerAttackRemaining.Recover(2f);
            }
        }
    }


    /// ///////////////////////////////////////////////////    更新処理    //////////////////////////////////////////////////////

    bool enemyAttacked = false; // 仮
    void Update()
    {
        UpdateState();
        switch (state)
        {
            case State.WAIT:
                break;
            case State.PLAYER_ATTACK:
                UpdatePlayerAttack();
                break;
            case State.ENEMY_ATTACK:
                if (enemyAttackRemainingTime < 0.8f && !enemyAttacked)
                {
                    PlayerHP.Damaged(150f);
                    enemyAttacked = true;
                }
                break;
        }
    }

    void UpdatePlayerAttack()
    {
        if (dropLane.DestroyIfUnderDropsAreSame())  // 3つ同時消しした時の処理
        {
            playerAttackRemaining.Recover(2f);
            PlayerHP.Recovery(40f);
        }
    }

    float enemyAttackRemainingTime; // 仮
    void UpdateState()
    {
        switch (state)
        {
            case State.WAIT:
                break;
            case State.PLAYER_ATTACK:
                if (playerAttackRemaining.IsFinished())
                {
                    ChangeState(State.ENEMY_ATTACK);
                }
                break;
            case State.ENEMY_ATTACK:
                enemyAttackRemainingTime -= Time.deltaTime;
                if (enemyAttackRemainingTime <= 0)
                {
                    ChangeState(State.WAIT);
                }
                break;
        }
    }

    void ChangeState(State state)
    {
        switch (state)
        {
            case State.WAIT:
                this.state = State.WAIT;
                break;
            case State.PLAYER_ATTACK:
                playerAttackRemaining.Restart(PLAYER_ATTACK_TIME);
                this.state = State.PLAYER_ATTACK;
                break;
            case State.ENEMY_ATTACK:
                enemyAttacked = false;
                enemyAttackRemainingTime = 2.5f;
                this.state = State.ENEMY_ATTACK;
                break;
        }
    }

}
