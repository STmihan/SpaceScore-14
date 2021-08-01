using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;
    public float offset;

    void Update()
    {
        var targetPos = target.position;
        targetPos.x = 0;
        targetPos.y -= offset;
        targetPos.z = -10;
        
        transform.position = targetPos;
    }
}
