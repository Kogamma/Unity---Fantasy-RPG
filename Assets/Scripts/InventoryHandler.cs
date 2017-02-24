using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    // This is the actual inventory which consists of different InventoryItem
    [System.NonSerialized]
    public List<InventoryItem> inventory;

	void Start ()
    {
        inventory = PlayerSingleton.instance.playerInventory;
	}

    public void AddItem(InventoryItem itemToAdd)
    {
        inventory = PlayerSingleton.instance.playerInventory;

        // If the stack is full or if there are no items we have to add a new one
        bool addItemOnNewSlot = false;

        // Loops through the inventory if we have atleast 1 item
        if (inventory.Count > 0 && itemToAdd.stackable)
            for (int i = 0; i < inventory.Count; i++)
            {
                // Checks if there is already a item like the one we're adding
                if (inventory[i].itemName == itemToAdd.itemName)
                {
                    if (inventory[i].amountOfItem < 99)
                    {
                        // ... we can just add to that amount
                        inventory[i].IncrementItem();

                        break;
                    }
                }

                // If we're on the last iteration
                if (i == inventory.Count - 1)
                {
                    // If we have a full stack or if this is a new item                    
                    if (inventory[i].amountOfItem >= 99 || inventory[i].itemName != itemToAdd.itemName)
                    {
                        addItemOnNewSlot = true;

                        break;
                    }
                }
            }
        // If we don't have a single item in our inventory, 
        // we don't have to check if we can add one
        else if (inventory.Count <= 0 || !itemToAdd.stackable)
            addItemOnNewSlot = true;

        if (addItemOnNewSlot)
        {
            if (inventory.Count < PlayerSingleton.instance.inventorySize)
            {
                addItemOnNewSlot = false;
                inventory.Add(itemToAdd);
                inventory[inventory.Count - 1].IncrementItem();
            }
            else
            {
                Debug.Log("You can't add any more items to the inventory!");
            }
        }

        GetComponent<InventoryMenu>().UpdateItems();
    }

    public bool RemoveItem(int index)
    {
        // If we should close the window where we remove items 
        bool closeWindowOnReturn = false;

        // If we have more than one of the item we just subtract the amount of it
        if (inventory[index].amountOfItem > 1)
        { 
            inventory[index].amountOfItem--;
        }
        // If there is only 1 item left we remove it completely from the list
        else
        {
            // Removes the specified item from the list
            inventory.RemoveAt(index);
            closeWindowOnReturn = true;
        }

        // Updates the list of items in the inventory menu
        GetComponent<InventoryMenu>().UpdateItems();

        return closeWindowOnReturn;
    }
}
