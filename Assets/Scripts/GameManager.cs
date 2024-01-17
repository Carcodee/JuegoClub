using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action OnItemDropedCorrectly;
    public static Action <ClipType>OnClipRelease;

    [Header("Round")]
    public int currentRoundTime;
    public float totalRoundTime = 90;
    public int currentRound = 1;

    [Header("Players")]
    public int numberOfPlayers;
    public PlayerStats[] playerStats;
    public int currentPlayer = 0;

    public PacientBehaivor currentPacient;

    private void OnEnable()
    {
        OnItemDropedCorrectly += AddPointToPlayer;
        OnClipRelease += AddReleaseClipToPlayer;
    }
    private void OnDisable()
    {
        OnItemDropedCorrectly -= AddPointToPlayer;
        OnClipRelease -= AddReleaseClipToPlayer;
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
        if(totalRoundTime > 0) {
            totalRoundTime -= Time.deltaTime;
        }else if(totalRoundTime <= 0) {

        }
    }
    void InitializatePlayers()
    {
        playerStats = new PlayerStats[numberOfPlayers];
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i] = new PlayerStats(100, 0,currentPacient);
        }
    }
    public void AddPointToPlayer()
    {
        playerStats[currentPlayer].AddPoints(1);
    }
    public void AddReleaseClipToPlayer(ClipType clipType)
    {
        playerStats[currentPlayer].ReleaseClip(clipType);
    }

    public void DamagePlayer(int damage)
    {
        playerStats[currentPlayer].MakeDamageOnPlayer(damage);
        Debug.Log("DAMAGE");

    }
}
