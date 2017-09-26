using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    float shakeTime;

    Vector3 defaultPos;
    float randomValue;
    public void Shake()
    {
        randomValue = Random.Range(-10f,10f);
        shakeTime = 0.4f;
        defaultPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            transform.position = defaultPos + new Vector3(1f, 1f, 1f) * Mathf.Sin(randomValue+ shakeTime*65f)*shakeTime;
            if (shakeTime <= 0)
            {
                transform.position = defaultPos;
            }
        }
	}
}
