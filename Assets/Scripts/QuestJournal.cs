using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class QuestJournal : MonoBehaviour
{
    public Button[] questButtons;
    public Text questInfoText;
    public Color completedTextColor;

	void Start ()
    {
        // Add listeners for the OnSelect event so they call OnQuestButtonSelect
        for (int i = 0; i < questButtons.Length; i++)
        {
            EventTrigger trigger = questButtons[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { OnQuestButtonSelect((BaseEventData)eventData);});
            
            trigger.triggers.Add(entry);
        }
	}

    void Update()
    {
        for (int i = 0; i < questButtons.Length; i++)
        {
            if (i <= PlayerSingleton.instance.activeQuestIndex && PlayerSingleton.instance.activeQuestIndex >= 0)
            {
                questButtons[i].interactable = true;

                if (i < PlayerSingleton.instance.activeQuestIndex)
                {
                    questButtons[i].transform.GetChild(0).GetComponent<Text>().color = completedTextColor;
                    questButtons[i].transform.GetChild(0).GetComponent<Text>().text = QuestDatabase.quests[i].title + "\n(COMPLETED)";
                }
                else
                    questButtons[i].transform.GetChild(0).GetComponent<Text>().text = QuestDatabase.quests[i].title;
            }
            else
            {
                questButtons[i].interactable = false;
                questButtons[i].transform.GetChild(0).GetComponent<Text>().text = null;
            }
        }
    }

    // Displays the chosen quest's description
    void OnQuestButtonSelect(BaseEventData eventData)
    {
        int index = questButtons.ToList().IndexOf(eventData.selectedObject.GetComponent<Button>());

        questInfoText.text = "";

        for (int i = 0; i < QuestDatabase.quests[index].description.Length; i++)
        {
            if (i <= QuestDatabase.quests[index].questStage)
            {
                if(i > 0)
                    questInfoText.text += "\n\n";
                questInfoText.text += QuestDatabase.quests[index].description[i];
            }
        }
        
    }

}
