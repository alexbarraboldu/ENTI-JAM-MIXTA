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
    private int energy;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        energySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        energyLoss = gm.energyLoss;
        energyRegen = gm.energyRegen;

        energySlider.maxValue = 10f;
        energySlider.value = energySlider.maxValue;

        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(energySlider.value<=0)
        {
            //TODO
            Debug.Log("sin energia");
            return;
        }

        if(player.isUnderDome)
        {
            energySlider.value += energyRegen;
        } else
        {
            energySlider.value -= energyLoss;
        }

        //Debug.Log("Energy: " + energySlider.value);

        time += Time.deltaTime;

        energy = Mathf.FloorToInt(time % 60);

        Debug.LogWarning(energy);



    }

    
}
