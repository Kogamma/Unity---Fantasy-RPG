using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagment : MonoBehaviour
{
    [SerializeField] GameObject mainGroup;
    [SerializeField] GameObject attackGroup;
    [SerializeField] GameObject itemsGroup;

    public AttackButton[] attackButtons;

    [SerializeField] Color redColor;
    [SerializeField] Color blueColor;

    [SerializeField] AudioClip clickSound;
    private AudioSource source;

    void Start ()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        for(int i = 0; i < attackButtons.Length; i++)
        {
            attackButtons[i].button.transform.GetChild(1).GetComponent<Text>().text ="(" + attackButtons[i].manaCost + " MP)";
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

    public void AttackSelect()
    {
        attackGroup.SetActive(true);
        mainGroup.SetActive(false);
        source.PlayOneShot(clickSound, 1f);
    }


    public void ItemsSelect()
    {
        itemsGroup.SetActive(true);
        mainGroup.SetActive(false);
        source.PlayOneShot(clickSound, 1f);
    }


    public void MainSelect()
    {
        mainGroup.SetActive(true);
        itemsGroup.SetActive(false);
        attackGroup.SetActive(false);
        source.PlayOneShot(clickSound, 1f);
    }
}
[System.Serializable]
public class AttackButton
{
    public Button button;
    public int manaCost;
} 