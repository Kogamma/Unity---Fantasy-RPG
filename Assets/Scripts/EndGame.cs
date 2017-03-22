using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Image blackImage;
    public GameObject credits;
    public AudioClip creditsMusic;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MusicHelper.Stop();
            StartCoroutine(FillBlackScreen());
        }
    }


    IEnumerator FillBlackScreen()
    {
        blackImage.fillAmount = 0;
        PlayerSingleton.instance.canMove = false;
        PlayerSingleton.instance.gameCanRun = false;

        while (blackImage.fillAmount < 1)
        {
            blackImage.fillAmount += 1f * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(4f);

        MusicHelper.PlaySound(creditsMusic);
        credits.SetActive(true);

        yield return new WaitForSeconds(48f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu_Scene");
    }
}
