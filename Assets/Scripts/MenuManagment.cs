using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManagment : MonoBehaviour
{
    [SerializeField] GameObject mainGroup;
    [SerializeField] GameObject attackGroup;
    [SerializeField] GameObject itemsGroup;
    [SerializeField] GameObject uiBox;
    [SerializeField] GameObject deathGroup;
    [SerializeField] GameObject descriptionBox;

    public GameObject mainButton;
    public GameObject crossButton;
    public GameObject returnButton;
    public GameObject restartFromSaveButton;

    public AttackButton[] attackButtons;

    [SerializeField] Color redColor;
    [SerializeField] Color blueColor;

    void Start()
    {
        Button[] buttons = new Button[attackButtons.Length];

        /*for (int i = 0; i < attackButtons.Length; i++)
        {
            buttons[i] = attackButtons[i].button;

            EventTrigger trigger = buttons[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { DisplayDescription((BaseEventData)eventData, i); });

            trigger.triggers.Add(entry);
        }

        // Add listeners for the OnSelect event so they call OnQuestButtonSelect
        for (int i = 0; i < questButtons.Length; i++)
        {
            
        }*/
    }

    void Update()
    {
        for(int i = 0; i < attackButtons.Length; i++)
        {
            if (attackButtons[i].manaCost != 0)
            {
                attackButtons[i].button.transform.GetChild(1).GetComponent<Text>().text = "(" + attackButtons[i].manaCost + " MP)";
                if (attackButtons[i].manaCost > PlayerSingleton.instance.playerMana)
                {
                    attackButtons[i].button.transform.GetChild(1).GetComponent<Text>().color = redColor;
                    attackButtons[i].button.interactable = false;
                }
                else if (attackButtons[i].manaCost <= PlayerSingleton.instance.playerMana)
                {
                    attackButtons[i].button.transform.GetChild(1).GetComponent<Text>().color = blueColor;
                    attackButtons[i].button.interactable = true;
                }
            }
        }
    }

    public void AttackSelect()
    {
        attackGroup.SetActive(true);
        mainGroup.SetActive(false);

        EventSystem.current.SetSelectedGameObject(attackGroup.transform.GetChild(0).gameObject);
    }


    public void ItemsSelect()
    {
        itemsGroup.SetActive(true);
        mainGroup.SetActive(false);

        EventSystem.current.SetSelectedGameObject(crossButton);
    }


    public void MainSelect()
    {
        uiBox.SetActive(true);
        mainGroup.SetActive(true);
        itemsGroup.SetActive(false);
        attackGroup.SetActive(false);

        EventSystem.current.SetSelectedGameObject(mainButton);
    }


    public void ReturnToWorldSelect()
    {
        uiBox.SetActive(true);
        returnButton.SetActive(true);
        attackGroup.SetActive(false);
    }


    public void RestartGameSelect()
    {
        attackGroup.SetActive(false);
        mainGroup.SetActive(false);
        uiBox.SetActive(true);
        deathGroup.SetActive(true);

        EventSystem.current.SetSelectedGameObject(restartFromSaveButton);
    }


    public void DisplayDescription(int buttonIndex)
    {
        descriptionBox.GetComponentInChildren<Text>().text = attackButtons[buttonIndex].description;
        descriptionBox.SetActive(true);
    }


    public void RemoveDescription()
    {
        descriptionBox.SetActive(false);
    }


    public void ExitToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu_Scene");
    }
}


[System.Serializable]
public class AttackButton
{
    public Button button;
    public int manaCost;
    [TextArea]
    public string description;
} 