using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalController : MonoBehaviour
{
    public GameObject otherPortal;
    private PortalController opController;
    public GameObject target;

    public bool cancelTeleport;
    private CamTargetController camTarget;

    public bool alreadyTeleported { get; set; }

    void Start()
    {
        camTarget = GameObject.Find("CamTarget").GetComponent<CamTargetController>();
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


                //PlaySound
                SoundManager.Instance.PlaySfx("Teleport");

                //Set new position of CamTarget
                camTarget.SetTarget(target.transform);


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
