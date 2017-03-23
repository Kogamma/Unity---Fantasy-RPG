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

    public OverworldManager ovManager;

    public TextBoxHandler textBox;

    public AudioClip openChestSFX;

    private bool opened;

    public enum Item
    {
        Healing_Potion,
        Mana_Potion,
        Antidote,
        Confusion_Heal,
        Clairvoyance_Potion,
        Golden_Hit_Potion,
        Ointment
    };

    public ItemPair[] realItems;

    //public Item[] items;

    private string[] itemReferences;

    private List<string> itemsToLoot = new List<string>();

    void Start()
    {
        itemReferences = new string[7];
        itemReferences[0] = "HealingPotion";
        itemReferences[1] = "ManaPotion";
        itemReferences[2] = "Antidote";
        itemReferences[3] = "ConfusionHeal";
        itemReferences[4] = "ClairvoyancePotion";
        itemReferences[5] = "GoldenHitPotion";
        itemReferences[6] = "Ointment";
        //itemReference[2] = "BasicSword";
        //itemReference[2] = "IronHelmet";
        
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
            AudioHelper.PlaySound(openChestSFX);
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

        string orgItemName = itemName;

        for (int i = 0; i < lootedItems.Count; i++)
        {           
            if (orgItemName != lootedItems[i])
            {
                itemName = lootedItems[i];
                orgItemName = itemName;

                //ItemName will be equal to "i" in lootedItems
                itemName = ItemLibrary.GetReferenceItem(orgItemName).codeName;

                //Add the number of how many items that have "itemName" in lootedItems and add itemName to text[0]
                text[0] += "[" + lootedItems.FindAll(x => x == orgItemName).Count + "] " + itemName + " ";

                /*if (i < lootedItems.Count)
                {
                    if (orgItemName != lootedItems[i + 1])
                    {
                        text[0] += ", ";
                    }
                }*/            
            }
        }

        //Check if the player didnt loot any item
        if (currentItems == itemsToLoot.Count)
            text[0] = "";

        //Check if the object still have items to loot
        if (itemsToLoot.Count != 0)
        {
            text[0] += (" Your inventory is full, these items will be thrown away: ");
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
        }

        //Setting the obects tag to Uninteractable
        gameObject.tag = "Uninteractable";
        textBox.PrintMessage(text.ToArray(), "Chest", gameObject, "InActivateTreasure");

        if (PlayerSingleton.instance.currentScene == "Forest_Scene_1")
            PlayerSingleton.instance.chestOpen_lightForest[ovManager.chests.IndexOf(gameObject.GetComponent<LootObject>())] = true;
        else if (PlayerSingleton.instance.currentScene == "dark_forest_1")
            PlayerSingleton.instance.chestOpen_darkForest[ovManager.chests.IndexOf(gameObject.GetComponent<LootObject>())] = true;
    }


    //Setting the treasure inactive in the chest 
    public void InActivateTreasure()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OpenAnim()
    {
        //Playing an animation to open the chest
        gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Open");
    }
}
