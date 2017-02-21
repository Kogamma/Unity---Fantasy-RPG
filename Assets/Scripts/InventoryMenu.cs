using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryMenu : MonoBehaviour
{
    private StandaloneInputModule _inputModule;
    private EventSystem _eventSystem;

    // The buttons that represent items in the inventory menu
    public GameObject[] itemButtons;

    public GameObject eventObj;

    // The current items in the inventory that are shown in the menu
    int[] currentItems;

	void Start ()
    {
        // Sets both components to be those that the EventSystem object in the scene has
        _inputModule = eventObj.GetComponent<StandaloneInputModule>();
        _eventSystem = eventObj.GetComponent<EventSystem>();

        // Gets the inventory from our PlayerSingleton
        List<InventoryItem> inv = PlayerSingleton.instance.playerInventory;
        
        // How many buttons we will iterate
        int iteratorLength = itemButtons.Length;
        // If there are more buttons than there are items in the inventory,
        // we iterate the amount of items in the inventory instead 
        if (iteratorLength > inv.Count)
            iteratorLength = inv.Count;

        // Initializes and sets the length of the list of currentItems
        currentItems = new int[iteratorLength];

        // Loops through them to give them all a default value of -1
        // -1 = no item
        for (int i = 0; i < currentItems.Length; i++)
        {
            currentItems[i] = -1;
        }

        for (int i = 0; i < iteratorLength; i++)
        {
            // Adds the item's sprite to the item button
            itemButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = inv[i].itemImage;

            // Adds the name of the item to the item button
            itemButtons[i].transform.GetChild(1).GetComponent<Text>().text = inv[i].itemName;

            // Adds the amount of the item to the item button
            itemButtons[i].transform.GetChild(2).GetComponent<Text>().text = "x" + inv[i].amountOfItem;

            // Sets the first values of the current items shown
            currentItems[i] = i;
        }
    }
	
	void Update ()
    {
        // Checks if we go down in the input module
		if(Input.GetAxis(_inputModule.verticalAxis) < 0)
        {
            // Checks if we're on the bottom of the list
            if(_eventSystem.currentSelectedGameObject == itemButtons[itemButtons.Length - 1])
            {
                // Checks if we have any more items to show on the bottom
                //if()
            }
        }
	}
}
