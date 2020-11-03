﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // PLAYER VARIABLES
    public float maxEnergy = 10f;
    public float energy = 0f;
    public float energyLoss = 1f;
    public float energyRegen = 2f;



    // CONFIGURAR OPCIONES
    public float mouseSensibility;
    public float zoomSensibility;


    //INSTANCE GAME MANAGER
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
