using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    
    public Text completedText;
    public Text completedQuestText;
    public Text newQuestText;
    public Text currentQuest;
    public GameObject questBox;

    private bool canInput = true;


    void Update()
    {
        if (!PlayerSingleton.instance.canMove)
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


    public IEnumerator DisplayQuestUpdate()
    {
        canInput = false;

        questBox.SetActive(true);

        if (PlayerSingleton.instance.activeQuestIndex > 0)
        {
            completedQuestText.text = "Quest: " + QuestDatabase.quests[PlayerSingleton.instance.activeQuestIndex - 1].title;

            completedQuestText.enabled = true;

            if (PlayerSingleton.instance.canMove)
                yield return new WaitForSeconds(1f);
            else
                yield return null;
            completedText.enabled = true;
        }

        if (PlayerSingleton.instance.canMove)
            yield return new WaitForSeconds(2f);
        else
            yield return null;

        completedQuestText.enabled = false;
        completedText.enabled = false;

        newQuestText.text = "New Quest:\n" + QuestDatabase.quests[PlayerSingleton.instance.activeQuestIndex].title;
        newQuestText.enabled = true;

        if (PlayerSingleton.instance.canMove)
            yield return new WaitForSeconds(2f);
        else
            yield return null;

        questBox.SetActive(false);
        newQuestText.enabled = false;

        canInput = true;
    }
}



