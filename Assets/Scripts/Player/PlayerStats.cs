using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStats
{
    public int patienHealth = 100;
    public int SessionGoal = 5 ;
    
    public int playerPoints;
    public int headClips;
    public int chestClips;
    
    public PlayerStats(int patienHealth, int playerPoints)
    {
        this.patienHealth = patienHealth;
        this.playerPoints = playerPoints;
    }

    public void AddPoints(int amount)   
    {
        playerPoints+= amount;
        if (playerPoints>= SessionGoal)
        {
            Debug.Log("Win");

            //win
        }
    }

    public void ReleaseClip(ClipType clipType)
    {
        switch (clipType)
        {
            //TODO: bad solution for this, need to change to a class or something
            case ClipType.HeadR:
                headClips++;
                OpenHeadRAnimation();
                if (headClips>=2)
                {
                    OpenHeadAnimation();
                    Debug.Log("OPEN HEAD");
                }
                break;
            
            case ClipType.HeadL:
                headClips++;
                OpenHeadLAnimation();
                if (headClips>=2)
                {
                    OpenHeadAnimation();
                    Debug.Log("OPEN HEAD");
                }
                break;
            
            case ClipType.Chest:
                chestClips++;
                if (chestClips>=5)
                {
                    OpenChestAnimation();
                    Debug.Log("OPEN Chest");

                }
                break;
        }
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
    public void OpenHeadAnimation()
    {
        //open head
    }
    public void OpenHeadRAnimation()
    {
        //open Right Head
    }
    public void OpenHeadLAnimation()
    {
        //open Left Head
    }
    public void OpenChestAnimation()
    {
        //open chest
    }
    
}

public enum ClipType
{
    Head,
    HeadR,
    HeadL,
    Chest
}