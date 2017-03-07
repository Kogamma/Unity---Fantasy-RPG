using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public InventoryItem[] equippedItems = new InventoryItem[6];

    void Awake()
    {
        UpdateItemList();

        UpdateEquipmentList();
    }

    public void UpdateItemList()
    {
        List<string> inventory = PlayerSingleton.instance.playerInventory;

        for (int i = 0; i < inventory.Count; i++)
        {
            if (i > items.Count - 1)
            {
                items.Add(GetComponent<ItemLibrary>().AddItem(inventory[i]));
                items[items.Count - 1].amountOfItem = PlayerSingleton.instance.inventoryAmounts[i];
            }
            else
            {
                items[i] = GetComponent<ItemLibrary>().AddItem(inventory[i]);
                items[i].amountOfItem = PlayerSingleton.instance.inventoryAmounts[i];
            }
            
        }
    }

    public void UpdateEquipmentList()
    {
        string[] equipment = PlayerSingleton.instance.equippedItems;

        for (int i = 0; i < equipment.Length; i++)
        {
            if (equipment[i] != "null")
            {
                equippedItems[i] = GetComponent<ItemLibrary>().AddItem(equipment[i]);
            }

        }
    }

    public bool AddItem(string itemStringToAdd)
    {
        // Gets the item we're adding via the string parameter value
        InventoryItem itemToAdd = GetComponent<ItemLibrary>().AddItem(itemStringToAdd);

        // If the stack is full or if there are no items we have to add a new one
        bool addItemOnNewSlot = false;

        // Loops through the inventory if we have atleast 1 item
        if (PlayerSingleton.instance.playerInventory.Count > 0 && itemToAdd.stackable)
            for (int i = 0; i < PlayerSingleton.instance.playerInventory.Count; i++)
            {
                // Checks if there is already a item like the one we're adding
                if (items[i].itemName == itemToAdd.itemName)
                {
                    if (items[i].amountOfItem < 99)
                    {
                        // ... we can just add to that amount
                        PlayerSingleton.instance.inventoryAmounts[i]++;
                        UpdateItemList();
                        break;
                    }
                }

                // If we're on the last iteration
                if (i == PlayerSingleton.instance.playerInventory.Count - 1)
                {
                    // If we have a full stack or if this is a new item                    
                    if (items[i].amountOfItem >= 99 || items[i].itemName != itemToAdd.itemName)
                    {
                        addItemOnNewSlot = true;

                        break;
                    }
                }
            }
        // If we don't have a single item in our inventory, 
        // we don't have to check if we can add one
        else if (PlayerSingleton.instance.playerInventory.Count <= 0 || !itemToAdd.stackable)
            addItemOnNewSlot = true;

        if (addItemOnNewSlot)
        {
            if (PlayerSingleton.instance.playerInventory.Count < PlayerSingleton.instance.inventorySize)
            {
                addItemOnNewSlot = false;
                PlayerSingleton.instance.playerInventory.Add(itemStringToAdd);
                PlayerSingleton.instance.inventoryAmounts.Add(1);
                UpdateItemList();
            }
            else
            {
                return false;
            }
        }


        if(GetComponent<CombatInventory>() != null)
            GetComponent<CombatInventory>().UpdateItems();
        else if (GetComponent<InventoryMenu>() != null)
            GetComponent<InventoryMenu>().UpdateItems();

        return true;
    }

    public bool RemoveItem(int index)
    {
        // If we should close the window where we remove items 
        bool closeWindowOnReturn = false;

        // If we have more than one of the item we just subtract the amount of it
        if (items[index].amountOfItem > 1)
        {
            PlayerSingleton.instance.inventoryAmounts[index]--;
            UpdateItemList();
        }
        // If there is only 1 item left we remove it completely from the list
        else
        {
            // Removes the specified item from the list
            PlayerSingleton.instance.playerInventory.RemoveAt(index);
            UpdateItemList();
            closeWindowOnReturn = true;
        }

        return closeWindowOnReturn;
    }

    // Equips the specified item
    public bool EquipItem(int invIndex)
    {
        // Gets the item we want to equip
        string itemStringToEquip = PlayerSingleton.instance.playerInventory[invIndex];

        InventoryItem itemToEquip = GetComponent<ItemLibrary>().AddItem(itemStringToEquip);

        // If we don't have an item equipped in that slot
        if(PlayerSingleton.instance.equippedItems[itemToEquip.equipSlot] == "null")
        {
            // We put the item in the list of equipped items
            PlayerSingleton.instance.equippedItems[itemToEquip.equipSlot] = itemStringToEquip;

            UpdateEquipmentList();

            // We remove the item from the inventory
            RemoveItem(invIndex);

            UpdateItemList();
        }
        // If we have an item equipped in the slot
        else
        {
            return false;
        }

        return true;
    }

    // Deequips the specified item
    public void DeequipItem()
    {

    }

    public void SwitchEquipItem()
    {

    }
}
