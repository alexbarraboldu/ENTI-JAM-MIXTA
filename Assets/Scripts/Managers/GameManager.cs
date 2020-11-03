using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // PLAYER VARIABLES

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }

        //CHEATS, numbers == load level
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(4);
        }

        

    }
}
