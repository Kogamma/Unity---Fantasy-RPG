using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuScreenManager : MonoBehaviour
{

    public GameObject normalCanvas;

    // The screen where the inventory will be visible
    public GameObject inventoryScreen;
    // The button that will bring up the inventoryScreen
    public Button inventoryScreenButton;

    // Where you will see the player's stats and be able to use level up points
    public GameObject playerScreen;
    // The button that will bring up the playerScreen
    public Button playerScreenButton;

    private GameObject lastOpen;

    void Start()
    {
        lastOpen = playerScreen;

        // Adds the OpenInventory method to the inventoryButton
        inventoryScreenButton.onClick.AddListener(OpenInventory);

        // Adds the PlayerScreen method to the PlayerScreenButton
        playerScreenButton.onClick.AddListener(OpenPlayerScreen);
	}

    public void OpenMenu()
    {
        this.transform.GetChild(0).GetComponent<Canvas>().enabled = true;

        inventoryScreen.SetActive(false);
        playerScreen.SetActive(false);

        lastOpen.SetActive(true);

        normalCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(playerScreenButton.gameObject);
    }

    // Opens the inventory menu and closes the others that are open
    void OpenInventory()
    {
        // Deactivates the inventory button
        //inventoryScreenButton.interactable = false;

        // Opens the inventory
        inventoryScreen.SetActive(true);
        
        lastOpen = inventoryScreen;

        // Deactivates all the other screens
        playerScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(inventoryScreenButton.gameObject);

        // Activates all the other screens' buttons
        //playerScreenButton.interactable = true;
    }


    // Opens the playerScreen menu and closes the others that are open
    void OpenPlayerScreen()
    {
        // Deactivates the playerScreen button
        //playerScreenButton.interactable = false;

        // Opens the player screen
        playerScreen.SetActive(true);

        lastOpen = playerScreen;
        
        // Deactivates all the other screens
        inventoryScreen.SetActive(false);
        inventoryScreen.transform.parent.GetComponent<InventoryMenu>().CancelButton();

        EventSystem.current.SetSelectedGameObject(playerScreenButton.gameObject);

        // Activates all the other screens' buttons
        //inventoryScreenButton.interactable = true;
    }


    // This closes the whole menu 
    public void CloseMenu()
    {
        // Hides the menu canvas
        this.transform.GetChild(0).GetComponent<Canvas>().enabled = false;

        // Deactivates the inventory screen
        inventoryScreen.SetActive(false);

        // Deactivates the leveling/player menu
        playerScreen.SetActive(false);

        // Hides the info window in the player menu if it's up when we close the menu
        playerScreen.GetComponent<Level_Stats_Script>().HideInfo();

        // Activates the normal canvas again
        normalCanvas.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }
}
