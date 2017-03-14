using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public GameObject choiceButtons;
    public OverworldManager ovManager;

    void OnInteract()
    {
        choiceButtons.SetActive(true);
        PlayerSingleton.instance.gameCanRun = false;
        PlayerSingleton.instance.canMove = false;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(choiceButtons.transform.GetChild(0).GetChild(0).gameObject);
        ovManager.interactedSaveStation = gameObject;
    }
}
