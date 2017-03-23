using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSetup : MonoBehaviour
{
    Button[] allButtonsInScene;

    public AudioClip clickSound;

	void Awake ()
    {
        // Makes the mouse cursor invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Button[] allObjects = Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[];
        // Do not activate if absolutely necessary, ask Oskar and Dennis first!
        for (int i = 0; i < allObjects.Length; i++)
        {
            EventTrigger[] triggers = allObjects[i].GetComponents<EventTrigger>();

            for (int j = 0; j < triggers.Length; j++)
            {
                DestroyImmediate(triggers[j], true);
            }
        }


        // Gets all objects with the button component in scene
        allButtonsInScene = Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[];

        for (int i = 0; i < allButtonsInScene.Length; i++)
        {
            allButtonsInScene[i].onClick.AddListener(() => AudioHelper.PlaySound(clickSound));

            if(allButtonsInScene[i].gameObject.GetComponent<EventTrigger>() == null)
                allButtonsInScene[i].gameObject.AddComponent<EventTrigger>();

            EventTrigger trigger = allButtonsInScene[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { OnSelect((BaseEventData) eventData); });

            //trigger.triggers[0] = entry;
            if (trigger.triggers.Count <= 1)
                trigger.triggers.Add(entry);
        }
	}
	
	void OnSelect(BaseEventData eventData)
    {
        AudioHelper.PlayPitched(clickSound, 0.8f, 0.8f);
    }
}
