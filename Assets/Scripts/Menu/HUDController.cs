using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI points;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI PlayerTime;
    [Header("Pause")]
    public GameObject pauseMenu;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

    }
    public void UpdateStats()
    {
        points.text = "Points: " + GameManager.instance.playerStats[GameManager.instance.currentPlayer].playerPoints;
        hp.text = "Hp: " + GameManager.instance.playerStats[GameManager.instance.currentPlayer].patienHealth;
        stamina.text = "Stamina: " + GameManager.instance.playerStats[GameManager.instance.currentPlayer].playerPoints;
        PlayerTime.text = "Time Left: " + GameManager.instance.currentRoundTime;

    }
    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 1;
        }
    }
    
    
}
