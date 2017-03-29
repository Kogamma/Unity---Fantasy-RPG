using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public GameObject choiceButtons;        // The buttons where the player must choose whether to save or not
    public SaveButtonsChoice saveChoice;    // The SaveButtonsChoice script that has the method that handles the choice of saving or not


    void OnInteract()
    {
        // Activates buttons to be pressed
        choiceButtons.SetActive(true);

        // The game is now paused
        PlayerSingleton.instance.gameCanRun = false;
        PlayerSingleton.instance.canMove = false;

        // Makes sure that the SaveButtonsChoice script knows that this was the save station that was interacted
        saveChoice.interactedStation = transform;

        // Highlights the the "No"-button
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(choiceButtons.transform.GetChild(0).GetChild(0).gameObject);
    }
}
