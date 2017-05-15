using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatStart : MonoBehaviour
{
    public Image blackScreen;
    

    //When an object has the tag "Player" and touch an enemy
    //with the "enemyOverworld" tag, "fill" is true
	void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.layer == 8 && PlayerSingleton.instance.canMove)
        {
            PlayerSingleton.instance.attackingEnemy = enemy.tag;
            StartCoroutine(FillScreen());
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
        PlayerSingleton.instance.canMove = !PlayerSingleton.instance.canMove;

        Time.timeScale = 0;

        while (blackScreen.fillAmount < 1)
        {
            blackScreen.fillAmount += 1f * Time.unscaledDeltaTime;
           
            yield return null;
        }
        PlayerSingleton.instance.gameCanRun = true;

        // Pauses the game if it's not already paused and vice versa
        PlayerSingleton.instance.canMove = !PlayerSingleton.instance.canMove;
        Time.timeScale = 1;
        Debug.Log("hallå");
        // Pauses the music before loading new scene
        MusicHelper.Stop();

        string currentScene = PlayerSingleton.instance.currentScene;

        // When the blackscreen is done the battle scene loads
        if (currentScene == "Forest_Scene_1" || currentScene == "Starting_Gate")
            SceneManager.LoadScene("Battle_scene");
        else if (currentScene == "dark_forest_1" && PlayerSingleton.instance.attackingEnemy == "DragonBoar")
        {
            SceneManager.LoadScene("Boss_Battle_Scene");
            PlayerSingleton.instance.canMove = false;
        }
        else if (currentScene == "dark_forest_1")
            SceneManager.LoadScene("Battle_Scene_dark");
    }
}
