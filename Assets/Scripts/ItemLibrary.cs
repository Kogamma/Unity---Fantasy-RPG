using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLibrary : MonoBehaviour
{
    InventoryHandler invHandler;

    //public LibraryItem[] items;

    public ItemSprites sprites;

    void Start()
    {
        invHandler = GetComponent<InventoryHandler>();
    }
    
    public InventoryItem AddItem(string item)
    {
        InventoryItem newItem = null;

        if (item == "HealingPotion")
            newItem = NewHealingPotion();
        else if (item == "ManaPotion")
            newItem = NewManaPotion();
        else if (item == "Antidote")
            newItem = NewAntidote();
        else if (item == "Ointment")
            newItem = NewOintment();
        else if (item == "ConfusionHeal")
            newItem = NewConfusionHeal();
        else if (item == "ClairvoyancePotion")
            newItem = NewClaivoyancePotion();
        else if (item == "GoldenHitPotion")
            newItem = NewGoldenHitPotion();
        else if (item == "BasicSword")
            newItem = NewBasicSword();
        else if (item == "IronHelmet")
            newItem = NewIronHelmet();

        return newItem;
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

        // If you can equip this item
        bool equippable = false;

        // Which slot this item will equip in
        int equipSlot = -1;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.healingPotionSprite;

        // How much the item is worth in a shop
        int value = 10;

        // Info text about the item
        string infoText = "This potion will heal you when it's used. It restores 10 HP";

        // The method to invoke when you use the item
        string method = "HealingPotion";

        // Creates a new inventoryitem     
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);
        
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

        // If you can equip this item
        bool equippable = false;

        // Which slot this item will equip in
        int equipSlot = -1;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.manaPotionSprite;

        // How much the item is worth in a shop
        int value = 10;

        // Info text about the item
        string infoText = "This potion will give back mana when used. It restores 5 mana";

        // The method to invoke when you use the item
        string method = "ManaPotion";

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

        // Returns the item we just created
        return item;
    }

    // Antidote/Poison Heal
    public InventoryItem NewAntidote()
    {
        // Name of the item we're creating
        string itemName = "Antidote";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = true;

        // If you can equip this item
        bool equippable = false;

        // Which slot this item will equip in
        int equipSlot = -1;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.antidoteSprite;

        // How much the item is worth in a shop
        int value = 15;

        // Info text about the item
        string infoText = "When you use this antidote it will remove a poison effect";

        // The method to invoke when you use the item
        string method = "Antidote";

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

        // Returns the item we just created
        return item;
    }

    // Ointment/Burn Heal
    public InventoryItem NewOintment()
    {
        // Name of the item we're creating
        string itemName = "Ointment";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = true;

        // If you can equip this item
        bool equippable = false;

        // Which slot this item will equip in
        int equipSlot = -1;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.ointmentSprite;

        // How much the item is worth in a shop
        int value = 15;

        // Info text about the item
        string infoText = "When you use this ointment it will remove a burn effect";

        // The method to invoke when you use the item
        string method = "Ointment";

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

        // Returns the item we just created
        return item;
    }

    // Confusion Heal
    public InventoryItem NewConfusionHeal()
    {
        // Name of the item we're creating
        string itemName = "Confusion Healing";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = true;

        // If you can equip this item
        bool equippable = false;

        // Which slot this item will equip in
        int equipSlot = -1;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.confusionHealSprite;

        // How much the item is worth in a shop
        int value = 15;

        // Info text about the item
        string infoText = "When you use this potion it will remove a confusion effect";

        // The method to invoke when you use the item
        string method = "ConfusionHeal";

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

        // Returns the item we just created
        return item;
    }

    // Clairvoyance
    public InventoryItem NewClaivoyancePotion()
    {
        // Name of the item we're creating
        string itemName = "Clairvoyance Potion";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = true;

        // If you can equip this item
        bool equippable = false;

        // Which slot this item will equip in
        int equipSlot = -1;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.clairvoyancePotionSprite;

        // How much the item is worth in a shop
        int value = 15;

        // Info text about the item
        string infoText = "When you use this potion, the number of notes of your next attack will be halved";

        // The method to invoke when you use the item
        string method = "ClairvoyancePotion";

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

        // Returns the item we just created
        return item;
    }

    // Golden hit
    public InventoryItem NewGoldenHitPotion()
    {
        // Name of the item we're creating
        string itemName = "Golden Hit Potion";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = true;

        // If you can equip this item
        bool equippable = false;

        // Which slot this item will equip in
        int equipSlot = -1;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.goldenHitSprite;

        // How much the item is worth in a shop
        int value = 15;

        // Info text about the item
        string infoText = "When you use this potion, the combo of your next attack will only have golden notes";

        // The method to invoke when you use the item
        string method = "GoldenHitPotion";

        // Creates a new inventoryitem
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

        // Returns the item we just created
        return item;
    }
    #endregion

    #region Equippable Items
    // Iron Helmet
    public InventoryItem NewIronHelmet()
    {
        // Name of the item we're creating
        string itemName = "Iron Helmet";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = false;

        // If you can equip this item
        bool equippable = true;

        // Which slot this item will equip in
        int equipSlot = 0;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.ironHelmetSprite;

        // How much the item is worth in a shop
        int value = 10;

        // Info text about the item
        string infoText = "A sturdy iron helmet that will protect your head from attacks.";

        // The method to invoke when you use the item
        string method = "null";

        // Creates a new inventoryitem     
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

        // Returns the item we just created
        return item;
    }

    public InventoryItem NewBasicSword()
    {
        // Name of the item we're creating
        string itemName = "Iron Sword";

        // If this item is stackable or if we can only have one in each slot
        bool stackable = false;

        // If you can equip this item
        bool equippable = true;

        // Which slot this item will equip in
        int equipSlot = 4;

        // The image that will represent the item in menu's and such
        Sprite itemImage = sprites.swordSprite;

        // How much the item is worth in a shop
        int value = 10;

        // Info text about the item
        string infoText = "The sword you started your adventure with. Pretty basic but it gets the job done.";

        // The method to invoke when you use the item
        string method = "null";

        // Creates a new inventoryitem     
        InventoryItem item = new InventoryItem(itemName, stackable, equippable, equipSlot, itemImage, value, infoText, method);

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

    public Sprite ointmentSprite;

    public Sprite confusionHealSprite;

    public Sprite clairvoyancePotionSprite;

    public Sprite goldenHitSprite;

    public Sprite ironHelmetSprite;

    public Sprite swordSprite;
}
