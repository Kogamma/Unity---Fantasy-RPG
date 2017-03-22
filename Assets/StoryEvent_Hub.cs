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
            if(QuestDatabase.quests[0].questStage >= 1 || PlayerSingleton.instance.activeQuestIndex > 0)
                invisibleWall.SetActive(false);
        }
		if (PlayerSingleton.instance.activeQuestIndex > 1)
        {
            questGiver.SetActive(false);
            trees.SetActive(false);
            lumberjacks_postQuest.SetActive(true);
        }
	}


    void RemoveInvisibleWall()
    {
        invisibleWall.SetActive(false);
        QuestDatabase.quests[0].questStage++;
    }
}
