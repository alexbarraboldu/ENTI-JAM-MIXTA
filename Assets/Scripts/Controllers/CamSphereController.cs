using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSphereController : MonoBehaviour
{
    private MeshRenderer[] renders;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter " + other);
        other.GetComponent<MeshRenderer>().enabled = false;

        renders = other.GetComponentsInChildren<MeshRenderer>();
        foreach (var render in renders)
        {
            render.enabled = false;
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit " + other);
        other.GetComponent<MeshRenderer>().enabled = true;
        
        renders = other.GetComponentsInChildren<MeshRenderer>();
        foreach (var render in renders)
        {
            render.enabled = true;
        }
    }
}
