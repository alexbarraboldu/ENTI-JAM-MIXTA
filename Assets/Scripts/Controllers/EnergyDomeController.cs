using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDomeController : MonoBehaviour
{

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        player.isUnderDome = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player.isUnderDome = false;
    }

}
