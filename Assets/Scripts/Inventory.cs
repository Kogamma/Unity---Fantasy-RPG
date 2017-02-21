using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
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
        // Loops through the inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            // Checks if there is already a item like the one we're adding
            if (inventory[i].itemName == itemToAdd.itemName)
            {
                // If there is less than 99 of those items...
                if (inventory[i].amountOfItem < 99)
                {
                    // ... we can just add to that amount
                    inventory[i].AddItem();
                }
                break;
            }
            else
            {
                // If we're on the last iteration
                if(i == inventory.Count - 1)
                {
                    // Checks if there are any slots that are vacant in the inventory
                    if(inventory.Count < PlayerSingleton.instance.inventorySize)
                    {
                        inventory.Add(itemToAdd);
                    }
                }
            }
        }
    }

    public void RemoveItem(int index)
    {
        // Creates a variable for the item that will be destroyed
        InventoryItem itemToDestroy = inventory[index];

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
        }

        // Destroys the item also
        Destroy(itemToDestroy);
    }
}
