using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour {

    float remaining;
    public float MaxRemaining;
    public float Recover;
    float defaultHeight;
    RectTransform rect;

    BattleManager battleManager;

	void Start () {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        rect = GetComponent<RectTransform>();
        defaultHeight = rect.sizeDelta.y;
        remaining = MaxRemaining;
	}

    void AddRemaining(float add)
    {
        remaining += add;
        remaining = Mathf.Clamp(remaining, 0f, MaxRemaining);
    }

    void SEPlay()
    {
        // SEPlayer.Play(SE.Name.BUTTON, 0.05f);
    }

    public void TapCross()
    {
        if (battleManager.IsPlayerTurn() && remaining >= 1f)
        {
            SEPlay();
            battleManager.TapCross();
            AddRemaining(-1f);
        }
    }
    public void TapCircle()
    {
        if (battleManager.IsPlayerTurn() && remaining >= 1f)
        {
            SEPlay();
            battleManager.TapCircle();
            AddRemaining(-1f);
        }
    }
    public void TapTryangle()
    {
        if (battleManager.IsPlayerTurn() && remaining >= 1f)
        {
            SEPlay();
            battleManager.TapTryangle();
            AddRemaining(-1f);
        }
    }

    void Update () {
        AddRemaining(Recover*Time.deltaTime);
        rect.sizeDelta = new Vector2( rect.sizeDelta.x, defaultHeight * remaining / MaxRemaining);
	}
}
