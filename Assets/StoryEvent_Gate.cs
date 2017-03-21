using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent_Gate : MonoBehaviour
{
    public GameObject questGiver;
    public TextBoxHandler textBox;
    public QuestDisplay questDisplay;
  

	// Use this for initialization
	void Start ()
    {
        if (PlayerSingleton.instance.activeQuestIndex >= 0)
            questGiver.SetActive(false);
	}


    public void QuestGiverMessage()
    {
        string[] text = new string[6]
        {
            "Oh, so you've finally woken up?",
            "You came through The Gate Between Worlds a couple of hours ago, but you were unconscious.",
            "The gate is closed now but lucky for you I know how to open it again.",
            "There is a key to open it, but it's in the posession of a mighty beast in The Dark Forest.",
            "To get to The Dark Forest you must first get through The Light Forest which is just east from here.",
            "This world is full of monsters so be careful!"
        };
        textBox.PrintMessage(text, "Mysterious Man", questDisplay.gameObject, "CompleteQuest");
    }
}
