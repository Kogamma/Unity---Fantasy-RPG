using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OverworldManager : MonoBehaviour
{
    public Image blackScreen;
    public GameObject player;
    public GameObject choiceButtons;
    public GameObject interactedSaveStation;
    public TextBoxHandler textBox;

    void Awake()
    {
        if (!OverworldEnemySingleton.instance.backFromCombat && PlayerSingleton.instance.entryPos != Vector3.zero)
        {
            player.transform.position = PlayerSingleton.instance.entryPos;
            player.transform.rotation = Quaternion.Euler(PlayerSingleton.instance.entryRot);
        }
        else if (PlayerSingleton.instance.loaded)
        {
            player.transform.position = new Vector3(PlayerSingleton.instance.savePosX, PlayerSingleton.instance.savePosY, PlayerSingleton.instance.savePosZ);
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            PlayerSingleton.instance.loaded = false;
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

        // Starts removing the black screen
        StartCoroutine(RemoveBlackScreen());
    }

    IEnumerator RemoveBlackScreen()
    {
        GetComponent<MenuController>().Pause();
        PlayerSingleton.instance.gameCanRun = false;

        while (blackScreen.fillAmount > 0)
        {
            blackScreen.fillAmount -= 0.025f;

            yield return null;
        }

        GetComponent<MenuController>().Pause();
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
            textBox.StartMessage(text, "", null, null);
        }
    }
}
