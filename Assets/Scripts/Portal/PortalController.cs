using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalController : MonoBehaviour
{
    public GameObject otherPortal;
    private PortalController opController;

    public bool cancelTeleport;

    public bool alreadyTeleported { get; set; }

    void Start()
    {
        opController = otherPortal.GetComponent<PortalController>();
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
                other.gameObject.transform.position = otherPortal.transform.position;
                other.gameObject.GetComponent<PlayerController>().hightOffset = otherPortal.transform.position.y;
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
