using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
	void Start ()
    {
        OverworldEnemySingleton.instance.enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
        OverworldEnemySingleton.instance.enemies.OrderBy(enemy => enemy.name);

        if (!OverworldEnemySingleton.instance.backFromCombat)
            OverworldEnemySingleton.instance.shouldDestroy = new List<bool>(new bool[OverworldEnemySingleton.instance.enemies.Count]);

        for (int i = 0; i < OverworldEnemySingleton.instance.enemies.Count; i++)
        {
            if (OverworldEnemySingleton.instance.shouldDestroy[i])
            {
                OverworldEnemySingleton.instance.enemies[i].SetActive(false);
            }
        }
    }
}
