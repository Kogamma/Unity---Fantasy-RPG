using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.L))
            PlayerSingleton.instance.Load();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerSingleton.instance.playerMaxHealth++;
            PlayerSingleton.instance.playerMaxMana += 5;
            PlayerSingleton.instance.playerDmg += 3;
        }
	}
}
