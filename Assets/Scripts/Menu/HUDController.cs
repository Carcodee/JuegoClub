using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI points;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI Time;


    private void OnEnable()
    {
        GameManager.OnItemDropedCorrectly += UpdateStats;
    }
    private void OnDisable()
    {
        GameManager.OnItemDropedCorrectly -= UpdateStats;
    }
    void Start()
    {
        UpdateStats();
    }

    void Update()
    {
        UpdateStats();

    }
    public void UpdateStats()
    {
        points.text = "Points: " + GameManager.instance.playerStats[GameManager.instance.currentPlayer].playerPoints;
        hp.text = "Hp: " + GameManager.instance.playerStats[GameManager.instance.currentPlayer].patienHealth;
        stamina.text = "Stamina: " + GameManager.instance.playerStats[GameManager.instance.currentPlayer].playerPoints;
        Time.text = "Time Left: " + GameManager.instance.currentRoundTime;

    }
}
