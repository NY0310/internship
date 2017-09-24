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
    bool inputed=false;

    enum State
    {
        WAITING_USER_INPUT,
        PLAYER_ATTACK,
        WAITING_ALL_PLAYER_ATTACKS_END,
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
                    PlayerHP.Damaged(150f);
                    enemyAttacked = true;
                }
                break;
        }
    }

    void CheckUnderDropsAreAllSame()
    {
        if (dropLane.DestroyIfUnderDropsAreAllSame())  // 3つ同時消しした時の処理
        {
            playerAttackRemaining.Recover(1f);
            PlayerHP.Recovery(40f);
        }
    }

    float enemyAttackRemainingTime; // 仮
    float playerAttackEffectRemainingTime; // 仮。プレイヤーの攻撃演出が実装されたら消える
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
                playerAttackEffectRemainingTime -= Time.deltaTime;
                if (playerAttackEffectRemainingTime <= 0)
                {
                    ChangeState(State.ENEMY_ATTACK);
                }
                break;
            case State.ENEMY_ATTACK:
                enemyAttackRemainingTime -= Time.deltaTime;
                if (enemyAttackRemainingTime <= 0)
                {
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
        }
    }

}
