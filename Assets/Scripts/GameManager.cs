using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action OnItemDropedCorrectly;

    [Header("Round")]
    public int currentRoundTime;
    public int totalRoundTime = 90;
    public int currentRound = 1;

    [Header("Players")]
    public int numberOfPlayers;
    public PlayerStats[] playerStats;
    public int currentPlayer = 0;

    private void OnEnable()
    {
        OnItemDropedCorrectly += AddPointToPlayer;
    }
    private void OnDisable()
    {
        OnItemDropedCorrectly -= AddPointToPlayer;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        InitializatePlayers();
    }
    
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitializatePlayers()
    {
        playerStats = new PlayerStats[numberOfPlayers];
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i] = new PlayerStats(100, 5, 5, 10, 1, 1, 0);
        }
    }
    public void AddPointToPlayer()
    {
        playerStats[currentPlayer].AddPoints(1);
    }
    public void DamagePlayer(int damage)
    {
        playerStats[currentPlayer].MakeDamageOnPlayer(damage);
        Debug.Log("DAMAGE");

    }
}
