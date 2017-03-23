using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;

public class Level_Stats_Script : MonoBehaviour
{
    int playerExp;
    int level;
    int playerVit;
    int playerLuck;
    int playerStr;
    int playerInt;
    int playerMaxHealth;
    int playerMaxMana;

    int statTotal;

    public Text lucky;
    public Text str;
    public Text Int;
    public Text vit;
    public Text currentLevel;
    public Text add;
    public Text XPtext;
    public Text statInfo;

    public GameObject stat_info;

    public Slider expSlider;

    public Text[] addButtonsText;
    private Button[] addButtons;

    void Start()
    {
        expSlider.maxValue = PlayerSingleton.instance.currentXPNeeded;

        addButtons = new Button[addButtonsText.Length];

        for (int i = 0; i < addButtonsText.Length; i++)
        {
            addButtons[i] = addButtonsText[i].transform.parent.GetComponent<Button>();
        }

        // Add listeners for showing info about the stats by selecting the buttons
        for (int i = 0; i < addButtons.Length; i++)
        {
            // Gets the eventtrigger component 
            EventTrigger trigger = addButtons[i].GetComponent<EventTrigger>();

            // Adds the select event on the buttons and the correct function to call
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { StatInformation((BaseEventData)eventData); });

            trigger.triggers.Add(entry);

            // Adds the deselect event on the buttons and the correct function to call
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.Deselect;
            entry2.callback.AddListener((eventData) => { HideInfo((BaseEventData)eventData); });

            trigger.triggers.Add(entry2);
        }
    }
     
    void Update()
    {
        playerExp = PlayerSingleton.instance.playerExp;
        level = PlayerSingleton.instance.level;
        playerLuck = PlayerSingleton.instance.playerLuck;
        playerVit = PlayerSingleton.instance.playerVit;
        playerStr = PlayerSingleton.instance.playerStr;
        playerInt = PlayerSingleton.instance.playerInt;
        playerMaxHealth = PlayerSingleton.instance.playerMaxHealth;
        playerMaxMana = PlayerSingleton.instance.playerMaxMana;
        statTotal = PlayerSingleton.instance.skillPoints;
        expSlider.maxValue = PlayerSingleton.instance.currentXPNeeded;

        expSlider.value = playerExp;
        XPtext.text = playerExp + " / " + expSlider.maxValue;

        lucky.text = playerLuck.ToString();
        str.text = playerStr.ToString();
        Int.text = playerInt.ToString();
        vit.text = playerVit.ToString();

        currentLevel.text = level.ToString();

        add.text = statTotal.ToString();

        if (PlayerSingleton.instance.skillPoints >= 1)
        {
            for (int i = 0; i < addButtonsText.Length; i++)
            {
                addButtonsText[i].transform.parent.GetComponent<Button>().interactable = true;
                if (!addButtonsText[i].text.Contains(" +")) 
                    addButtonsText[i].text = addButtonsText[i].text + " +";
            }
        }
        else
        {
            
            for (int i = 0; i < addButtonsText.Length; i++)
            {
                //addButtons[i].transform.parent.GetComponent<Button>().interactable = false;
                if (addButtonsText[i].text.Contains(" +"))
                    addButtonsText[i].text = addButtonsText[i].text.Remove(addButtonsText[i].text.Length - 2, 2);
            }
        }
    }

	public bool PlayerLevel()
    {
        playerExp = PlayerSingleton.instance.playerExp;
        level = PlayerSingleton.instance.level;
        playerLuck = PlayerSingleton.instance.playerLuck;
        playerVit = PlayerSingleton.instance.playerVit;
        playerStr = PlayerSingleton.instance.playerStr;
        playerInt = PlayerSingleton.instance.playerInt;
        playerMaxHealth = PlayerSingleton.instance.playerMaxHealth;
        playerMaxMana = PlayerSingleton.instance.playerMaxMana;
        statTotal = PlayerSingleton.instance.skillPoints;
        expSlider.maxValue = PlayerSingleton.instance.currentXPNeeded;


        //Level 2
        if (playerExp >= 100 && level == 1)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 100;
            expSlider.maxValue = 200;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 3
        else if (playerExp >= 200 && level == 2)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 200;
            expSlider.maxValue = 300;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 4
        else if (playerExp >= 300 && level == 3)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 300;
            expSlider.maxValue = 400;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 5
        else if (playerExp >= 400 && level == 4)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 400;
            expSlider.maxValue = 500;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 6
        else if (playerExp >= 500 && level == 5)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 500;
            expSlider.maxValue = 600;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 7
        else if (playerExp >= 600 && level == 6)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 600;
            expSlider.maxValue = 700;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 8
        else if (playerExp >= 700 && level == 7)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 700;
            expSlider.maxValue = 800;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 9
        else if (playerExp >= 800 && level == 8)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 800;
            expSlider.maxValue = 900;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 10
        else if (playerExp >= 900 && level == 9)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 900;
            expSlider.maxValue = 1000;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 11
        else if (playerExp >= 1000 && level == 10)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 1000;
            expSlider.maxValue = 1100;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 12
        else if (playerExp >= 1100 && level == 11)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 1100;
            expSlider.maxValue = 1200;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 13
        else if (playerExp >= 1200 && level == 12)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 1200;
            expSlider.maxValue = 1300;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 14
        else if (playerExp >= 1300 && level == 13)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            playerExp -= 1300;
            expSlider.maxValue = 1400;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        //Level 15
        else if (playerExp >= 1400 && level == 14)
        {
            //Add Level
            level++;

            //Change expSlider max value & reset exp bar
            //playerExp -= 700;
            //expSlider.maxValue = 750;

            //Add 1 plus on every stat 
            playerVit++;
            playerLuck++;
            playerInt++;
            playerStr++;

            //Add 5 Health and mana
            playerMaxHealth += 5;
            playerMaxMana += 5;

            PlayerSingleton.instance.playerHealth = playerMaxHealth;
            PlayerSingleton.instance.playerMana = playerMaxMana;

            //Set the amount stats a player can pick
            statTotal += 2;
        }
        else
            return false;

        PlayerSingleton.instance.playerExp = playerExp;
        PlayerSingleton.instance.level = level;
        PlayerSingleton.instance.playerLuck = playerLuck;
        PlayerSingleton.instance.playerVit = playerVit;
        PlayerSingleton.instance.playerStr = playerStr;
        PlayerSingleton.instance.playerInt = playerInt;
        PlayerSingleton.instance.playerMaxHealth = playerMaxHealth;
        PlayerSingleton.instance.playerMaxMana = playerMaxMana;
        PlayerSingleton.instance.skillPoints = statTotal;
        PlayerSingleton.instance.currentXPNeeded = (int)expSlider.maxValue;


        return true;
    }

    #region Add Stat Buttons

    //Allow the player to add Vit, Luck, Int or Str
    public void AddVit()
    {
        statTotal = PlayerSingleton.instance.skillPoints;
        if (statTotal >= 1)
        {
            playerVit++;
            statTotal--;
            
            PlayerSingleton.instance.playerVit = playerVit;
            PlayerSingleton.instance.skillPoints = statTotal;
            PlayerSingleton.instance.playerHealth += 3;
            PlayerSingleton.instance.playerMaxHealth += 3;
        }
    }
    public void AddLuck()
    {
        statTotal = PlayerSingleton.instance.skillPoints;

        if (statTotal >= 1)
        {
            playerLuck++;
            statTotal--;

            PlayerSingleton.instance.playerLuck = playerLuck;
            PlayerSingleton.instance.skillPoints = statTotal;
        }
    }
    public void AddInt()
    {
        statTotal = PlayerSingleton.instance.skillPoints;

        if (statTotal >= 1)
        {
            playerInt++;
            statTotal--;

            PlayerSingleton.instance.playerInt = playerInt;
            PlayerSingleton.instance.skillPoints = statTotal;

            PlayerSingleton.instance.playerMana += 2;
            PlayerSingleton.instance.playerMaxMana += 2;
        }
    }
    public void AddStr()
    {
        statTotal = PlayerSingleton.instance.skillPoints;

        if (statTotal >= 1)
        {
            playerStr++;
            statTotal--;

            PlayerSingleton.instance.playerStr = playerStr;
            PlayerSingleton.instance.skillPoints = statTotal;
        }
    }

    #endregion

    //Show the player what the stats do in game
    public void StatInformation(BaseEventData eventData)
    {
        int index = addButtons.ToList().IndexOf(eventData.selectedObject.GetComponent<Button>());

        if (index == 0)
        {
            statInfo.text = "Vitality - Makes your amount of health larger";
        }
        else if(index == 1)
        {
            statInfo.text = "Luck - Make a greater possibility to get a critical hit note";
        }
        else if(index == 2)
        {
            statInfo.text = "Intelligence - Make magic attacks stronger";
        }
        else if(index == 3)
        {
            statInfo.text = "Strength - Make melee attacks stronger";
        }
        stat_info.SetActive(true);
    }


    public void HideInfo(BaseEventData eventData)
    {
        stat_info.SetActive(false);
    }
}
