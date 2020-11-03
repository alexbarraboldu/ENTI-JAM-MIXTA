using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //CONFIG VARIABLES
    public float maxZoom;
    public float minZoom;

    //INTERNAL VARIABLES
    //Variables control camara    
    private float zoomSensibility;
    private float cameraSensibility;

    //Sólo necesitamos calcular de 1 rig, el resto se mueven a la vez usando el mismo ratio y limites
    private float initRadiusBotRig;
    private float maxRadiusBotRig;
    private float minRadiusBotRig;

    private float actualRadiusBotRig;
    private float newRadiusBopRig;

    private float zoomVariation;

    private float mouseWheel;

    // Referencias GameObjects
    private CinemachineFreeLook cinemachine;

        

    private void Start()
    {
        cinemachine = GetComponent<CinemachineFreeLook>();

        //Set min/max zoom
        initRadiusBotRig = cinemachine.m_Orbits[2].m_Radius;
        maxRadiusBotRig = maxZoom + initRadiusBotRig;
        minRadiusBotRig = initRadiusBotRig - minZoom;
        
        //Get sensibility
        zoomSensibility = GameManager.Instance.zoomSensibility;
        cameraSensibility = GameManager.Instance.cameraSensibility;
    }



    // Update is called once per frame
    void Update()
    {
        
        //INPUTS
        mouseWheel = Input.mouseScrollDelta.y;
        

        //CONTROL ZOOM
        if (mouseWheel != 0)
        {
            //Get cantidad de zoom a modificar
            zoomVariation = mouseWheel * Time.deltaTime * zoomSensibility * -1;
            
            //Calculamos el rig actual
            actualRadiusBotRig = cinemachine.m_Orbits[2].m_Radius;

            //Calculamos la nueva posicion del rig
            newRadiusBopRig = zoomVariation + actualRadiusBotRig;
            
            //Verificamos que pueda aplicarse el zoom
            if ((newRadiusBopRig < maxRadiusBotRig) && (newRadiusBopRig > minRadiusBotRig))
            {
                cinemachine.m_Orbits[0].m_Radius += zoomVariation;
                cinemachine.m_Orbits[1].m_Radius += zoomVariation;
                cinemachine.m_Orbits[2].m_Radius += zoomVariation;
            }
            
        }       

    }
}
