using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveGame : MonoBehaviour
{
    public GameObject choiceButtons;
    public OverworldManager ovManager;

    void OnInteract()
    {
        choiceButtons.SetActive(true);
        PlayerSingleton.instance.gameCanRun = false;
        PlayerSingleton.instance.canMove = false;
        EventSystem.current.SetSelectedGameObject(choiceButtons.transform.GetChild(0).transform.GetChild(1).gameObject);
        ovManager.interactedSaveStation = gameObject;
    }
}
