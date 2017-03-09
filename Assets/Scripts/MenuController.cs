using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    public GameObject pauseMenuCanvas;

    public Button closeMenuCanvasButton;

    private GameObject lastButton;

    void Start()
    {
        closeMenuCanvasButton.onClick.AddListener(ActivateMenu);
    }

	void Update ()
    {
        if (PlayerSingleton.instance.gameCanRun)
        {
            // Brings up the pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseMenu();

            // Brings up the menu for stats, inventory, journal etc.
            if (Input.GetKeyDown(KeyCode.Tab) && !pauseMenuCanvas.activeSelf)
                ActivateMenu();
        }
	}


    public void ActivateMenu()
    {
        // Checks if the menu is not already activated
        if(!menuCanvas.transform.GetChild(0).GetComponent<Canvas>().enabled)
            menuCanvas.GetComponent<MenuScreenManager>().OpenMenu();
        else if (menuCanvas.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled)
            menuCanvas.GetComponent<MenuScreenManager>().CloseMenu();
        // Pauses or unpauses the game
        Pause();
    }

    public void PauseMenu()
    {  
        // Pauses or unpauses the game
        if (!menuCanvas.transform.GetChild(0).GetComponent<Canvas>().enabled)
        {
            Pause();
        }
        else if (menuCanvas.transform.GetChild(0).GetComponent<Canvas>().enabled)
        {
            if(pauseMenuCanvas.activeSelf)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(lastButton);
            }
            else
            {
                lastButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            }
        }

        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);

        if(pauseMenuCanvas.activeSelf)
            pauseMenuCanvas.GetComponent<PauseMenuManager>().Main_SetActive();
    }

    public void Pause()
    {
        // Pauses the game if it's not already paused and vice versa
        PlayerSingleton.instance.canMove = !PlayerSingleton.instance.canMove;
    }
}
