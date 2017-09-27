using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour {

    Image image;
    public BattleManager battleManager;

    float Y;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        Y = transform.position.y;
        image.color = new Color(1f, 1f, 1f, 0f);
    }

    float count;

	// Update is called once per frame
	void Update () {

        count += Time.deltaTime;
        if (battleManager.enemyManager.targeted != null)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            image.transform.position = new Vector3(battleManager.enemyManager.TargetX(), Y + Mathf.Sin(count * 2f) * 10f, 0f);
        }
        else
        {
            image.color = new Color(1f,1f,1f,0f);
        }
	}
}
