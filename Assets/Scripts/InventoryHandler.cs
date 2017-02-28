using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

    public void AddItem(InventoryItem itemToAdd)
    {
        // If the stack is full or if there are no items we have to add a new one
        bool addItemOnNewSlot = false;

        // Loops through the inventory if we have atleast 1 item
        if (PlayerSingleton.instance.playerInventory.Count > 0 && itemToAdd.stackable)
            for (int i = 0; i < PlayerSingleton.instance.playerInventory.Count; i++)
            {
                // Checks if there is already a item like the one we're adding
                if (PlayerSingleton.instance.playerInventory[i].itemName == itemToAdd.itemName)
                {
                    if (PlayerSingleton.instance.playerInventory[i].amountOfItem < 99)
                    {
                        // ... we can just add to that amount
                        PlayerSingleton.instance.playerInventory[i].IncrementItem();

                        break;
                    }
                }

                // If we're on the last iteration
                if (i == PlayerSingleton.instance.playerInventory.Count - 1)
                {
                    // If we have a full stack or if this is a new item                    
                    if (PlayerSingleton.instance.playerInventory[i].amountOfItem >= 99 || PlayerSingleton.instance.playerInventory[i].itemName != itemToAdd.itemName)
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
                PlayerSingleton.instance.playerInventory.Add(itemToAdd);
                PlayerSingleton.instance.playerInventory[PlayerSingleton.instance.playerInventory.Count - 1].IncrementItem();
            }
            else
            {
                Debug.Log("You can't add any more items to the inventory!");
            }
        }

        PlayerSingleton.instance.playerInventory = PlayerSingleton.instance.playerInventory;

        GetComponent<InventoryMenu>().UpdateItems();
    }

    public bool RemoveItem(int index)
    {
        // If we should close the window where we remove items 
        bool closeWindowOnReturn = false;

        // If we have more than one of the item we just subtract the amount of it
        if (PlayerSingleton.instance.playerInventory[index].amountOfItem > 1)
        {
            PlayerSingleton.instance.playerInventory[index].amountOfItem--;
        }
        // If there is only 1 item left we remove it completely from the list
        else
        {
            // Removes the specified item from the list
            PlayerSingleton.instance.playerInventory.RemoveAt(index);
            closeWindowOnReturn = true;
        }

        // Updates the list of items in the inventory menu
        GetComponent<InventoryMenu>().UpdateItems();

        return closeWindowOnReturn;
    }

    // Equips the specified item
    public bool EquipItem(int invIndex)
    {

        // Gets the item we want to equip
        InventoryItem itemToEquip = PlayerSingleton.instance.playerInventory[invIndex];
 
        // If we don't have an item equipped in that slot
        if(PlayerSingleton.instance.equippedItems[itemToEquip.equipSlot] == null)
        {
            // We put the item in the list of equipped items
            PlayerSingleton.instance.equippedItems[itemToEquip.equipSlot] = itemToEquip;

            // We remove the item from the inventory
            RemoveItem(invIndex);
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
}
