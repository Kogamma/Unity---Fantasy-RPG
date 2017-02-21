using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // This is the actual inventory which consists of different InventoryItem
    public List<InventoryItem> inventory;

	void Start ()
    {

	}
	
	void Update ()
    {
		
	}

    public void RemoveItem(InventoryItem itemToRemove)
    {
        // Removes the specified item from the list
        inventory.Remove(itemToRemove);

        // Destroys the item also
        Destroy(itemToRemove);
    }
}
