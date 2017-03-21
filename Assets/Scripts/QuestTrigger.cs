using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    public int questToComplete;
    public QuestDisplay questDisplay;

    //When you enter the trigger
    void OnTriggerEnter(Collider other)
    {
        //This if statement will run
        if (other.gameObject.tag == "Player" && PlayerSingleton.instance.activeQuestIndex == questToComplete)
        {
            questDisplay.CompleteQuest();
            Destroy(this.gameObject);
        }
    }
}
