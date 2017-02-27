using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemySingleton : MonoBehaviour
{
    public static OverworldEnemySingleton instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject prefab = (GameObject)Resources.Load("OverworldEnemySingleton");
                GameObject created = Instantiate(prefab);
                DontDestroyOnLoad(created);
                m_instance = created.GetComponent<OverworldEnemySingleton>();
            }

            return m_instance;
        }
    }

    private static OverworldEnemySingleton m_instance;

    // List of enemies
    public List<GameObject> enemies;

    // List of bools telling if an enemy should be dead
    // Since the enemy list will be sorted, these bools will share the index with the enemies
    [System.NonSerialized] public List<bool> shouldDestroy; 
    
    // The index of the enemy that is encountered in combat
    public int currentEnemyIndex;

    // Has the player returned from combat?
    public bool backFromCombat = false;

    // Has the player fled from combat?
    public bool fled = false;
}
