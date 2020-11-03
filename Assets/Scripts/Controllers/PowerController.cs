using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    [Range(5f, 20f)]
    public float rotation;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotation * Time.fixedDeltaTime);
    }
}
