using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagment : MonoBehaviour
{
    [SerializeField] GameObject mainGroup;
    [SerializeField] GameObject attackGroup;
    [SerializeField] GameObject itemsGroup;


    public void AttackSelect()
    {
        attackGroup.SetActive(true);
        mainGroup.SetActive(false);
    }


    public void ItemsSelect()
    {
        itemsGroup.SetActive(true);
        mainGroup.SetActive(false);
    }


    public void MainSelect()
    {
        mainGroup.SetActive(true);
        itemsGroup.SetActive(false);
        attackGroup.SetActive(false);
    }
}
