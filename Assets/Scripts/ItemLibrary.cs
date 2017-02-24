using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLibrary : MonoBehaviour
{
    InventoryHandler invHandler;

    public LibraryItem[] items;

    public ItemSprites sprites;

    void Start()
    {
        invHandler = GetComponent<InventoryHandler>();
    }

    public void AddItemWithButton(string item)
    {
        InventoryItem newItem = null;

        if (item == "Healing")
            newItem = NewHealingPotion();
        else if (item == "Mana")
            newItem = NewManaPotion();
        else if (item == "Ointment")
            newItem = NewOintment();

        invHandler.AddItem(newItem);
    }

    /*
    Down below are methods that hold all the different inventory
    items in the game, the first item will have comments on what 
    each variable does. When you want to add an item to your inventory
    you will simply call this function to get a new InventoryItem
    returned to where you called the function
    */
    #region Inventory Items
    // Healing Potion
    public InventoryItem NewHealingPotion()
    {
        // Name of the item we're creating
        string itemName = "Potion of <color=red>Healing</color>";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = true;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.healingPotionSprite;

        // How much the item is worth in a shop
        int value = 10;

        // Creates a new inventoryitem
        
        InventoryItem item = new InventoryItem(itemName, stackable, itemImage, value);
        // Returns the item we just created
        return item;
    }

    // Mana Potion
    public InventoryItem NewManaPotion()
    {
        // Name of the item we're creating
        string itemName = "<color=blue>Mana</color> Potion";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = true;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.manaPotionSprite;

        // How much the item is worth in a shop
        int value = 10;

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, itemImage, value);

        // Returns the item we just created
        return item;
    }

    // Ointment/Burn Heal
    public InventoryItem NewOintment()
    {
        // Name of the item we're creating
        string itemName = "Ointment";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = false;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.antidoteSprite;

        // How much the item is worth in a shop
        int value = 15;

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, itemImage, value);

        // Returns the item we just created
        return item;
    }
    #endregion
}

// This is so we can create item's from the editor if we want
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

    /*public LibraryItem(string name, bool stack, Sprite img, int val)
    {
        itemName = name;

        stackable = stack;

        itemImage = img;

        value = val;
    }*/
}

[System.Serializable]
public class ItemSprites
{
    public Sprite healingPotionSprite;

    public Sprite manaPotionSprite;

    public Sprite antidoteSprite;
}
