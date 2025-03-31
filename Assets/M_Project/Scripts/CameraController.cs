using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speedMoveCamera;
    public float speedRotateCamera;
    public Vector3 cameraOffSetForward = new Vector3(0, 8, -7);
    public Vector3 cameraOffSetBack = new Vector3(0, 8, 7);
    public Vector3 cameraOffSetLeft = new Vector3(-7, 8, 0);
    public Vector3 cameraOffSetRight = new Vector3(7, 8, 0);
    public void CameraRotateToPlayer(int cell, bool isOutSide, Transform target)
    {
        if(isOutSide)
        {
            if(cell >= 15 && cell < 24)
            {
                transform.rotation = Quaternion.Euler(45, 90, 0);
                transform.position = target.position + cameraOffSetLeft;

            }
            else if (cell >= 24 && cell < 38)
            {
                transform.rotation = Quaternion.Euler(45, 180, 0);
                transform.position = target.position + cameraOffSetBack;
            }
            if (cell >= 38 && cell <= 45)
            {
                transform.rotation = Quaternion.Euler(45, -90, 0);
                transform.position = target.position + cameraOffSetRight;
            }
            if (cell >= 0 && cell < 15)
            {
                transform.rotation = Quaternion.Euler(45, 0, 0);
                transform.position = target.position + cameraOffSetForward;
            }
        }
        else
        {
            if (cell >= 8 && cell < 12)
            {
                transform.rotation = Quaternion.Euler(45, 90, 0);
                transform.position = target.position + cameraOffSetLeft;
            }
            else if (cell >= 12 && cell < 20)
            {
                transform.rotation = Quaternion.Euler(45, 180, 0);
                transform.position = target.position + cameraOffSetBack;
            }
            if (cell >= 20 && cell <= 24)
            {
                transform.rotation = Quaternion.Euler(45, -90, 0);
                transform.position = target.position + cameraOffSetRight;
            }
            if (cell >= 0 && cell < 12)
            {
                transform.rotation = Quaternion.Euler(45, 0, 0);
                transform.position = target.position + cameraOffSetForward;
            }
        }
    }
    public void RotateToDice()
    {
        transform.rotation = Quaternion.Euler(45, 0, 0);
        transform.position = new Vector3(0,8,-6);
    }
}
