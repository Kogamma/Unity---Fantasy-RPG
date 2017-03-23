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
            if (PlayerSingleton.instance.questStages[0] >= 1 || PlayerSingleton.instance.activeQuestIndex > 0)
            {
                invisibleWall.SetActive(false);
                questGiver.transform.GetChild(0).gameObject.SetActive(false);

                // Removes the method variables from the quest givers signscript so they can't keep calling this method
                questGiver.GetComponent<SignScript>().methodHolder = null;
                questGiver.GetComponent<SignScript>().methodName = null;

                questGiver.GetComponent<SignScript>().textPages = new string[1] { "Would you mind go getting Birch in the town so he can help me with these trees?" };

            }
            if (PlayerSingleton.instance.questStages[0] >= 2 || PlayerSingleton.instance.activeQuestIndex > 0)
            {
                questGiver.SetActive(false);
                trees.SetActive(false);
                lumberjacks_postQuest.SetActive(true);
            }
        }
	}


    void RemoveInvisibleWall()
    {
        // Removes the method variables from the quest givers signscript so they can't keep calling this method
        questGiver.GetComponent<SignScript>().methodHolder = null;
        questGiver.GetComponent<SignScript>().methodName = null;

        questGiver.GetComponent<SignScript>().textPages = new string[1] { "Would you mind go getting Birch in the town so he can help me with these trees?" };


        // Deactivates the exclamation mark
        questGiver.transform.GetChild(0).gameObject.SetActive(false);

        invisibleWall.SetActive(false);
        questDisplay.UpdateQuest();
    }
}
