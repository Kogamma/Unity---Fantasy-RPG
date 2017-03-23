using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent_Gate : MonoBehaviour
{
    public GameObject questGiver;
    public GameObject gateExit;
    public TextBoxHandler textBox;
    public QuestDisplay questDisplay;
    public Transform player;

    
	void Start ()
    {
        if (PlayerSingleton.instance.activeQuestIndex >= 0)
            questGiver.SetActive(false);
        if (PlayerSingleton.instance.activeQuestIndex >= 3)
            gateExit.SetActive(true);
        if (PlayerSingleton.instance.activeQuestIndex <= -1)
            player.rotation = Quaternion.Euler(0, 30, 0);
	}


    public void QuestGiverMessage()
    {


        string[] text = new string[6]
        {
            "Oh, so you've finally woken up?",
            "You came through The Gate Between Worlds a couple of hours ago, but you were unconscious.",
            "The gate is closed for now, but lucky for you I know how to open it again.",
            "There is a key to open it, but it's in the posession of a mighty beast in The Dark Forest.",
            "To get to The Dark Forest, you must first get through The Light Forest which is just east from here.",
            "This world is full of monsters so be careful!"
        };
        textBox.PrintMessage(text, "Mysterious Man", this.gameObject, "CompleteQuest");
    }


    public void CompleteQuest()
    {
        PlayerSingleton.instance.activeQuestIndex = 0;
        PlayerSingleton.instance.questStages.Add(0);
        StartCoroutine(FirstQuestUpdate());
    }


    public IEnumerator FirstQuestUpdate()
    {
        questDisplay.canInput = false;

        questDisplay.questBox.SetActive(true);

        questDisplay.completedQuestText.enabled = false;
        questDisplay.completedText.enabled = false;

        questDisplay.newQuestText.text = "New Quest:\n" + QuestDatabase.quests[PlayerSingleton.instance.activeQuestIndex].title;
        questDisplay.newQuestText.enabled = true;

        if (PlayerSingleton.instance.canMove)
            yield return new WaitForSeconds(2f);
        else
            yield return null;

        questDisplay.questBox.SetActive(false);
        questDisplay.newQuestText.enabled = false;

        questDisplay.canInput = true;
    }
}
