using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    public int questToComplete;
    public enum TriggerType {COMPLETE, UPDATE};
    public TriggerType triggerType;
    public QuestDisplay questDisplay;

    //When you enter the trigger
    void OnTriggerEnter(Collider other)
    {
        //This if statement will run
        if (other.gameObject.tag == "Player")
        {
            if (PlayerSingleton.instance.activeQuestIndex == questToComplete && triggerType == TriggerType.COMPLETE)
            {
                questDisplay.CompleteQuest();
                Destroy(this.gameObject);
            }
            else if (triggerType == TriggerType.UPDATE)
            {
                questDisplay.UpdateQuest();
                Destroy(this.gameObject);
            }
        }
    }
}
