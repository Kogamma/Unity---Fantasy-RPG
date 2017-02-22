using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLibrary : MonoBehaviour
{
    InventoryHandler invHandler; 

    public LibraryItem[] items;

    void Start()
    {
        invHandler = GetComponent<InventoryHandler>();

        for (int i = 0; i < items.Length; i++)
        {
            InventoryItem item = new InventoryItem(
                items[i].itemName,
                items[i].stackable,
                items[i].itemImage,
                items[i].value);

            invHandler.AddItem(item);
            
        }
    }
}

[System.Serializable]
public class LibraryItem
{
    // The name of the item
    public string itemName = "default";

    // If the item is stackable, as in a consumable such as a potion or other heal items
    public bool stackable = true;

    // The image that will represent the item in menu's and such
    public Sprite itemImage;

    // How much the item is worth in a shop
    public int value = 0;
}
