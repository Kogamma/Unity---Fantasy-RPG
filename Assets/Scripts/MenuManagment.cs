using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

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

    Button[] buttons;

    [SerializeField] Color redColor;
    [SerializeField] Color blueColor;

    void Start()
    {
        buttons = new Button[attackButtons.Length];

        for (int i = 0; i < attackButtons.Length; i++)
        {
            buttons[i] = attackButtons[i].button;

            EventTrigger trigger = buttons[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { DisplayDescription((BaseEventData)eventData); });

            trigger.triggers.Add(entry);
        }

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


    public void DisplayDescription(BaseEventData eventData)
    {
        int buttonIndex = buttons.ToList().IndexOf(eventData.selectedObject.GetComponent<Button>());

        descriptionBox.GetComponentInChildren<Text>().text = attackButtons[buttonIndex].description;
        descriptionBox.SetActive(true);
    }


    public void RemoveDescription()
    {
        descriptionBox.SetActive(false);
    }


    public void ExitToMainMenu()
    {
        PlayerSingleton.instance.poisoned = false;
        PlayerSingleton.instance.confused = false;
        PlayerSingleton.instance.onFire = false;
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