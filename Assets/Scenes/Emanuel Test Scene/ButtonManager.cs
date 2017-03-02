using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour 
{
    public void NewPlayBtn(string newPlayLevel)
    {
        SceneManager.LoadScene(newPlayLevel);   // starts the game
    }

    public void OptionBtn(string OptionLevel)
    {
        SceneManager.LoadScene(OptionLevel);    // Loads the Option Scene
    }

    public void ExitBtn()
    {
        Application.Quit(); //  Quits the game
    }

}