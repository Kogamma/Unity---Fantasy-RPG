using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Stats_Script : MonoBehaviour
{
    int playerExp;
    int level;
    int playerDex;
    int playerLuck;
    int playerStr;
    int playerInt;
    int playerMaxHealth;
    int playerMana;

    int statTotal;

    public Text lucky;
    public Text str;
    public Text Int;
    public Text dex;
    public Text currentLevel;
    public Text maxHealth;
    public Text Mana;
    public Text add;

    public Slider expSlider;

	// Use this for initialization
	void Start ()
    {
        playerExp = PlayerSingleton.instance.playerExp;
        level = PlayerSingleton.instance.level;
        playerLuck = PlayerSingleton.instance.playerLuck;
        playerDex = PlayerSingleton.instance.playerDex;
        playerStr = PlayerSingleton.instance.playerStr;
        playerInt = PlayerSingleton.instance.playerInt;
        playerMaxHealth = PlayerSingleton.instance.playerMaxHealth;
        playerMana = PlayerSingleton.instance.playerMana;
    }
    void LateUpdate()
    {
        expSlider.value = playerExp;
        PLayerLevel();

        lucky.text = playerLuck.ToString();
        str.text = playerStr.ToString();
        Int.text = playerInt.ToString();
        dex.text = playerDex.ToString();

        currentLevel.text = level.ToString();
        maxHealth.text = playerMaxHealth.ToString();
        Mana.text = playerMana.ToString();
        add.text = statTotal.ToString();
    }

	void PLayerLevel()
    {
        //Level 2
        if(playerExp >= 100 && level == 1)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 100; 
            expSlider.maxValue = 200;

            //Add 1 plus on every stat 
            playerDex++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMana += 5;
           
            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 3
        if (playerExp >= 200 && level == 2)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 200;
            expSlider.maxValue = 300;

            //Add 1 plus on every stat 
            playerDex++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMana += 5;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 4
        if (playerExp >= 300 && level == 3)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 300;
            expSlider.maxValue = 400;

            //Add 1 plus on every stat 
            playerDex++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMana += 5;

            //Set the amount stats a player can pick
            statTotal += 2;
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
