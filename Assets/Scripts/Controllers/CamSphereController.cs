using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSphereController : MonoBehaviour
{
    private MeshRenderer[] renders;
    private void OnTriggerEnter(Collider other)
    {
        renders = other.GetComponentsInChildren<MeshRenderer>();
        foreach (var render in renders)
        {
            render.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {               
        renders = other.GetComponentsInChildren<MeshRenderer>();
        foreach (var render in renders)
        {
            render.enabled = true;
        }
    }
}
