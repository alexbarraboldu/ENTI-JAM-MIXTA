using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // PLAYER VARIABLES
    public float maxEnergy = 100f;
    public float energy = 0f;
    public float energyLoss = 1f;
    public float energyRegen = 2f;

    //  GAME ELEMENTS
    public int maxDiamonds = 0;
    public int diamonds = 0;

    private Slider energySlider;
    private PlayerController player;

    //  INSTANCE GAME MANAGER
    
    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {            
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
    }

    void Start()
    {
        energySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        maxDiamonds = GameObject.FindGameObjectsWithTag("Diamond").Length;
        diamonds = 0;

        energy = maxEnergy;

        energySlider.maxValue = maxEnergy;
        energySlider.value = energy;

        SoundManager.Instance.playingNow = Utils.PlayingNow.INGAME;
        SoundManager.Instance.StopMusic();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            energy = 100f;
        }

        if (diamonds >= maxDiamonds)
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        EnergyControl();
    }


    private float timeDangerSound = 0f;
    private float maxTimeDangerSound = 2f;
    public float thresholdDanger0 = 80;
    public float thresholdDanger1 = 50;
    public float thresholdDanger2 = 25;

    void EnergyControl()
    {
        if (energySlider.value <= 0)
        {

            SceneManager.LoadScene(1);

            return;
        }


        // PARTE AUDIO
        if (!player.isUnderDome)
        {
            if (energySlider.value <= thresholdDanger0 && energySlider.value > thresholdDanger1)
            {
                timeDangerSound += Time.deltaTime;
                if(timeDangerSound >= maxTimeDangerSound - 0.75f)
                {
                    timeDangerSound = 0;
                    SoundManager.Instance.PlaySfxForcePitch("Danger", 1);
                }
            } 
            else if (energySlider.value <= thresholdDanger1 && energySlider.value > thresholdDanger2)
            {
                timeDangerSound += Time.deltaTime;
                if (timeDangerSound >= maxTimeDangerSound-0.25f)
                {
                    timeDangerSound = 0;
                    SoundManager.Instance.PlaySfxForcePitch("Danger", 0.87f);
                }
            } 
            else if (energySlider.value <= thresholdDanger2)
            {
                timeDangerSound += Time.deltaTime;
                if (timeDangerSound >= maxTimeDangerSound)
                {
                    timeDangerSound = 0;
                    SoundManager.Instance.PlaySfxForcePitch("Danger", 0.80f);
                }
            }
        }
        // PARTE LOGICA

        if (player.isUnderDome)
        {
            if (energySlider.maxValue != energySlider.value)
            {
                energy += energyRegen;
            }
        }
        else
        {
            energy -= energyLoss;
        }

        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        energySlider.value = energy;
    }

}
