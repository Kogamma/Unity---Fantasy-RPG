using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    // The name of the item
    public string itemName = "default";

    // If the item is stackable, as in a consumable such as a potion or other heal items
    public bool stackable = false;

    // The image that will represent the item in menu's and such
    public Sprite itemImage;

    // How much the item is worth in a shop
    public int value = 0;

    // How many there are of this item currently
    public int amountOfItem = 0;

    // Constructor
    public InventoryItem(string name, bool stack, Sprite img, int val)
    {
        itemName = name;

        stackable = stack;

        itemImage = img;

        value = val;

        amountOfItem = 0;
    }

    // Adds another item to the count of items
    public void IncrementItem()
    {
        // Only the consumable items are stackable so only they can get more than 1
        if (stackable)
            amountOfItem++;
        else
        {
            amountOfItem = 1;
        }
    }
}
