using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour 
{
    public GameObject main;
    public GameObject option;
    public GameObject resolution;
    public GameObject audio;
    public GameObject howToPlay;

    public void NewPlayBtn(string newPlayLevel)
    {
        PlayerSingleton.instance.playerInventory.Add("HealingPotion");
        PlayerSingleton.instance.inventoryAmounts.Add(5);

        PlayerSingleton.instance.playerInventory.Add("ManaPotion");
        PlayerSingleton.instance.inventoryAmounts.Add(5);

        PlayerSingleton.instance.playerInventory.Add("Antidote");
        PlayerSingleton.instance.inventoryAmounts.Add(5);

        PlayerSingleton.instance.Save();
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
        resolution.SetActive(false);
        option.SetActive(false);
        main.SetActive(true);
    }
    public void ReturnToOption()
    {
        resolution.SetActive(false);
        howToPlay.SetActive(false);
        audio.SetActive(false);
        option.SetActive(true);
    }
    public void Resolution()
    {
        option.SetActive(false);
        resolution.SetActive(true);
    }
    public void Audio()
    {
        option.SetActive(false);
        audio.SetActive(true);
    }
    public void HowToPlay()
    {
        option.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit(); //  Quits the game
    }

}