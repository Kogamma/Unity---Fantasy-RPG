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

        //Check if the obects name contains Chest
        if (name.Contains("Chest"))
        {
            //Playing an animation to open the chest
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Open");
            //Playing a sfx when the chest opens
            audioSource.PlayOneShot(openChestSFX);
        }
        
        text.Add("You got ");

        for (int i = 0; i < itemsToLoot.Count; i++)
        {
            //Check if the player have enough space in the inventory to loot the item 
            if (inventHanlder.AddItem(itemsToLoot[i]))
            {
                //Placing number "i" from itemsToLoot in lootedItems
                lootedItems.Add(itemsToLoot[i]);
                //Reomve i from the list itemsToLoot
                itemsToLoot.RemoveAt(i);
                i--;
            }

        }


        for (int i = 0; i < lootedItems.Count; i++)
        {
            if(itemName != lootedItems[i])
            {
                //ItemName will be equal to "i" in lootedItems
                itemName = lootedItems[i]; 
                //Add the number of how many items that have "itemName" in lootedItems and add itemName to text[0]
                text[0] += "[" + lootedItems.FindAll(x => x == itemName).Count + "] " + itemName + " ";
            }
        }

        //Check if the player didnt loot any item
        if (currentItems == itemsToLoot.Count)
            text[0] = "";

        //Check if the object still have items to loot
        if (itemsToLoot.Count != 0)
        {
            text[0] += (" Your inventory is full, this items will be left behind ");
            itemName = "";

            for (int i = 0; i < itemsToLoot.Count; i++)
            {
                if (itemName != itemsToLoot[i])
                {
                    itemName = itemsToLoot[i];
                    //Print out how many items that are left in the chest.
                    text[0] += "[" + itemsToLoot.FindAll(x => x == itemName).Count + "] " + itemName + " ";
                }
            }

            textBox.PrintMessage(text.ToArray(), "Chest", gameObject, "ReversAnim");
        }

        //Doing this when all the items are picked up
        else
        {
            //Setting the obects tag to Uninteractable
            gameObject.tag = "Uninteractable";
            textBox.PrintMessage(text.ToArray(), "Chest", gameObject, "InActivateTreasure");
        }

        

        
    }

    //Playing the close animation if their still are items in the chest 
    void ReversAnim()
    {
        gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Close");
    }
    //Setting the treasure inactive in the chest 
    void InActivateTreasure()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

}
