using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas menuCanvas;

    public GameObject pauseMenuCanvas;

    public Button closeMenuCanvasButton;

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
            if (Input.GetKeyDown(KeyCode.Tab))
                ActivateMenu();
        }
	}


    public void ActivateMenu()
    {
        menuCanvas.enabled = !menuCanvas.enabled;

        // Pauses or unpauses the game
        Pause();
    }

    void PauseMenu()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);

        // Pauses or unpauses the game
        Pause();
    }

    public void Pause()
    {
        // Pauses the game if it's not already paused and vice versa
        PlayerSingleton.instance.canMove = !PlayerSingleton.instance.canMove;
    }
}
