using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // PLAYER VARIABLES
    public float maxEnergy = 100f;
    public float energy = 0f;
    public float energyLoss = 1f;
    public float energyRegen = 2f;

    //  GAME ELEMENTS
    public int maxDiamonds = 3;
    public int diamonds = 0;

    private Slider energySlider;
    private PlayerController player;

    private float time = 0f;

    private float nextActionTime = 0.0f;
    private float period = 1.0f;

    //  INSTANCE GAME MANAGER
    public GameObject fillArea;
    public static LevelManager Instance { get; private set; }
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

    void Start()
    {
        energySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        diamonds = 0;

        energy = maxEnergy;

        energySlider.maxValue = maxEnergy;
        energySlider.value = energy;

        SoundManager.Instance.StopMusic();
        SoundManager.Instance.playingNow = Utils.PlayingNow.INGAME;

    }

    void Update()
    {
        if (player == null)
        {
            GameManager.Instance.isDead = true;
            return;
        }

        EnergyControl();
    }

    void EnergyControl()
    {
        if (energySlider.value <= 0)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                Debug.Log("sin energia");

                Destroy(player.gameObject, 2f);
            }
            fillArea.SetActive(false);
            return;
        }

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
