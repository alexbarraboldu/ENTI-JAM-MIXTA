using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GameManager gm;
    private Slider energySlider;
    private PlayerController player;

    private float energyLoss;
    private float energyRegen;

    private float time = 0f;
    public int energy;

    private float nextActionTime = 0.0f;
    private float period = 1.0f;

    public GameObject fillArea;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        energySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        energyLoss = gm.energyLoss;
        energyRegen = gm.energyRegen;

        energySlider.maxValue = gm.maxEnergy;
        energySlider.value = energySlider.maxValue;

        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (energySlider.value<=0)
        {
            //TODO

            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                Debug.Log("sin energia");
            }
            fillArea.SetActive(false);
            return;
        }

        if (player.isUnderDome)
        {
            if (energySlider.maxValue != energySlider.value)
            {
                energySlider.value += energyRegen;
            }
        } 
        else
        {
            energySlider.value -= energyLoss;
        }

        //Debug.Log("Energy: " + energySlider.value);

        time += Time.deltaTime;
        energy = Mathf.FloorToInt(time % 60);
        //  UPDATE EVERY SECOND
        if(Time.time > nextActionTime)
        {
            nextActionTime += period;
            //Debug.Log(energy);
        }


        gm.energy = energySlider.value;
    }

    
}
