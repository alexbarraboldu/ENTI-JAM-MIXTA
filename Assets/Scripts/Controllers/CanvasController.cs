using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CanvasController : MonoBehaviour
{
    private TextMeshProUGUI diamondsCount;
    public LevelManager levelManager;

    private string actualDiamonds;
    private string maxDiamonds;
    private string message;

    private void Start()
    {
        diamondsCount = GameObject.Find("DiamondText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        actualDiamonds = levelManager.diamonds.ToString();
        maxDiamonds = levelManager.maxDiamonds.ToString();

        message = actualDiamonds + " / " + maxDiamonds;

        diamondsCount.text = message;
    }
}
