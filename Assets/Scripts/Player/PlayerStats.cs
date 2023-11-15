using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStats
{
    public int patienHealth = 100;
    public int heightSpeed = 5;
    public float staminaTotalTime = 5.0f;
    public float staminaTotalCooldown = 10.0f;
    public float handSpeed = 1.0f;
    public float holdTime = 1.0f;
    public int playerPoints;

    
    public PlayerStats(int patienHealth, int heightSpeed, float staminaTotalTime, float staminaTotalCooldown, float handSpeed, float holdTime, int playerPoints)
    {
        this.patienHealth = patienHealth;
        this.heightSpeed = heightSpeed;
        this.staminaTotalTime = staminaTotalTime;
        this.staminaTotalCooldown = staminaTotalCooldown;
        this.handSpeed = handSpeed;
        this.holdTime = holdTime;
        this.playerPoints = playerPoints;
    }

    public void AddPoints(int amount)
    {
        playerPoints+= amount;
    }
    public void MakeDamageOnPlayer(int damage)
    {
    
        if (patienHealth > 0)
        {
            patienHealth -= damage;
        }
        else
        {
            //die
        }
    }
}
