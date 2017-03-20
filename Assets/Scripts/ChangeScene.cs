using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Vector3 spawnRot;

    public Image blackScreen;


    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.CompareTag("Player"))
        {
            PlayerSingleton.instance.entryPos = spawnPos;
            PlayerSingleton.instance.entryRot = spawnRot;
            StartCoroutine(FillScreen());
        }
    }

    IEnumerator FillScreen()
    {
        PlayerSingleton.instance.gameCanRun = false;
        PlayerSingleton.instance.canMove = false;

        while (blackScreen.fillAmount < 1)
        {
            blackScreen.fillAmount += 1f * Time.deltaTime;

            yield return null;
        }
        PlayerSingleton.instance.gameCanRun = true;

        // Pauses the game if it's not already paused and vice versa
        PlayerSingleton.instance.canMove = !PlayerSingleton.instance.canMove;
        Time.timeScale = 1;

        // Pauses the music before loading new scene
        MusicHelper.Stop();

        SceneManager.LoadScene(sceneName);
    }
}