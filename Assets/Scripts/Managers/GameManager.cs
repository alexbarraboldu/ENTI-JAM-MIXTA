﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // PLAYER VARIABLES
    public float energy = 1f;
    public float energyLoss = 1f;
    public float energyRegen = 2f;



    // CONFIGURAR OPCIONES
    public float cameraSensibility = 50f;
    public float zoomSensibility = 50f;


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
