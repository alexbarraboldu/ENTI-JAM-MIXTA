using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalController : MonoBehaviour
{
    public Transform otherPortal;
    public GameObject otherPortal2;
    private PortalController opController;

    private bool cancelTeleport;

    public bool alreadyTeleported { get; set; }

    void Start()
    {
        opController = otherPortal2.GetComponent<PortalController>();
        cancelTeleport = false;
    }

    void Update()
    {
        if (opController.alreadyTeleported) cancelTeleport = true;
        else cancelTeleport = false;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (!cancelTeleport)
            {
                other.gameObject.transform.position = otherPortal.position;
               // other.gameObject.transform.rotation = otherPortal.rotation;
                alreadyTeleported = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            alreadyTeleported = false;
        }
    }
}
