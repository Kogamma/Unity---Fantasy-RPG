using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    public GameObject pauseMenuCanvas;

    public Button closeMenuCanvasButton;

    void Start()
    {
        closeMenuCanvasButton.onClick.AddListener(ActivateMenu);
    }

	void Update ()
    {
        // Brings up the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu();

        // Brings up the menu for stats, inventory, journal etc.
        if (Input.GetKeyDown(KeyCode.Tab))
            ActivateMenu();
	}


    void ActivateMenu()
    {
        menuCanvas.SetActive(menuCanvas.activeSelf ? false : true);

        // Pauses or unpauses the game
        Pause();
    }

    void PauseMenu()
    {
        pauseMenuCanvas.SetActive(pauseMenuCanvas.activeSelf ? false : true);

        // Pauses or unpauses the game
        Pause();
    }

    public void Pause()
    {
        // Pauses the game if it's not already paused and vice versa
        PlayerSingleton.instance.canMove = PlayerSingleton.instance.canMove ? false : true;

        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else if (Time.timeScale == 0)
            Time.timeScale = 1;
    }
}
