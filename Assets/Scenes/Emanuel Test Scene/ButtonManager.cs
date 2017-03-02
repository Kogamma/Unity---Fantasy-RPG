using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour 
{
    public GameObject main;
    public GameObject option;

    public void NewPlayBtn(string newPlayLevel)
    {
        SceneManager.LoadScene(newPlayLevel);   // starts the game
    }
    //Go to Options menu
    public void OptionBtn()
    {
        main.SetActive(false);
        option.SetActive(true);
    }
    //Return to Main menu
    public void ReturnToMain ()
    {
        option.SetActive(false);
        main.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit(); //  Quits the game
    }

}