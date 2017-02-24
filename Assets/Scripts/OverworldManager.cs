using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
	void Start ()
    {
        OverworldEnemySingleton.instance.enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();

        if (OverworldEnemySingleton.instance.shouldDestroy && OverworldEnemySingleton.instance.currentEnemy != null)
        {
            int index = OverworldEnemySingleton.instance.enemies.IndexOf(OverworldEnemySingleton.instance.currentEnemy);
            OverworldEnemySingleton.instance.enemies[index].SetActive(false);
            OverworldEnemySingleton.instance.shouldDestroy = false;
        }
    }

    GameObject CheckObj (GameObject obj)
    {
        return obj;
    }
}
