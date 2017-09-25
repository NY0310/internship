using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerSkillManager : MonoBehaviour {

    [SerializeField]
    private UnityEvent skill = new UnityEvent();

    Image image;

    public tDrop.Type type; // 仮。本当はプレイヤーのステータス管理クラスから引っ張ってくるが、今回はそういうのが無いので

    void Start()
    {
        image = GetComponent<Image>();
        switch (type)
        {
            case tDrop.Type.Circle:
                image.color = new Color(0.8f,0.2f,0.2f,1f);
                break;
            case tDrop.Type.Cross:
                image.color = new Color(0.0f, 0.2f, 0.8f, 1f);
                break;
            case tDrop.Type.Tryangle:
                image.color = new Color(0.6f, 0.6f, 0.1f, 1f);
                break;
        }
    }

    public float NeedEnergy = 100f;
    public float ChargedEnergy = 0;

    public void Charge(float charge)
    {
        ChargedEnergy += charge;
        ChargedEnergy = Mathf.Clamp(ChargedEnergy, 0f, NeedEnergy);
    }

    public void Activate()
    {
        if (ChargedEnergy < NeedEnergy) return;
        ChargedEnergy = 0;
        skill.Invoke();
    }

    // Update is called once per frame
    void Update () {
        Charge(0.1f);
        transform.localScale = new Vector3(transform.localScale.x, 0.92f*ChargedEnergy/NeedEnergy, transform.localScale.z);
	}
}
