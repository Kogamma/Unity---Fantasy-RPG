using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void MenuWarning_SetActive()
    {
        mainGroup.SetActive(false);
        exitToMenuWarningGroup.SetActive(true);
    }

    public void Options_SetActive()
    {
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
    }

    public void HowToPlay_SetActive()
    {
        optionsGroup.SetActive(false);
        howToPlayGroup.SetActive(true);
    }

    public void Resolutions_SetActive()
    {
        optionsGroup.SetActive(false);
        resolutionGroup.SetActive(true);
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
