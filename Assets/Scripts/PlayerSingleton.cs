using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject prefab = (GameObject)Resources.Load("PlayerSingleton");
                GameObject created = Instantiate(prefab);
                DontDestroyOnLoad(created);
                m_instance = created.GetComponent<PlayerSingleton>();
            }

            return m_instance;
        }
    }

    private static PlayerSingleton m_instance;

    /* Variables to be saved */

    // How much damage and magic damage the player does
    public float playerDmg = 1;
    public int playerMagicDmg = 1;

    // How many hitpoints the player has
    public int playerMaxHealth = 10;
    public int playerHealth = 10;

    public int level = 1;
    public int currentXPNeeded = 50;
    public int skillPoints = 0;

    // How much experience points the player has in total
    public int playerExp = 0;
    
    // How much of the intelligence stat the player has
    public int playerInt = 5;
    // How much of the strength stat the player has
    public int playerStr = 5;
    // How much of the dexterity stat the player has
    public int playerDex = 5;
    // How much of the luck stat the player has
    public int playerLuck = 5;

    // How much mana points the player has
    public int playerMana = 10;
    public int playerMaxMana = 10;

    public List<string> playerInventory = new List<string>();
    public List<int> inventoryAmounts = new List<int>();

    // The current maximum size of slots in the inventory
    public int inventorySize = 15;
    
    // Creates the equipped items array and fils it with default value items that means the slot is empty
    public string[] equippedItems = new string[6] {"null", "null", "null", "null", "null", "null"};

    #region In-game variables

    // Variables used for in-combat purposes to see what the current damage of the player is,
    // and if the player has attacked this round or not
    //public int currentDmg;
    public bool playerAttacked = false;

    public bool poisoned = false;
    public bool confused = false;

    // Saves the position and rotation of the player when they go into battle to know where to spawn when we come out of battle
    public Vector3 overWorldPos;
    public Quaternion overWorldRot;

    public int currentScene;

    // Saves position of entry point to know where to spawn when entering or restarting scenes
    public Vector3 entryPos;
    public Vector3 entryRot;

    // This is a variable you can use if you want to know if the player should be able to move
    public bool canMove = true;

    public string attackingEnemy;

    // Controls if the game can start doing stuff yet or not
    public bool gameCanRun;

    #endregion
}