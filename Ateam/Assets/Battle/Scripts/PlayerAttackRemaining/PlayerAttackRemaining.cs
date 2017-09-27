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

    float stoppingTime=0f;
    public void Stop(float stoppingTime)
    {
        this.stoppingTime = stoppingTime;
    }

    public bool IsFinished()
    {
        return remainingTime <= 0f;
    }

    void Update()
    {
        if (stoppingTime <= 0)
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
        else
        {
            stoppingTime -= Time.deltaTime;
            text.text = remainingTime.ToString("#0.00");
            if (remainingTime <= 0)
            {
                text.text = "";
            }
        }
    }
}
