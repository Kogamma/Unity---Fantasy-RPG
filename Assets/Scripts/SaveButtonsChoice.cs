using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonsChoice : MonoBehaviour
{
    public GameObject choiceButtons;    // The buttons where the player must choose wether to save or not
    public TextBoxHandler textBox;      // The textbox that will print confirmation message;

    [System.NonSerialized]
    public Transform interactedStation; // The save station that was interacted 

    string[] text = new string[1];      // The text to be printed


    public void SaveChoice(bool yes)
    {
        // If the "Yes"-button was pressed
        if (yes)
        {
            // Sets spawnpositions to be saved
            PlayerSingleton.instance.savePosX = interactedStation.transform.GetChild(0).position.x;
            PlayerSingleton.instance.savePosY = interactedStation.transform.GetChild(0).position.y;
            PlayerSingleton.instance.savePosZ = interactedStation.transform.GetChild(0).position.z;

            // Assigns confirmation message to the text
            text[0] = "Your progress was saved!";

            // Saves the players progress
            PlayerSingleton.instance.Save();
        }
        // If "No"-button was pressed
        else
            text[0] = "You didn't save your progress";

        // Deactivates the choice buttons
        choiceButtons.SetActive(false);

        // Deselects all potential buttons
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        // The game is no longer paused
        PlayerSingleton.instance.gameCanRun = true;
        PlayerSingleton.instance.canMove = true;

        // Prints confirmation message
        textBox.PrintMessage(text, null, null, null);
    }
}
