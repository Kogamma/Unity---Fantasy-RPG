using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSetup : MonoBehaviour
{
    Button[] allButtonsInScene;

    public AudioClip clickSound;

    float clickVolume = 0.8f;

    GameObject lastSelectedGameObject;

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

        // Sets our last selected GameObject
        lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
	}
	
    void Update()
    {
        // Checks all three different mouse buttons to see if we click 
        // because then we will lose focus of the selected UI element
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetMouseButtonDown(i))
            {
                // Sets the clickVolume to 0 temporarily since we don't want to hear 
                // the click sound when we reselect the object after losing focus of it
                clickVolume = 0;

                // Sets the focus to the gameObject we recently had selected when we click the mouse
                EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
            }
        }
    }

	void OnSelect(BaseEventData eventData)
    {
        AudioHelper.PlayPitched(clickSound, clickVolume, clickVolume);

        lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;

        clickVolume = 0.8f;
    }
}
