using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start ()
    {
        // Sets the player screen as default when we first start the game
        OpenPlayerScreen();

        // Adds the OpenInventory method to the inventoryButton
        inventoryScreenButton.onClick.AddListener(OpenInventory);

        // Adds the PlayerScreen method to the PlayerScreenButton
        playerScreenButton.onClick.AddListener(OpenPlayerScreen);
	}
	
	void Update ()
    {
		
	}

    // Opens the inventory menu and closes the others that are open
    void OpenInventory()
    {
        // Deactivates the inventory button
        inventoryScreenButton.interactable = false;

        // Opens the inventory
        inventoryScreen.SetActive(true);


        // Deactivates all the other screens
        playerScreen.SetActive(false);

        // Activates all the other screens' buttons
        playerScreenButton.interactable = true;
    }

    // Opens the playerScreen menu and closes the others that are open
    void OpenPlayerScreen()
    {
        // Deactivates the playerScreen button
        playerScreenButton.interactable = false;

        // Opens the player screen
        playerScreen.SetActive(true);


        // Deactivates all the other screens
        inventoryScreen.SetActive(false);

        // Activates all the other screens' buttons
        inventoryScreenButton.interactable = true;
    }

    // This closes the whole menu 
    void CloseMenu()
    {
        this.gameObject.SetActive(false);

        normalCanvas.SetActive(false);
    }
}
