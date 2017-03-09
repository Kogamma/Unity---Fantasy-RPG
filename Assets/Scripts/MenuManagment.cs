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
    public GameObject returnButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject descriptionBox;

    public AttackButton[] attackButtons;

    [SerializeField] Color redColor;
    [SerializeField] Color blueColor;

    [SerializeField] AudioClip clickSound;

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
        AudioHelper.PlaySound(clickSound);
    }


    public void ItemsSelect()
    {
        itemsGroup.SetActive(true);
        mainGroup.SetActive(false);
        AudioHelper.PlaySound(clickSound);
    }


    public void MainSelect()
    {
        uiBox.SetActive(true);
        mainGroup.SetActive(true);
        itemsGroup.SetActive(false);
        attackGroup.SetActive(false);
        AudioHelper.PlaySound(clickSound);

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
        uiBox.SetActive(true);
        restartButton.SetActive(true);
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
}


[System.Serializable]
public class AttackButton
{
    public Button button;
    public int manaCost;
    [TextArea]
    public string description;
} 