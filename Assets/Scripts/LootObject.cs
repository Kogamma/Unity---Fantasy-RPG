using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{

    public InventoryHandler inventHanlder;

    public TextBoxHandler textBox;

    public AudioClip openChestSFX;

    public AudioSource audioSource;

    private bool opened;

    public enum Item { Healing_Potion, Mana_Potion, Antidote };

    public Item[] items;

    private string[] itemReferences;

    private List<string> itemsToLoot = new List<string>();

    void Start()
    {
        itemReferences = new string[3];
        itemReferences[0] = "HealingPotion";
        itemReferences[1] = "ManaPotion";
        itemReferences[2] = "Antidote";
        //itemReference[2] = "BasicSword";
        //itemReference[2] = "IronHelmet";
        
        audioSource = GetComponent<AudioSource>();

        if (!opened && itemsToLoot.Count <= 0)
        {
            for (int i = 0; i < items.Length; i++)
            {
                itemsToLoot.Add(itemReferences[(int)items[i]]);
            }
        }
    }

    public void OnInteract()
    {
        List<string> text = new List<string>();
        int hpPotions = 0;
        int manaPotions = 0;
        int antidote = 0;

        opened = true;

        int currentItems = itemsToLoot.Count;

        if (name.Contains("Chest"))
        {
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Open");
            audioSource.PlayOneShot(openChestSFX);
        }

        text.Add("You got ");
        for (int i = 0; i < itemsToLoot.Count; i++)
        {
            if (inventHanlder.AddItem(itemsToLoot[i]))
            {
                if (itemsToLoot[i] == "HealingPotion")
                    hpPotions++;

                else if (itemsToLoot[i] == "ManaPotion")
                    manaPotions++;

                itemsToLoot.RemoveAt(i);
                i--;
            }
        }

        Debug.Log(itemsToLoot.Count);

        Debug.Log(currentItems);
        if (hpPotions > 0)
            text[0] += "[" + hpPotions + "] Potion of healing ";
        if (manaPotions > 0)
            text[0] += "[" + manaPotions + "] Mana Potion ";

        if (currentItems == itemsToLoot.Count)
            text[0] = "";

        if (itemsToLoot.Count != 0)
        {
            hpPotions = 0;
            manaPotions = 0;
            text[0] += (" Your inventory is full, this items will be left behind");
            for (int i = 0; i < itemsToLoot.Count; i++)
            {
                if (itemsToLoot[i] == "HealingPotion")
                    hpPotions++;
                else if (itemsToLoot[i] == "ManaPotion")
                    manaPotions++;
            }

            if (hpPotions > 0)
                text[0] += "[" + hpPotions + "] Potion of healing ";
            if (manaPotions > 0)
                text[0] += "[" + manaPotions + "] Mana Potion ";
            textBox.StartMessage(text.ToArray(), "Chest", gameObject, "ReversAnim");
        }

        else
        {
            gameObject.tag = "Uninteractable";
            textBox.StartMessage(text.ToArray(), "Chest", gameObject, "InActivateTreasure");
        }

        

        
    }

    void ReversAnim()
    {
        gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Close");
    }

    void InActivateTreasure()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

}
