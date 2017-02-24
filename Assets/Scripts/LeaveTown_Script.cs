using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTown_Script : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        Application.LoadLevel("Forest_Scene_1");
    }
}
