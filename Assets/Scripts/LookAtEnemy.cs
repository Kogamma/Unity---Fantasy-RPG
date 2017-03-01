using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    private Vector3 originPos;  // The original position of the camera
    public Vector3 target;      // Target to look at
    private int offset = 0;     // Distance from target


    void Start ()
    {
        originPos = transform.position;
    }
    

    void Update ()
    {
        if (PlayerSingleton.instance.attackingEnemy == "Troll")
            offset = 1;

        else if (PlayerSingleton.instance.attackingEnemy == "Boss")
            offset = 2;

        // Looks slightly above the target
        transform.LookAt(new Vector3(target.x, target.y + 0.5f, target.z));
        // Sets position of the camera further away from bigger enemies, if needed
        transform.position = new Vector3(originPos.x - offset, originPos.y + offset, originPos.z - offset);
    }
}
