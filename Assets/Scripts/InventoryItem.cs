using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    // The name of the item
    public string itemName = "default";

    // The type of item if it is a 'consumable' or 'equippable' item
    public string itemType = "none";

    // How much the item is worth in a shop
    public int value = 0;

    // Constructor
    public InventoryItem(string name, string type, int val)
    {
        itemName = name;

        type = itemType;

        value = val;
    }
}
