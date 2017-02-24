using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class InventoryMenu : MonoBehaviour
{
    private StandaloneInputModule _inputModule;
    private EventSystem _eventSystem;

    List<InventoryItem> inv;

    // The buttons that represent items in the inventory menu
    public GameObject[] itemButtons;

    // The arrow buttons in the menu
    public Button[] arrowButtons;

    // This is the window that pops up when we press an item
    public GameObject itemOptions;

    public GameObject eventObj;

    // Counts how many slots that are filled in the inventory
    public Text invCountText;

    // Info text about the item currently chosen
    public Text itemInfoText;

    // The current items in the inventory that are shown in the menu
    int[] currentItems;
    // The index for all the items in the player's inventory
    int[] currentItemIndexes;

    // This is the item currently selected by the player
    int currentItem = -1;

    int iteratorLength;

    // Used for timing input
    float startTime;

    // The textbox that will write what we want
    public CombatTextBoxHandler textBox;

    // If we can click on the item buttons
    bool canClick = true;

	void Start ()
    {
        // Sets both components to be those that the EventSystem object in the scene has
        _inputModule = eventObj.GetComponent<StandaloneInputModule>();
        _eventSystem = eventObj.GetComponent<EventSystem>();

        // Selects the top item button as the first item selected
        _eventSystem.firstSelectedGameObject = itemButtons[0];

        // Gets the inventory from our PlayerSingleton
        inv = PlayerSingleton.instance.playerInventory;

        // How many buttons we will iterate
        iteratorLength = itemButtons.Length;
        // If there are more buttons than there are items in the inventory,
        // we iterate the amount of items in the inventory instead 
        if (iteratorLength > inv.Count)
            iteratorLength = inv.Count;

        // Initializes and sets the length of the list of currentItems
        currentItems = new int[itemButtons.Length];


        // Loops through them to give them all a default value of -1
        // -1 = no item
        for (int i = 0; i < currentItems.Length; i++)
        {
            currentItems[i] = i;
        }

        // What the current items shown has in their slots
        currentItemIndexes = new int[100];

        for (int i = 0; i < currentItemIndexes.Length; i++)
        {
            currentItemIndexes[i] = -1;
        }
    }
	
	void Update ()
    {
        UpdateButtons();

        // Shows how full the inventory is
        invCountText.text = PlayerSingleton.instance.playerInventory.Count + "/" + PlayerSingleton.instance.inventorySize;

        // Vertical input for navigating the list of items
        float listInput = Input.GetAxis(_inputModule.verticalAxis);

        // Checks if we press up or down in the input module
        if (listInput != 0 )
        {
            ListNavigation(listInput);
        }
    }

    public void UpdateButtons()
    {
        // Gets the inventory from our PlayerSingleton
        inv = PlayerSingleton.instance.playerInventory;

        // How many buttons we will iterate
        iteratorLength = itemButtons.Length;

        for (int i = 0; i < iteratorLength; i++)
        {
            // Checks if there is anything in that inventory slot
            if (currentItemIndexes[currentItems[i]] != -1)
            {
                if (canClick)
                {
                    // Activates the button component on slots that aren't empty
                    itemButtons[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    // Deactivates the button component on slots when we have the item options menu open
                    itemButtons[i].GetComponent<Button>().enabled = false;
                }

                // Adds the item's sprite to the item button
                itemButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = inv[currentItemIndexes[currentItems[i]]].itemImage;
                itemButtons[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                // Adds the name of the item to the item button
                itemButtons[i].transform.GetChild(1).GetComponent<Text>().text = inv[currentItemIndexes[currentItems[i]]].itemName;

                // Adds the amount of the item to the item button
                itemButtons[i].transform.GetChild(2).GetComponent<Text>().text = "x" + inv[currentItemIndexes[currentItems[i]]].amountOfItem;
            }
            // If there is nothing in the inventory slot...
            else
            {
                // Deactivates the button component on the empty slots
                itemButtons[i].GetComponent<Button>().enabled = false;

                // Clears the item button's sprite
                itemButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                itemButtons[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);

                // Sets the name to 'empty'
                itemButtons[i].transform.GetChild(1).GetComponent<Text>().text = "Empty";

                // Adds the amount of the item to the item button
                itemButtons[i].transform.GetChild(2).GetComponent<Text>().text = "";
            }
        }

        // Updates the info text
        if (itemOptions.activeSelf)
        {
            if (currentItem != -1)
            {
                itemInfoText.text = inv[currentItemIndexes[currentItems[currentItem]]].infoText;
            }
            else
                itemInfoText.text = "Choose an item to see info about it.";
        }
    }

    // Moves down in the item list if we can
    public void ListNavigation(float direction)
    {
        int incrementer = 0;

        List<InventoryItem> inv = PlayerSingleton.instance.playerInventory;

        // Checks what direction of input we made
        if (direction < 0)
        {
            // Checks if we're on the bottom of the list and that we have more items to show below
            if (_eventSystem.currentSelectedGameObject == itemButtons[itemButtons.Length - 1]
                && currentItems[currentItems.Length - 1] + 1 < inv.Count)
            {
                // Times the up and down input with the input modules repeat delay variable
                if (Time.time - startTime >= _inputModule.repeatDelay / 2)
                {
                    // Says that we should go down in the list
                    incrementer = 1;

                    // Restarts the timer
                    startTime = Time.time;
                }
            }
        }
        else if (direction > 0)
        { 
            // Checks if we're on the top of the list
            if (_eventSystem.currentSelectedGameObject == itemButtons[0] && currentItems[0] > 0)
            {
                // Times the up and down input with the input modules repeat delay variable
                if (Time.time - startTime >= _inputModule.repeatDelay)
                {
                    // Says that we should go up in the list
                    incrementer = -1;

                    // Restarts the timer
                    startTime = Time.time;
                }
            }
        }

        // Changes the values of the list accordingly
        for (int i = 0; i < currentItems.Length; i++)
        {
            currentItems[i] += incrementer;
        }
    }

    public void ButtonListNavigation(int direction)
    {
        int incrementer = 0;

        List<InventoryItem> inv = PlayerSingleton.instance.playerInventory;

        // Checks what direction of input we made
        if (direction < 0)
        {
            // Checks if we have more items to show below
            if (currentItems[currentItems.Length - 1] + 1 < inv.Count)
            {
                // Says that we should go down in the list
                incrementer = 1;
            }
        }
        else if (direction > 0)
        {
            // Checks if we're not on the first item
            if (currentItems[0] > 0)
            {
                // Says that we should go up in the list
                incrementer = -1;
            }
        }

        // Changes the values of the list accordingly
        for (int i = 0; i < currentItems.Length; i++)
        {
            currentItems[i] += incrementer;
        }
        for (int i = 0; i < currentItems.Length; i++)
        {
            Debug.Log(currentItems[i]);
        }
    }

    public void UpdateItems()
    {
        List<InventoryItem> inv = PlayerSingleton.instance.playerInventory;

        iteratorLength = itemButtons.Length;

        for (int i = 0; i < currentItemIndexes.Length; i++)
        {
            // If we're on a slot that has an item
            if (i <= inv.Count - 1)
            {
                // Sets the value of the current items
                currentItemIndexes[i] = i;
            }
            else
            {
                // If there is no item on this slot in the inventory, it will be set to -1
                currentItemIndexes[i] = -1;
            }
        }
    }

    public void ItemOptions(int index)
    {
        // Sets the position of the item options menu
        itemOptions.transform.position = itemButtons[index].transform.position;
        // Activates the item options
        itemOptions.SetActive(true);

        // Deactivates all the buttons in the item list
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].GetComponent<Button>().enabled = false;
        }

        // Deactivates the arrow buttons
        for (int i = 0; i < arrowButtons.Length; i++)
        {
            arrowButtons[i].GetComponent<Button>().enabled = false;
        }

        // Sets that the current item selected is the button we just pressed
        currentItem = index;

        canClick = false;
    }

    // Uses the effect of the item selected
    public void UseItem()
    {
        List<string> textPages = new List<string>();

        // Calls the method to use the item and we will also get a message back to print
        textPages = PlayerSingleton.instance.playerInventory[currentItemIndexes[currentItems[currentItem]]].UseItem();

        itemInfoText.transform.parent.gameObject.SetActive(false);

        itemOptions.SetActive(false);

        GetComponent<InventoryHandler>().RemoveItem(currentItem);

        canClick = false;

        // Prints the message the item usage makes
        textBox.PrintMessage(textPages, this.gameObject, "ActivateInfoBox");
    }

    // Throws away the item selected
    public void ThrowAwayItem()
    {
        // Calls the function to remove the item, if there is no items left then in this stack we close the window also
        if(GetComponent<InventoryHandler>().RemoveItem(currentItemIndexes[currentItems[currentItem]]))
        {
            CancelButton();
        }
    }

    // Cancels to take away the item options menu
    public void CancelButton()
    {
        // Deactivates the item option menu
        itemOptions.SetActive(false);

        // Reactivates all the buttons in the item list
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].GetComponent<Button>().enabled = true;
        }
        // Activates the arrow buttons
        for (int i = 0; i < arrowButtons.Length; i++)
        {
            arrowButtons[i].GetComponent<Button>().enabled = true;
        }

        currentItem = -1;

        canClick = true;
    }
    
    // Activates the info text box
    public void ActivateInfoBox()
    {
        itemInfoText.transform.parent.gameObject.SetActive(true);

        CancelButton();
    }
}
