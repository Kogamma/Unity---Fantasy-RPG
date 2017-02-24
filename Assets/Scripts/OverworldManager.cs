using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    public List<GameObject> sortedEnemyList;

	void Start ()
    {
        OverworldEnemySingleton.instance.enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
        sortedEnemyList = OverworldEnemySingleton.instance.enemies.OrderBy(enemy => enemy.name).ToList();

        if (!OverworldEnemySingleton.instance.backFromCombat)
            OverworldEnemySingleton.instance.shouldDestroy = new List<bool>(new bool[sortedEnemyList.Count]);

        for (int i = 0; i < sortedEnemyList.Count; i++)
        {
            if (OverworldEnemySingleton.instance.shouldDestroy[i])
            {
                sortedEnemyList[i].SetActive(false);
            }
        }
    }
}
