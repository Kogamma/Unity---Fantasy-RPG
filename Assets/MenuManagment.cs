using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagment : MonoBehaviour
{
    [SerializeField] GameObject mainGroup;
    [SerializeField] GameObject attackGroup;
    [SerializeField] GameObject itemsGroup;
    [SerializeField] AudioClip clickSound;
    private AudioSource source;


    void Start ()
    {
        source = GetComponent<AudioSource>();
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
