using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    // The name of the item
    public string itemName = "default";

    // The type of item if it is a 'consumable' or 'equippable' item
    public string itemType = "none";

    // The image that will represent the item in menu's and such
    public Sprite itemImage;

    // How much the item is worth in a shop
    public int value = 0;

    // How many there are of this item currently
    public int amountOfItem = 0;

    // Constructor
    public InventoryItem(string name, string type, Sprite img, int val)
    {
        itemName = name;

        type = itemType;

        itemImage = img;

        value = val;

        amountOfItem = 1;
    }

    // Adds another item to the count of items
    public void AddItem()
    {
        // Only the consumable items are stackable so only they can get more than 1
        if(itemType == "consumable")
            amountOfItem++;
        else if(itemType == "equippable")
        {
            Debug.Log("Can't add another equippable of the same type in the same slot!");
            amountOfItem = 1;
        }
    }
}
