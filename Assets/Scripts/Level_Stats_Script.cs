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
        maxHealth.text = PlayerSingleton.instance.playerHealth + " / " + playerMaxHealth.ToString();
        Mana.text = PlayerSingleton.instance.playerMana + " / " + playerMana.ToString();
        add.text = statTotal.ToString();
    }

	void PLayerLevel()
    {
        //Level 2
        if(playerExp >= 50 && level == 1)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 50; 
            expSlider.maxValue = 100;

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
        if (playerExp >= 100 && level == 2)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 100;
            expSlider.maxValue = 150;

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
        if (playerExp >= 150 && level == 3)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 150;
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
        //Level 5
        if (playerExp >= 200 && level == 4)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 200;
            expSlider.maxValue = 250;

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
        //Level 6
        if (playerExp >= 250 && level == 5)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 250;
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
        //Level 7
        if (playerExp >= 300 && level == 6)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 300;
            expSlider.maxValue = 350;

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
        //Level 8
        if (playerExp >= 350 && level == 7)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 350;
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
        //Level 9
        if (playerExp >= 400 && level == 8)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 400;
            expSlider.maxValue = 450;

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
        //Level 10
        if (playerExp >= 450 && level == 9)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 450;
            expSlider.maxValue = 500;

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
        //Level 11
        if (playerExp >= 500 && level == 10)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 500;
            expSlider.maxValue = 550;

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
        //Level 12
        if (playerExp >= 550 && level == 11)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 550;
            expSlider.maxValue = 600;

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
        //Level 13
        if (playerExp >= 600 && level == 12)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 600;
            expSlider.maxValue = 650;

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
        //Level 14
        if (playerExp >= 650 && level == 13)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 650;
            expSlider.maxValue = 700;

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
        //Level 15
        if (playerExp >= 700 && level == 14)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 700;
            expSlider.maxValue = 750;

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
        //Level 16
        if (playerExp >= 750 && level == 15)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 750;
            expSlider.maxValue = 800;

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
        //Level 17
        if (playerExp >= 800 && level == 16)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 800;
            expSlider.maxValue = 850;

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
        //Level 18
        if (playerExp >= 850 && level == 17)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 850;
            expSlider.maxValue = 900;

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
        //Level 19
        if (playerExp >= 900 && level == 18)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 900;
            expSlider.maxValue = 950;

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
        //Level 20
        if (playerExp >= 950 && level == 19)
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

            //Set the amount stats a player can pick
            statTotal += 2;
        }

        PlayerSingleton.instance.playerExp = playerExp;
        PlayerSingleton.instance.level = level;
        PlayerSingleton.instance.playerLuck = playerLuck;
        PlayerSingleton.instance.playerDex = playerDex;
        PlayerSingleton.instance.playerStr = playerStr;
        PlayerSingleton.instance.playerInt = playerInt;
        PlayerSingleton.instance.playerMaxHealth = playerMaxHealth;
        PlayerSingleton.instance.playerMana = playerMana;

    }
    //Allow the player to add Dex, Luck, Int or Str
    public void AddDex()
    {
        if(statTotal >= 1)
        {
            playerDex++;
            statTotal--;

            PlayerSingleton.instance.playerDex = playerDex;
        }
    }
    public void AddLuck()
    {
        if (statTotal >= 1)
        {
            playerLuck++;
            statTotal--;

            PlayerSingleton.instance.playerLuck = playerLuck;
        }
    }
    public void AddInt()
    {
        if (statTotal >= 1)
        {
            playerInt++;
            statTotal--;

            PlayerSingleton.instance.playerInt = playerInt;
        }
    }
    public void AddStr()
    {
        if (statTotal >= 1)
        {
            playerStr++;
            statTotal--;

            PlayerSingleton.instance.playerStr = playerStr;
        }
    }
}
