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

    public float playerDmg = 1;
    public int playerMagicDmg = 1;
    public int playerHealth = 10;
    public int playerExp = 0;
    public int playerInt = 5;
    public int playerStr = 5;
    public int playerDex = 5;
    public int playerLuck = 5;
    public int playerMana = 10;

    public int currentDmg;
    public bool playerAttacked = false;

    public Transform overWorldTransform;
    public bool canMove = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
