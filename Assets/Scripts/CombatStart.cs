using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatStart : MonoBehaviour {

    public Image blackScreen;
    bool fill = false;
    public float waitTime = 10.0f;


    //
    void Update()
    {
        if(fill)
        {
            blackScreen.fillAmount += 1.0f / waitTime * Time.deltaTime;
            if (blackScreen.fillAmount >= 1)
            {
                SceneManager.LoadScene("Battle_scene");

                
            }

        }
    }

	void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "enemyOverworld")
        {
            fill = true;
        }
    }
}
