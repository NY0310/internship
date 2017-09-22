using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackRemaining : MonoBehaviour {

    float remainingTime;
    public float RemainingTime { get { return remainingTime; } }

    Text text;

    void Start()
    {
        text = GetComponent<Text>();    
    }

    public void Restart(float time)
    {
        remainingTime = time;
    }

    public void Recover(float amount)
    {
        remainingTime += amount;
    }

    public bool IsFinished()
    {
        return remainingTime <= 0f;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            text.text = remainingTime.ToString("#0.00");
            if (remainingTime <= 0)
            {
                text.text = "";
            }
        }
    }
}
