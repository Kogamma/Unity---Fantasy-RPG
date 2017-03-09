using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject mainGroup;
    public GameObject audioGroup;
    public GameObject howToPlayGroup;
    public GameObject resolutionGroup;
    public GameObject optionsGroup;
    public GameObject exitToDesktopWarningGroup;
    public GameObject exitToMenuWarningGroup;


    public void Main_SetActive ()
    {
        // Checks if we were in some other pause menu screen before this to know what button to highlight
        if(optionsGroup.activeSelf)
            EventSystem.current.SetSelectedGameObject(mainGroup.transform.GetChild(2).gameObject);
        else 
            EventSystem.current.SetSelectedGameObject(mainGroup.transform.GetChild(0).gameObject);

        optionsGroup.SetActive(false);
        audioGroup.SetActive(false);
        resolutionGroup.SetActive(false);
        howToPlayGroup.SetActive(false);
        exitToDesktopWarningGroup.SetActive(false);
        exitToMenuWarningGroup.SetActive(false);
        mainGroup.SetActive(true);


    }

    public void DesktopWarning_SetActive()
    {
        mainGroup.SetActive(false);
        exitToDesktopWarningGroup.SetActive(true);

        EventSystem.current.SetSelectedGameObject(exitToDesktopWarningGroup.transform.GetChild(1).gameObject);
    }

    public void MenuWarning_SetActive()
    {
        mainGroup.SetActive(false);
        exitToMenuWarningGroup.SetActive(true);

        EventSystem.current.SetSelectedGameObject(exitToMenuWarningGroup.transform.GetChild(1).gameObject);
    }

    public void Options_SetActive()
    {
        // Checks what menu screen we we're in last to highlight the right button
        if(resolutionGroup.activeSelf)
            EventSystem.current.SetSelectedGameObject(optionsGroup.transform.GetChild(0).gameObject);
        else if (audioGroup.activeSelf)
            EventSystem.current.SetSelectedGameObject(optionsGroup.transform.GetChild(1).gameObject);
        else if (howToPlayGroup.activeSelf)
            EventSystem.current.SetSelectedGameObject(optionsGroup.transform.GetChild(2).gameObject);
        else if (mainGroup.activeSelf)
            EventSystem.current.SetSelectedGameObject(optionsGroup.transform.GetChild(3).gameObject);

        mainGroup.SetActive(false);
        audioGroup.SetActive(false);
        howToPlayGroup.SetActive(false);
        resolutionGroup.SetActive(false);
        optionsGroup.SetActive(true);
    }

    public void Audio_SetActive()
    {
        optionsGroup.SetActive(false);
        audioGroup.SetActive(true);

        EventSystem.current.SetSelectedGameObject(audioGroup.transform.GetChild(0).gameObject);
    }

    public void HowToPlay_SetActive()
    {
        optionsGroup.SetActive(false);
        howToPlayGroup.SetActive(true);

        EventSystem.current.SetSelectedGameObject(howToPlayGroup.transform.GetChild(0).gameObject);
    }

    public void Resolutions_SetActive()
    {
        optionsGroup.SetActive(false);
        resolutionGroup.SetActive(true);

        EventSystem.current.SetSelectedGameObject(resolutionGroup.transform.GetChild(resolutionGroup.transform.childCount - 1).gameObject);
    }

    public void LoadSave()
    {
        PlayerSingleton.instance.Load();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu_Scene");
    }
}
