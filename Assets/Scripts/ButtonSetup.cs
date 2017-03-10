using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSetup : MonoBehaviour
{
    Button[] allButtonsInScene;

    public AudioClip clickSound;

	void Start ()
    {
        // Gets all objects with the button component in scene
        allButtonsInScene = Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[];

        for (int i = 0; i < allButtonsInScene.Length; i++)
        {
            allButtonsInScene[i].onClick.AddListener(() => AudioHelper.PlaySound(clickSound));

            allButtonsInScene[i].gameObject.AddComponent<EventTrigger>();

            EventTrigger trigger = allButtonsInScene[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { OnSelect((BaseEventData) eventData); });
            trigger.triggers.Add(entry);
        }
	}
	
	void OnSelect(BaseEventData eventData)
    {
        AudioHelper.PlayPitched(clickSound, 0.8f, 0.8f);
    }
}
