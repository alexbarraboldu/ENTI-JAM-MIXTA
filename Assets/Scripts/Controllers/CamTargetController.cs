using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTargetController : MonoBehaviour
{
    //TODO Trigger zone, change target.
    public Transform target;
    [Range(0f, 1f)]
    public float smoothTime = 0.3F;
    public Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }
}
