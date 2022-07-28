using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1,20)]
    public float SmoothCam;
    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    void Follow(){
        Vector3 targetPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, SmoothCam * Time.fixedDeltaTime);
        transform.position = smoothPos;
    }
}
