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

    public List<GameObject> enemies;

    [System.NonSerialized] public List<bool> shouldDestroy;

    public int currentEnemyIndex;

    public bool backFromCombat = false;
}
