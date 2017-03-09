using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPair
{
    public LootObject.Item item;
    public int count;
}

public class LootObject : MonoBehaviour
{

    public InventoryHandler inventHanlder;

    public TextBoxHandler textBox;

    public AudioClip openChestSFX;

    public AudioSource audioSource;

    private bool opened;

    public enum Item { Healing_Potion, Mana_Potion, Antidote };

    public ItemPair[] realItems;

    //public Item[] items;

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
            for (int i = 0; i < realItems.Length; i++)
            {
                for (int j = 0; j < realItems[i].count; j++)
                    itemsToLoot.Add(itemReferences[(int)realItems[i].item]);
            }
        }
    }

    public void OnInteract()
    {
        List<string> text = new List<string>();

        opened = true;

        List<string> lootedItems = new List<string>();

        string itemName = "";

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
                lootedItems.Add(itemsToLoot[i]);
                itemsToLoot.RemoveAt(i);
                i--;
            }

        }


        for (int i = 0; i < lootedItems.Count; i++)
        {
            if(itemName != lootedItems[i])
            {
                itemName = lootedItems[i]; 
                text[0] += "[" + lootedItems.FindAll(x => x == itemName).Count + "] " + itemName + " ";
            }
        }


        if (currentItems == itemsToLoot.Count)
            text[0] = "";

        if (itemsToLoot.Count != 0)
        {
            text[0] += (" Your inventory is full, this items will be left behind ");
            itemName = "";

            for (int i = 0; i < itemsToLoot.Count; i++)
            {
                if (itemName != itemsToLoot[i])
                {
                    itemName = itemsToLoot[i];
                    text[0] += "[" + itemsToLoot.FindAll(x => x == itemName).Count + "] " + itemName + " ";
                }
            }

            textBox.PrintMessage(text.ToArray(), "Chest", gameObject, "ReversAnim");
        }

        else
        {
            gameObject.tag = "Uninteractable";
            textBox.PrintMessage(text.ToArray(), "Chest", gameObject, "InActivateTreasure");
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
