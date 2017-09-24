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

    void Start()
    {
        hp.Init(MaxHP);    
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
        if (damagedType == type || damagedType == tDrop.Type.All)
        {
            power *= 2f;
        }
        return hp.Damaged(power);
    }

    void Update()
    {
        if (hp.IsDie()){
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
            if (image.color.a < 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
