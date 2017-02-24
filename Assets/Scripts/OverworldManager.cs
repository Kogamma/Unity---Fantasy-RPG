using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
	void Start ()
    {
        // Finds all game objects tagged as Enemy and adds them to the list of enemies
        OverworldEnemySingleton.instance.enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
        // Sorts the enemies by their names so that everyone will have the same index everytime the scene is entered
        OverworldEnemySingleton.instance.enemies = OverworldEnemySingleton.instance.enemies.OrderBy(enemy => enemy.name).ToList();

        // If the player hasn't returned from combat, the list with bools will have the same amount of items as the enemy-list
        // This makes sure that the list isn't reset and that the amount of enemies can vary from scene to scene
        if (!OverworldEnemySingleton.instance.backFromCombat)
            OverworldEnemySingleton.instance.shouldDestroy = new List<bool>(new bool[OverworldEnemySingleton.instance.enemies.Count]);

        for (int i = 0; i < OverworldEnemySingleton.instance.enemies.Count; i++)
        {
            // Deactivates all enemies that has been defeated in combat
            if (OverworldEnemySingleton.instance.shouldDestroy[i])
                OverworldEnemySingleton.instance.enemies[i].SetActive(false);
        }
    }
}
