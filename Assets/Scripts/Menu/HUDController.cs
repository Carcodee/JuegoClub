using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI points;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI PlayerTime;
    [Header("Pause")]
    public GameObject pauseMenu;

    public Image[] images;
    public Image currentHpImage;

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
        HandleHpStates();
        UpdateStats();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

    }
    public void UpdateStats()
    {
        points.text = "Score: " + GameManager.instance.playerStats[GameManager.instance.currentPlayer].playerPoints;
        hp.text = GameManager.instance.playerStats[GameManager.instance.currentPlayer].patienHealth.ToString();
        PlayerTime.text = "Time Left: " + GameManager.instance.currentRoundTime;

    }

    public void HandleHpStates() {

        int currentHp = GameManager.instance.playerStats[GameManager.instance.currentPlayer].patienHealth;

        if (currentHp==100) {
            currentHpImage = images[0];
        }
        if (currentHp < 60) {
            currentHpImage = images[1];
        }
        if (currentHp < 25) {
            currentHpImage = images[2];
        }
        if (currentHp <= 0) {
            currentHpImage = images[3];
        }
        for (int i = 0; i < images.Length; i++) {
            images[i].gameObject.SetActive(false);
        }
        currentHpImage.gameObject.SetActive(true);

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
