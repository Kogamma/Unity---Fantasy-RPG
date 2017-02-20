using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatStart : MonoBehaviour {

    public Image blackScreen;
    bool fill = false;
    public float waitTime = 1.0f;

    void Start()
    {
        if(PlayerSingleton.instance.overWorldTransform == null)
        {
            PlayerSingleton.instance.overWorldTransform = this.transform;
        }
    }

    //When "fill" it true, a blackscreen starts to fill
    //the screen. When it's done it loads the battle scene
    void Update()
    {
        if(fill)
        {
            blackScreen.fillAmount += 1.0f / waitTime * Time.deltaTime;
            if (blackScreen.fillAmount >= 1)
            {
                //When the blackscreen is done
                //the battle scene loads
                SceneManager.LoadScene("Battle_scene");
                PlayerSingleton.instance.overWorldTransform = this.transform;
            }
        }
    }

    //When an object has the tag "Player" and touch an enemy
    //with the "enemyOverworld" tag, "fill" is true
	void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "enemyOverworld")
        {
            fill = true;
        }
    }
}
