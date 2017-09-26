using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggMove : MonoBehaviour {

    Image image;
    float defaultY;
    BattleManager battleManager;
    public tEnemy enemy;

	// Use this for initialization
	void Start () {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        image = GetComponent<Image>();
        defaultY = image.transform.localPosition.y;
	}

    float count = 0f;
    float dieCount=0f;
    bool dieSounded = false;
	// Update is called once per frame
	void Update () {
        if (enemy.IsDie())
        {
            if (!dieSounded)
            {
                dieSounded = true;
                SEPlayer.Play(SE.Name.ENEMY_DIE, 2.0f);
            }
            dieCount += Time.deltaTime;
            image.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, image.transform.rotation.eulerAngles.z + 1f));
            image.transform.localPosition = new Vector3(image.transform.localPosition.x, defaultY + Mathf.Sin(dieCount*5)*150f, image.transform.localPosition.z);
        }
        else
        {
            if (battleManager.IsEnemyTurn())
            {
                count += Time.deltaTime * Random.Range(1f, 2f);
                image.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Max(Mathf.Sin(-0.75f + count * 6.5f) - 0.9f, 0f) * 20f));
                image.transform.localPosition = new Vector3(image.transform.localPosition.x, defaultY + Mathf.Max(Mathf.Sin(count * 6.5f) - 0.4f, 0f) * 205f, image.transform.localPosition.z);
            }
            else
            {
                count += Time.deltaTime * Random.Range(0.5f, 1.5f);
                image.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Max(Mathf.Sin(-0.75f + count * 5f) - 0.9f, 0f) * 70f));
                image.transform.localPosition = new Vector3(image.transform.localPosition.x, defaultY + Mathf.Max(Mathf.Sin(count * 5f) - 0.9f, 0f) * 235f, image.transform.localPosition.z);
            }
        }
    }
}
