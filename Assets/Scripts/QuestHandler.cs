using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{
    
    public Text completedText;
    public Text completedQuestText;
    public Text newQuestText;
    public Text currentQuest;
    public GameObject questBox;

    private bool _hideQuestBox = false;
    private bool canInput = true;


    void Update()
    {
        if (_hideQuestBox)
        {
            questBox.SetActive(false);
            StopCoroutine(DisplayQuestUpdate());
        }

        if (canInput)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                currentQuest.text = "Quest:\n" + QuestDatabase.quests[PlayerSingleton.instance.activeQuestIndex].title;
                currentQuest.enabled = true;
                questBox.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                questBox.SetActive(false);
                currentQuest.enabled = false;
            }
        }
        else
            currentQuest.enabled = false;
    }


    public void CompleteQuest()
    {
        PlayerSingleton.instance.activeQuestIndex++;

        PlayerSingleton.instance.activeQuestIndex = Mathf.Clamp(PlayerSingleton.instance.activeQuestIndex, 0, QuestDatabase.quests.Count);

        QuestDatabase.UpdateQuestLog();

        StartCoroutine(DisplayQuestUpdate());
    }


    public void HideQuestBox ()
    {
        _hideQuestBox = true;
    }


    public IEnumerator DisplayQuestUpdate()
    {
        canInput = false;

        questBox.SetActive(true);

        if (PlayerSingleton.instance.activeQuestIndex > 0)
        {
            completedQuestText.text = "Quest: " + QuestDatabase.quests[PlayerSingleton.instance.activeQuestIndex - 1].title;

            completedQuestText.enabled = true;

            yield return new WaitForSeconds(1f);

            completedText.enabled = true;
        }

        yield return new WaitForSeconds(2f);

        completedQuestText.enabled = false;
        completedText.enabled = false;

        newQuestText.text = "New Quest:\n" + QuestDatabase.quests[PlayerSingleton.instance.activeQuestIndex].title;
        newQuestText.enabled = true;

        yield return new WaitForSeconds(2f);

        questBox.SetActive(false);
        newQuestText.enabled = false;

        canInput = true;
    }
}



