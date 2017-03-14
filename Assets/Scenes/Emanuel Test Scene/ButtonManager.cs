using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour 
{
    public GameObject main;
    public GameObject option;
    public GameObject resolution;
    public GameObject audio;
    public GameObject howToPlay;
    public GameObject credits;

    private AutoCredits autoCred;

    void Start()
    {
        autoCred = GetComponent<AutoCredits>();
    }


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

        EventSystem.current.SetSelectedGameObject(option.transform.GetChild(option.transform.childCount - 1).gameObject);
    }
    //Return to Main menu
    public void ReturnToMain ()
    {
        resolution.SetActive(false);
        option.SetActive(false);
        credits.SetActive(false);
        main.SetActive(true);

        autoCred.credBool = false;

        EventSystem.current.SetSelectedGameObject(main.transform.GetChild(2).gameObject);
    }
    public void ReturnToOption()
    {
        // Checks what button to highlight based on what menu we we're in
        if(resolution.activeSelf)
            EventSystem.current.SetSelectedGameObject(option.transform.GetChild(0).gameObject);
        else if (audio.activeSelf)
            EventSystem.current.SetSelectedGameObject(option.transform.GetChild(1).gameObject);
        else if (howToPlay.activeSelf)
            EventSystem.current.SetSelectedGameObject(option.transform.GetChild(2).gameObject);
        else if (main.activeSelf)
            EventSystem.current.SetSelectedGameObject(option.transform.GetChild(3).gameObject);


        resolution.SetActive(false);
        howToPlay.SetActive(false);
        audio.SetActive(false);
        option.SetActive(true);
    }
    public void Resolution()
    {
        option.SetActive(false);
        resolution.SetActive(true);

        EventSystem.current.SetSelectedGameObject(resolution.transform.GetChild(resolution.transform.childCount - 1).gameObject);
    }
    public void Audio()
    {
        option.SetActive(false);
        audio.SetActive(true);

        EventSystem.current.SetSelectedGameObject(audio.transform.GetChild(audio.transform.childCount - 1).gameObject);
    }
    public void HowToPlay()
    {
        option.SetActive(false);
        howToPlay.SetActive(true);

        EventSystem.current.SetSelectedGameObject(howToPlay.transform.GetChild(howToPlay.transform.childCount - 1).gameObject);
    }
    public void Credits()
    {
        main.SetActive(false);
        credits.SetActive(true);
        autoCred.credBool = true;
    }

    public void ExitBtn()
    {
        Application.Quit(); //  Quits the game
    }

    public void ExitToMenuBtn()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }

}