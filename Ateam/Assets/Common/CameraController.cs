using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector3 targetPos;
    float moveSpeedRate;

    Vector3 targetRotate;
    float rotateSpeedRate;

    public void MoveTo(Vector3 target, float speedRate)
    {
        targetPos = target;
        moveSpeedRate = speedRate;
    }

    public void RotateTo(Vector3 target, float speedRate)
    {
        targetRotate = target;
        rotateSpeedRate = speedRate;
    }

    void Update()
    {
        transform.position += ( targetPos - transform.position )*moveSpeedRate;
        transform.rotation = Quaternion.Euler( transform.rotation.eulerAngles + (targetRotate - transform.rotation.eulerAngles) * rotateSpeedRate );
    }

}
