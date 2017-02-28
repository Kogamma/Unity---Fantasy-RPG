using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Stats_Script : PlayerSingleton
{
    int statTotal = 0;
     
	// Use this for initialization
	void Start ()
    {
		
	}
    void LateUpdate()
    {
        PLayerLevel();
    }

	void PLayerLevel()
    {
        //Level 2
        if(playerExp >= 100 && level == 1)
        {
            //Add Level
            level++;

            //Add 1 plus on every stat 
            playerDex++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMana += 5;

            //Reset Players Exp
            playerExp = 0;

            //Set the amount stats a player can pick
            statTotal = 2;
        }
        //Level 3
        if (playerExp >= 200 && level == 2)
        {
            //Add Level
            level++;

            //Add 1 plus on every stat 
            playerDex++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMana += 5;

            //Reset Players Exp
            playerExp = 0;

            //Set the amount stats a player can pick
            statTotal = 2;
        }
        //Level 4
        if (playerExp >= 300 && level == 3)
        {
            //Add Level
            level++;

            //Add 1 plus on every stat 
            playerDex++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMana += 5;

            //Reset Players Exp
            playerExp = 0;

            //Set the amount stats a player can pick
            statTotal = 2;
        }

    }
    //Allow the player to add Dex, Luck, Int or Str
    public void AddDex()
    {
        if(statTotal >= 1)
        {
            playerDex++;
            statTotal--;
        }
    }
    public void AddLuck()
    {
        if (statTotal >= 1)
        {
            playerLuck++;
            statTotal--;
        }   
    }
    public void AddInt()
    {
        if (statTotal >= 1)
        {
            playerInt++;
            statTotal--;
        }
    }
    public void AddStr()
    {
        if (statTotal >= 1)
        {
            playerStr++;
            statTotal--;
        }      
    }
}
