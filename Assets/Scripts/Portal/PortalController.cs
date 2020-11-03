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

   // private Rigidbody rigidbody;

    public bool alreadyTeleported { get; set; }

    void Start()
    {
     //   rigidbody = GetComponent<Rigidbody>();
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
                //other.gameObject.transform.position = new Vector3(otherPortal.transform.position.x + 1f, otherPortal.transform.position.y, otherPortal.transform.position.z + 1f);
                other.gameObject.transform.position = otherPortal.transform.position;


                //PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

                other.gameObject.GetComponent<PlayerController>().hightOffset = otherPortal.transform.position.y;
                other.gameObject.GetComponent<PlayerController>().xToGo = otherPortal.transform.position.x;
                other.gameObject.GetComponent<PlayerController>().zToGo = otherPortal.transform.position.z;

                other.gameObject.GetComponent<PlayerController>().arrived = true;
                other.gameObject.GetComponent<PlayerController>().moving = false;
                other.gameObject.GetComponent<PlayerController>().walking = false;
                other.gameObject.GetComponent<PlayerController>().isOnFloor = true;

                other.gameObject.GetComponent<PlayerController>().teleporting = true;

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
