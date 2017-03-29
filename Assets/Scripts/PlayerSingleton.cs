using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton instance
    {
        get
        {
            // If an instance of this script doesn't exist already
            if (m_instance == null)
            {
                // Creates a GameObject based of this class
                GameObject prefab = (GameObject)Resources.Load("PlayerSingleton");
                // Instantiates the created GameObject
                GameObject created = Instantiate(prefab);
                // Prevents the object from being destroyed when changing scenes
                DontDestroyOnLoad(created);
                // Assigns an instance of this script
                m_instance = created.GetComponent<PlayerSingleton>();
            }

            return m_instance;
        }
    }

    private static PlayerSingleton m_instance;

    /* Variables to be saved */

    // Sound volumes set in options
    public float sfxVol = 0.5f;
    public float musicVol = 0.5f;

    // How much damage and magic damage the player does
    public float playerDmg = 1;
    public int playerMagicDmg = 1;

    // How many hitpoints the player has
    public int playerMaxHealth = 10;
    public int playerHealth = 10;

    public int level = 1;
    public int currentXPNeeded = 100;
    public int skillPoints = 0;

    // How much experience points the player has in total
    public int playerExp = 0;
    
    // How much of the intelligence stat the player has
    public int playerInt = 5;
    // How much of the strength stat the player has
    public int playerStr = 5;
    // How much of the dexterity stat the player has
    public int playerVit = 5;
    // How much of the luck stat the player has
    public int playerLuck = 5;

    // How much mana points the player has
    public int playerMana = 10;
    public int playerMaxMana = 10;

    // The players inventory
    public List<string> playerInventory = new List<string>();
    public List<int> inventoryAmounts = new List<int>();

    // The current maximum size of slots in the inventory
    public int inventorySize = 15;
    
    // Creates the equipped items array and fils it with default value items that means the slot is empty
    public string[] equippedItems = new string[6] {"null", "null", "null", "null", "null", "null"};

    // The non-combat scene that the player is in, used for returning after combat and after the save file is loaded
    public string currentScene = "Starting_Gate";

    // The spawn coordinates of the save station that the player has saved to, used for positioning the player at the right statue after load of save file
    // All coordinates are saved separatly since Vector3 isn't serializable
    public float savePosX = -103.46f;
    public float savePosY = 4.986f;
    public float savePosZ = 8.86f;

    // Arrays containing bools that tells which chests is open
    public bool[] chestOpen_lightForest;
    public bool[] chestOpen_darkForest;

    // Array containing bools that tells which areas of the game that has been explored
    public bool[] areaExplored = new bool[5];

    // The currently active quest number
    public int activeQuestIndex = -1;

    // The stages of each quest
    public List<int> questStages;
    
    /* End of variables to be saved */

    #region In-game variables

    // Variables used for in-combat purposes to see what the current damage of the player is,
    // and if the player has attacked this round or not

    //public int currentDmg;
    public bool playerAttacked = false;

    public bool poisoned = false;
    public bool confused = false;
    public bool clairvoyance = false;
    public bool onFire = false;

    // Saves the position and rotation of the player when they go into battle to know where to spawn when we come out of battle
    public Vector3 overWorldPos;
    public Quaternion overWorldRot;

    // Saves position of entry point to know where to spawn when entering or restarting scenes
    public Vector3 entryPos;
    public Vector3 entryRot;

    // This is a variable you can use if you want to know if the player should be able to move
    public bool canMove = true;

    public string attackingEnemy;

    // Controls if the game can start doing stuff yet or not
    public bool gameCanRun = false;
    
    // Used to check if the game recently was loaded, then the player will be moved to the proper position.
    public bool loaded = false;
    #endregion


    // Saves player data and progress
    public void Save ()
    {
        // Binary formatter that will serialize the PlayerData class
        BinaryFormatter bf = new BinaryFormatter();

        // The file that the data will be stored in
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        // An instance of PlayerData
        PlayerData data = new PlayerData();

        // Assigns all variables in PlayerData to its correspondants values in PlayerSingleton
        data.playerMaxHealth = playerMaxHealth;
        data.playerHealth = playerHealth;
        data.playerMaxMana = playerMaxMana;
        data.playerMana = playerMana;
        data.playerDmg = playerDmg;
        data.playerMagicDmg = playerMagicDmg;
        data.playerExp = playerExp;
        data.level = level;
        data.currentXPNeeded = currentXPNeeded;
        data.skillPoints = skillPoints;
        data.playerInt = playerInt;
        data.playerVit = playerVit;
        data.playerStr = playerStr;
        data.playerLuck = playerLuck;
        data.playerInventory = playerInventory;
        data.inventoryAmounts= inventoryAmounts;
        data.equippedItems = equippedItems;
        data.inventorySize = inventorySize;
        data.currentScene = currentScene;
        data.savePosX = savePosX;
        data.savePosY = savePosY;
        data.savePosZ = savePosZ;
        data.chestOpen_lightForest = chestOpen_lightForest;
        data.chestOpen_darkForest = chestOpen_darkForest;
        data.areaExplored = areaExplored;
        data.activeQuestIndex = activeQuestIndex;
        data.questStages = questStages;

        // Stores the data in the file
        bf.Serialize(file, data);

        // Closes file
        file.Close();
    }


    // Loads savefile
    public void Load (bool loadScene)
    {
        // Checks if a savefile exists
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            // Binary formatter that will deserialize the datafile
            BinaryFormatter bf = new BinaryFormatter();

            // An instance of the savefile
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            // An instance of the PlayerData class that is initialized with the savefiles data
            PlayerData data = (PlayerData)bf.Deserialize(file);

            // Closes file
            file.Close();

            // Assigns all variables to be saved/loaded in PlayerSingleton to its correspondants values in PlayerData
            playerMaxHealth = data.playerMaxHealth;
            playerHealth = data.playerHealth;
            playerMaxMana = data.playerMaxMana;
            playerMana = data.playerMana;
            playerDmg = data.playerDmg;
            playerMagicDmg = data.playerMagicDmg;
            playerExp = data.playerExp;
            level = data.level;
            currentXPNeeded = data.currentXPNeeded;
            skillPoints = data.skillPoints;
            playerInt = data.playerInt;
            playerVit = data.playerVit;
            playerStr = data.playerStr;
            playerLuck = data.playerLuck;
            playerInventory = data.playerInventory;
            inventoryAmounts = data.inventoryAmounts;
            equippedItems = data.equippedItems;
            inventorySize = data.inventorySize;
            currentScene = data.currentScene;
            savePosX = data.savePosX;
            savePosY = data.savePosY;
            savePosZ = data.savePosZ;
            chestOpen_lightForest = data.chestOpen_lightForest;
            chestOpen_darkForest = data.chestOpen_lightForest;
            areaExplored = data.areaExplored;
            activeQuestIndex = data.activeQuestIndex;
            questStages = data.questStages;

            // The savefile has been loaded
            loaded = true;

            // Unpauses the game
            canMove = true;

            // Loads the scene that was open when the game was saved if the scene should be loaded
            if(loadScene)
                UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
        }
    }


    // Saves options
    public void SaveOptions()
    {
        // Sets a key that will tell if there is any saved options
        PlayerPrefs.SetInt("HasOptionsSave", 1);

        // Sets the volume values to keys
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
        PlayerPrefs.SetFloat("sfxVolume", sfxVol);

    }


    // Loads options
    public void LoadOptions()
    {   
        // If there are any saved options
        if (PlayerPrefs.HasKey("HasOptionsSave"))
        {
            // Assigns the saved values to the volume variables
            musicVol = PlayerPrefs.GetFloat("MusicVolume");
            sfxVol = PlayerPrefs.GetFloat("sfxVolume");

            // Updates volume
            MusicHelper.UpdateVolume();
            AudioHelper.UpdateVolume();
        }
    }


    [System.Serializable]
    class PlayerData
    {
        // How much damage and magic damage the player does
        public float playerDmg;
        public int playerMagicDmg;

        // How many hitpoints the player has
        public int playerMaxHealth;
        public int playerHealth;

        public int level;
        public int currentXPNeeded = 100;
        public int skillPoints;

        // How much experience points the player has in total
        public int playerExp;

        // How much of the intelligence stat the player has
        public int playerInt;
        // How much of the strength stat the player has
        public int playerStr;
        // How much of the dexterity stat the player has
        public int playerVit;
        // How much of the luck stat the player has
        public int playerLuck;

        // How much mana points the player has
        public int playerMana;
        public int playerMaxMana;

        public List<string> playerInventory;
        public List<int> inventoryAmounts;

        // The current maximum size of slots in the inventory
        public int inventorySize;

        // Creates the equipped items array and fils it with default value items that means the slot is empty
        public string[] equippedItems;

        // The non-combat scene that the player is in, used for returning after combat and after the save file is loaded
        public string currentScene;

        // The coordinates to spawn at of the save station that the player has saved to, used for positioning the player at the right statue after load of save file
        // All coordinates are save separatly since Vector3 isn't serializable
        public float savePosX;
        public float savePosY;
        public float savePosZ;

        // Arrays containing bools that tells which chests is open
        public bool[] chestOpen_lightForest;
        public bool[] chestOpen_darkForest;

        // Array containing bools that tells which areas of the game that has been explored
        public bool[] areaExplored;

        public int activeQuestIndex;

        public List<int> questStages;
    }
 }