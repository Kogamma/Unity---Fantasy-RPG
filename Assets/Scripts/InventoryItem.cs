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

    // If this item is equippable or not
    public bool equippable = false;

    // Which slot this item will be equipped to
    /*
    0 = Head Slot
    1 = Chest Slot
    2 = Leg Slot
    3 = Ring Slot
    4 = Weapon Slot
    5 = Amulet Slot
    */
    public int equipSlot = -1;

    // The image that will represent the item in menu's and such
    public Sprite itemImage;

    // How much the item is worth in a shop
    public int value = 0;

    // How many there are of this item currently
    public int amountOfItem = 0;

    // Info about this item that will show in a info box
    public string infoText = "";

    public string methodName = "";

    // Constructor
    public InventoryItem(string name, bool stack, bool equip, int eqSlot, Sprite img, int val, string info, string method)
    {
        itemName = name;

        stackable = stack;

        equippable = equip;

        equipSlot = eqSlot;

        itemImage = img;

        value = val;

        amountOfItem = 0;

        infoText = info;

        methodName = method;
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

    // This method checks what method the item will call so it will do an effect
    public List<string> UseItem()
    {
        // Textpages will return to the caller so they can print a message to the textbox
        List<string> textPages = new List<string>();

        // Checks which method that should be called
        if (methodName == "HealingPotion")
            textPages = HealingPotion();
        else if (methodName == "ManaPotion")
            textPages = ManaPotion();
        else if (methodName == "Antidote")
            textPages = Antidote();

        // Returns the text to the call
        return textPages;
    }

    public List<string> HealingPotion()
    {
        List<string> textPages = new List<string>();

        if (PlayerSingleton.instance.playerHealth >= PlayerSingleton.instance.playerMaxHealth)
        {
            textPages.Add("NotHealth");
        }
        else
        {
            // Heals the player for 5 HP
            PlayerSingleton.instance.playerHealth += 5;

            PlayerSingleton.instance.playerHealth = Mathf.Clamp(PlayerSingleton.instance.playerHealth, 0, PlayerSingleton.instance.playerMaxHealth);
            
            textPages.Add("You were healed! You got 5 HP back!");
        }

        return textPages;
    }

    public List<string> ManaPotion()
    {
        List<string> textPages = new List<string>();

        if (PlayerSingleton.instance.playerMana >= PlayerSingleton.instance.playerMaxMana)
        {
            textPages.Add("NotMana");
        }
        else
        {
            // Gives the player 5 mana back
            PlayerSingleton.instance.playerMana += 5;

            PlayerSingleton.instance.playerMana = Mathf.Clamp(PlayerSingleton.instance.playerMana, 0, 10);
            
            textPages.Add("You're mana was restored! You got 5 mana back!");
        }

        return textPages;
    }

    public List<string> Antidote()
    {
        List<string> textPages = new List<string>();

        textPages.Add("Antidote");

        if (!PlayerSingleton.instance.poisoned)
            textPages.Insert(0, "NotAntidote");
        else
            textPages.Add("You used the antidote and your poison effect was cured!");

        return textPages;
    }
}
