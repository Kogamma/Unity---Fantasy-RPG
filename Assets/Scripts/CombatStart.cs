using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatStart : MonoBehaviour
{
    public Image blackScreen;
    bool fill = false;
    public float waitTime = 1.0f;

    void Start()
    {
        if(PlayerSingleton.instance.overWorldPos != Vector3.zero)
        {
            transform.position = PlayerSingleton.instance.overWorldPos;
            transform.rotation = PlayerSingleton.instance.overWorldRot;
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
            }
        }
    }

    //When an object has the tag "Player" and touch an enemy
    //with the "enemyOverworld" tag, "fill" is true
	void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.layer == 8)
        {
            fill = true;
            PlayerSingleton.instance.attackingEnemy = enemy.tag;
            PlayerSingleton.instance.overWorldPos = this.transform.position;
            PlayerSingleton.instance.overWorldRot = this.transform.rotation;
            OverworldEnemySingleton.instance.currentEnemyIndex = OverworldEnemySingleton.instance.enemies.IndexOf(enemy.transform.parent.parent.gameObject);
        }
    }
}
