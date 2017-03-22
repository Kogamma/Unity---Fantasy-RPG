using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent_Hub : MonoBehaviour
{
    public GameObject trees;
    public GameObject questGiver;
    public GameObject lumberjacks_postQuest;
    public GameObject invisibleWall;
    public QuestDisplay questDisplay;


	// Use this for initialization
	void Start ()
    {
        if (PlayerSingleton.instance.activeQuestIndex >= 0)
        {
            if(PlayerSingleton.instance.questStages[0] >= 1 || PlayerSingleton.instance.activeQuestIndex > 0)
                invisibleWall.SetActive(false);
        }
		if (PlayerSingleton.instance.questStages[0] >= 2 || PlayerSingleton.instance.activeQuestIndex > 0)
        {
            questGiver.SetActive(false);
            trees.SetActive(false);
            lumberjacks_postQuest.SetActive(true);
        }
	}


    void RemoveInvisibleWall()
    {
        invisibleWall.SetActive(false);
        PlayerSingleton.instance.questStages[0]++;
        questDisplay.DisplayQuestUpdate();
    }
}
