using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestEvent_Town : MonoBehaviour
{
    public GameObject lumberjack_birch;
    public Image blackScreen;
    public QuestDisplay questDisplay;
    

	// Use this for initialization
	void Start ()
    {
        if (PlayerSingleton.instance.questStages[0] >= 2 || PlayerSingleton.instance.activeQuestIndex >= 1)
            lumberjack_birch.SetActive(false);
	}


    public void StartRemoveLumberjack()
    {
        StartCoroutine(RemoveLumberjack());
    }


    IEnumerator RemoveLumberjack()
    {
        blackScreen.fillAmount = 0;
        PlayerSingleton.instance.canMove = false;
        PlayerSingleton.instance.gameCanRun = false;

        while (blackScreen.fillAmount < 1)
        {
            blackScreen.fillAmount += 1f * Time.deltaTime;

            yield return null;
        }

        lumberjack_birch.SetActive(false);

        yield return new WaitForSeconds(1f);

        while (blackScreen.fillAmount > 0)
        {
            blackScreen.fillAmount -= 1f * Time.deltaTime;

            yield return null;
        }

        PlayerSingleton.instance.canMove = true;
        PlayerSingleton.instance.gameCanRun = true;

        PlayerSingleton.instance.questStages[0] = 2;
        StartCoroutine(questDisplay.DisplayQuestUpdate());
    }
}
