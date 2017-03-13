using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OverworldManager : MonoBehaviour
{
    // An image for a loading animation when you start a new scene
    public Image blackScreen;
    
    // Gets the player
    public GameObject player;
 
    // This is the script that prints messages
    public TextBoxHandler textBox;

    // Holds which save station that you interacted with
    [System.NonSerialized]
    public GameObject interactedSaveStation;
    // The buttons for choosing yes or no in the save window
    public GameObject choiceButtons;

    public List<LootObject> chests;

    void Awake()
    {
        if (PlayerSingleton.instance.loaded)
        {
            player.transform.position = new Vector3(PlayerSingleton.instance.savePosX, PlayerSingleton.instance.savePosY, PlayerSingleton.instance.savePosZ);
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            PlayerSingleton.instance.loaded = false;
        }
        else if (!OverworldEnemySingleton.instance.backFromCombat && PlayerSingleton.instance.entryPos != Vector3.zero)
        {
            player.transform.position = PlayerSingleton.instance.entryPos;
            player.transform.rotation = Quaternion.Euler(PlayerSingleton.instance.entryRot);
        }

        PlayerSingleton.instance.currentScene = Application.loadedLevel;

        // Resets the black screen so we can remove it with fillamount
        blackScreen.fillAmount = 1;
    }

	void Start ()
    {
        // Finds all game objects tagged as Enemy and adds them to the list of enemies
        OverworldEnemySingleton.instance.enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
        // Sorts the enemies by their names so that everyone will have the same index everytime the scene is entered
        OverworldEnemySingleton.instance.enemies = OverworldEnemySingleton.instance.enemies.OrderBy(enemy => enemy.name).ToList();
        
        chests = (FindObjectsOfType(typeof(LootObject)) as LootObject[]).ToList();
        chests = chests.OrderBy(chest => chest.name).ToList();


        // If the player hasn't returned from combat, the list with bools will have the same amount of items as the enemy-list
        // This makes sure that the list isn't reset and that the amount of enemies can vary from scene to scene
        if (!OverworldEnemySingleton.instance.backFromCombat)
        {
            OverworldEnemySingleton.instance.shouldDestroy = new List<bool>(new bool[OverworldEnemySingleton.instance.enemies.Count]);
            OverworldEnemySingleton.instance.backFromCombat = false;
        }

        for (int i = 0; i < OverworldEnemySingleton.instance.enemies.Count; i++)
        {
            // Deactivates all enemies that has been defeated in combat
            if (OverworldEnemySingleton.instance.shouldDestroy[i])
                OverworldEnemySingleton.instance.enemies[i].SetActive(false);
        }

        if(OverworldEnemySingleton.instance.fled)
        {
            OverworldEnemySingleton.instance.enemies[OverworldEnemySingleton.instance.currentEnemyIndex].GetComponentInChildren<EnemyAiMovement>().isFrozen = true;
            OverworldEnemySingleton.instance.fled = false;
        }

        if (PlayerSingleton.instance.currentScene == 5)
        {
            System.Array.Resize(ref PlayerSingleton.instance.chestOpen_lightForest, chests.Count);

            for (int i = 0; i < PlayerSingleton.instance.chestOpen_lightForest.Length; i++)
            {
                if (PlayerSingleton.instance.chestOpen_lightForest[i])
                {
                    chests[i].OpenAnim();
                    chests[i].InActivateTreasure();
                    chests[i].gameObject.tag = "Uninteractable";
                }
            }
        }
        else if (PlayerSingleton.instance.currentScene == 6)
        {
            System.Array.Resize(ref PlayerSingleton.instance.chestOpen_darkForest, chests.Count);

            for (int i = 0; i < PlayerSingleton.instance.chestOpen_darkForest.Length; i++)
            {
                if (PlayerSingleton.instance.chestOpen_darkForest[i])
                {
                    chests[i].OpenAnim();
                    chests[i].InActivateTreasure();
                    chests[i].gameObject.tag = "Uninteractable";
                }
            }
        }
        
        // Starts removing the black screen
        StartCoroutine(RemoveBlackScreen());
    }

    // Removes the black screen with fill amount
    IEnumerator RemoveBlackScreen()
    {
        // Pauses the game
        PlayerSingleton.instance.canMove = !PlayerSingleton.instance.canMove;        
        
        // Also sets it so the game can't run when loading this black screen
        PlayerSingleton.instance.gameCanRun = false;

        // Looping until the black screen is gone
        while (blackScreen.fillAmount > 0)
        {
            // Removes part of the blackscreen
            blackScreen.fillAmount -= 1f * Time.deltaTime;

            yield return null;
        }

        // Unpauses the game
        PlayerSingleton.instance.canMove = !PlayerSingleton.instance.canMove;        
        // Sets it so the game can start running
        PlayerSingleton.instance.gameCanRun = true;
    }


    public void SaveChoice(bool yes)
    {
        Debug.Log(name);
        choiceButtons.SetActive(false);
        PlayerSingleton.instance.gameCanRun = true;
        PlayerSingleton.instance.canMove = true;

        if (yes)
        {
            PlayerSingleton.instance.savePosX = interactedSaveStation.transform.GetChild(0).position.x;
            PlayerSingleton.instance.savePosY = interactedSaveStation.transform.GetChild(0).position.y;
            PlayerSingleton.instance.savePosZ = interactedSaveStation.transform.GetChild(0).position.z;
            string[] text = new string[1];
            text[0] = "Your progress was saved!";
            PlayerSingleton.instance.Save();
            textBox.PrintMessage(text, "", null, null);
        }
    }
}
