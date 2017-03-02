using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatStart : MonoBehaviour
{
    public Image blackScreen;
    public float waitTime = 1.0f;

    void Start()
    {
        if(PlayerSingleton.instance.overWorldPos != Vector3.zero && OverworldEnemySingleton.instance.backFromCombat)
        {
            transform.position = PlayerSingleton.instance.overWorldPos;
            transform.rotation = PlayerSingleton.instance.overWorldRot;
            OverworldEnemySingleton.instance.backFromCombat = false;
        }
    }

    //When an object has the tag "Player" and touch an enemy
    //with the "enemyOverworld" tag, "fill" is true
	void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.layer == 8 && Time.timeScale > 0)
        {
            StartCoroutine(FillScreen());
            PlayerSingleton.instance.attackingEnemy = enemy.tag;
            PlayerSingleton.instance.overWorldPos = this.transform.position;
            PlayerSingleton.instance.overWorldRot = this.transform.rotation;

            // Saves the index of the enemy encountered so that we know which to deactivate if it is defeated in combat
            OverworldEnemySingleton.instance.currentEnemyIndex = OverworldEnemySingleton.instance.enemies.IndexOf(enemy.transform.parent.parent.gameObject);
        }
    }

    IEnumerator FillScreen ()
    {
        PlayerSingleton.instance.gameCanRun = false;
        // Pauses the game if it's not already paused and vice versa
        PlayerSingleton.instance.canMove = PlayerSingleton.instance.canMove ? false : true;

        Time.timeScale = 0;

        while (blackScreen.fillAmount < 1)
        {
            blackScreen.fillAmount += 0.025f;
            yield return null;
        }
        PlayerSingleton.instance.gameCanRun = true;

        // Pauses the game if it's not already paused and vice versa
        PlayerSingleton.instance.canMove = PlayerSingleton.instance.canMove ? false : true;
        Time.timeScale = 1;

        // When the blackscreen is done the battle scene loads
        if (PlayerSingleton.instance.currentScene == 5)
            SceneManager.LoadScene("Battle_scene");
        else if (PlayerSingleton.instance.currentScene == 6)
            SceneManager.LoadScene("Battle_Scene_dark");
    }
}
