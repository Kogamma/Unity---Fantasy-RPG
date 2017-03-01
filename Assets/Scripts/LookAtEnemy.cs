using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public GameObject target;   // Target to look at
    public int offset;      // Distance from target

    
    void Update()
    {
        if (PlayerSingleton.instance.attackingEnemy == "Slime")
            offset = 2;

        else if (PlayerSingleton.instance.attackingEnemy == "Zombie")
            offset = 2;

        else if (PlayerSingleton.instance.attackingEnemy == "Bat")
            offset = 2;

        else if (PlayerSingleton.instance.attackingEnemy == "Ghost")
            offset = 2;

        else if (PlayerSingleton.instance.attackingEnemy == "Troll")
            offset = 2;

        else if (PlayerSingleton.instance.attackingEnemy == "Boss")
            offset = 2;


        //transform.position = target.transform.position + transform.loca offset;

        transform.LookAt(target.transform.position);
    }
}
