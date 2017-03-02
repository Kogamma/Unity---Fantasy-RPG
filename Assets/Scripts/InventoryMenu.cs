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

    // The buttons that represent items in the inventory menu
    public GameObject[] itemButtons;

    // The arrow buttons in the menu
    public Button[] arrowButtons;

    // This is the window that pops up when we press an item
    public GameObject itemOptions;
    // This is the window that pops up when we press an equippable item
    public GameObject equipOptions;
    // This is the window that pops up when we have to choose if we want to switch equipment
    public GameObject switchEquipOptions;

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

    // Holds the inventory menu
    public GameObject inventoryHolder;
    // Holds the equipment menu
    public GameObject equipmentHolder;

    public GameObject enterInventory;

    /* GameObjects that hold the different slots
    of the equipment menu*/
    public GameObject headSlot;
    public GameObject chestSlot;
    public GameObject legSlot;
    public GameObject ringSlot;
    public GameObject weaponSlot;
    public GameObject amuletSlot;

    public GameObject[] equipmentSlots;

    // Used for closing the different menu's
    public GameObject crossButton;

    string currentMenu = "Select";

    bool updateInfoText = false;

	void Start ()
    {
        // Sets both components to be those that the EventSystem object in the scene has
        _inputModule = eventObj.GetComponent<StandaloneInputModule>();
        _eventSystem = eventObj.GetComponent<EventSystem>();

        // Disables both the inventories
        DisableInventories();

        _eventSystem.SetSelectedGameObject(enterInventory);

        // How many buttons we will iterate
        iteratorLength = itemButtons.Length;
        // If there are more buttons than there are items in the inventory,
        // we iterate the amount of items in the inventory instead 
        if (iteratorLength > PlayerSingleton.instance.playerInventory.Count)
            iteratorLength = PlayerSingleton.instance.playerInventory.Count;

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

        // Initializes the equipmentslots array
        equipmentSlots = new GameObject[6];
        // Puts the equipment slots in the array
        equipmentSlots[0] = headSlot;
        equipmentSlots[1] = chestSlot;
        equipmentSlots[2] = legSlot;
        equipmentSlots[3] = ringSlot;
        equipmentSlots[4] = weaponSlot;
        equipmentSlots[5] = amuletSlot;

        // Adds all the events to the item buttons
        /*for (int i = 0; i < itemButtons.Length; i++)
        {
            // Gets the currentbutton
            Button currentButton = itemButtons[i].GetComponent<Button>();

            // Adds a listener for the click event on this button
            currentButton.onClick.AddListener(() => { ItemButtonPress(i); });
        }*/
    }
	
	void Update ()
    {
        UpdateButtons();

        UpdateItems();
       // UpdateEquipment();

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
        #region Inventory

        InventoryHandler handler = GetComponent<InventoryHandler>();

        // How many buttons we will iterate
        iteratorLength = itemButtons.Length;

        for (int i = 0; i < iteratorLength; i++)
        {
            // Checks if there is anything in that inventory slot
            if (currentItemIndexes[currentItems[i]] != -1)
            {
                if (canClick && currentMenu == "Inventory")
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
                itemButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = handler.items[currentItemIndexes[currentItems[i]]].itemImage;
                itemButtons[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                // Adds the name of the item to the item button
                itemButtons[i].transform.GetChild(1).GetComponent<Text>().text = handler.items[currentItemIndexes[currentItems[i]]].itemName;

                // Adds the amount of the item to the item button
                itemButtons[i].transform.GetChild(2).GetComponent<Text>().text = "x" + handler.items[currentItemIndexes[currentItems[i]]].amountOfItem;
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
                itemInfoText.text = handler.items[currentItemIndexes[currentItems[currentItem]]].infoText;
            }     
            else
                itemInfoText.text = "Choose an item to see info about it.";
        }
        else
            itemInfoText.text = "Choose an item to see info about it.";

        #endregion
    }

    public void UpdateEquipment()
    {
        // Loops through all the equipment slots on the player
        for (int i = 0; i < PlayerSingleton.instance.equippedItems.Length; i++)
        {
            // Checks if we have any equipment on that slot
            if(GetComponent<InventoryHandler>().items[i].itemName != "null")
            {
                // Disables the text that shows what item will go in this slot
                equipmentSlots[i].GetComponentInChildren<Text>().enabled = false;

                // Sets what image to display in the equipment slot
                equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = GetComponent<InventoryHandler>().items[i].itemImage;

                // Enables the preview image for the equipment
                equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
            }
            // If we don't have any equipment in that slot
            else
            {
                // Enables the text that shows what item will go in this slot
                equipmentSlots[i].GetComponentInChildren<Text>().enabled = true;

                // Disables the preview image for the equipment
                equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    // Moves down in the item list if we can
    public void ListNavigation(float direction)
    {
        int incrementer = 0;

        // Checks what direction of input we made
        if (direction < 0)
        {
            Debug.Log("down");
            // Checks if we're on the bottom of the list and that we have more items to show below
            if (_eventSystem.currentSelectedGameObject == itemButtons[itemButtons.Length - 1]
                && currentItems[currentItems.Length - 1] + 1 < PlayerSingleton.instance.playerInventory.Count)
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
            Debug.Log("up");
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

    // Moves down in the item list if we can with the buttons in the UI
    public void ButtonListNavigation(int direction)
    {
        int incrementer = 0;

        // If we pressed the 'up' arrow in the UI
        if (direction > 0)
        {
            // Check so we're not already at the top
            if (currentItems[0] > 0)
            {
                incrementer = -1;
            }
        }
        else if (direction < 0)
        {
            // Checks if we're at the bottom of the list
            if (currentItems[currentItems.Length - 1] + 1 < PlayerSingleton.instance.playerInventory.Count)
            {
                incrementer = 1;
            }
        }
        // Changes the values of the list accordingly
        for (int i = 0; i < currentItems.Length; i++)
        {
            currentItems[i] += incrementer;
        }
    }

    public void UpdateItems()
    {
        iteratorLength = itemButtons.Length;

        for (int i = 0; i < currentItemIndexes.Length; i++)
        {
            // If we're on a slot that has an item
            if (i <= PlayerSingleton.instance.playerInventory.Count - 1)
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

        /*for (int i = 0; i < PlayerSingleton.instance.playerInventory.Count; i++)
        {
            Debug.Log(PlayerSingleton.instance.playerInventory[i].itemName);
        }*/
    }
    // All the methods for when we are pressing the item buttons in the inventory list
    #region Item Button Press

    // Checks what kind of item we are pressing
    public void ItemButtonPress(int index)
    {
        // Checks if the item is equippable
        if (GetComponent<InventoryHandler>().items[currentItemIndexes[currentItems[index]]].equippable)
        {
            // Sets the position of the item options menu
            equipOptions.transform.position = itemButtons[index].transform.position;
            // Activates the item options
            equipOptions.SetActive(true);
        }
        else
        {
            // Sets the position of the item options menu
            itemOptions.transform.position = itemButtons[index].transform.position;
            // Activates the item options
            itemOptions.SetActive(true);
        }

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

        // Deactivates the cross button
        crossButton.GetComponent<Button>().enabled = false;

        // Sets that the current item selected is the button we just pressed
        currentItem = index;

        _eventSystem.SetSelectedGameObject(itemOptions.transform.GetChild(1).gameObject);

        canClick = false;
    }

    // Uses the effect of the item selected
    public void UseItem()
    {
        // If we actually used the item in this method or not
        bool usedItem = true;

        List<string> textPages = new List<string>();

        // Calls the method to use the item and we will also get a message back to print
        textPages = GetComponent<InventoryHandler>().items[currentItemIndexes[currentItems[currentItem]]].UseItem();

        // Checks if the first letters are not, which means the item should not be used
        if (textPages[0][0] == 'N' && textPages[0][1] == 'o' && textPages[0][2] == 't')
        {
            if (textPages[0].Contains("Health"))
            {
                textPages.Clear();
                textPages.Add("You already have the max amount of health!");
            }
            else if (textPages[0].Contains("Mana"))
            {
                textPages.Clear();
                textPages.Add("You already have the max amount of mana!");
            }
            else if (textPages[0].Contains("Antidote"))
            {
                textPages.Clear();
                textPages.Add("You are not poisoned, you don't need any antidote!");
            }

            usedItem = false;
        }
        // If the item we used was an antidote, we call the remove poison method in combatscript
        else if (textPages[0] == "Antidote")
        {
            PlayerSingleton.instance.poisoned = false;
            textPages.RemoveAt(0);
        }
        // Hides the info box
        itemInfoText.transform.parent.gameObject.SetActive(false);

        // Hides the item options window
        itemOptions.SetActive(false);

        // Sets it so we can't click on anything while the message is printing
        canClick = false;

        // If the item was actually used or not
        if (usedItem)
        {
            // Removes the item when we use it
            GetComponent<InventoryHandler>().RemoveItem(currentItemIndexes[currentItems[currentItem]]);

            // Prints the message the item usage makes and ends the turn
            textBox.PrintMessage(textPages, this.gameObject, "ActivateInfoBox");
        }
        else
        {
            // Prints the message the item usage makes and ends the turn
            textBox.PrintMessage(textPages, this.gameObject, "ActivateInfoBox");
        }
    }

    // Throws away the item selected
    public void ThrowAwayItem()
    {
        // Calls the function to remove the item, if there is no items left then in this stack we close the window also
        if(GetComponent<InventoryHandler>().RemoveItem(currentItemIndexes[currentItems[currentItem]]))
        {
            // Closes the item options window
            CancelButton();
        }
    }

    // Equips the current item we have selected
    public void EquipItem()
    {
        // Tries to equip the item and returns if we succeded or not
        if (GetComponent<InventoryHandler>().EquipItem(currentItemIndexes[currentItems[currentItem]]))
        {
            // If we succeded we just close the window and do nothing more
            CancelButton();
        }
        else
        {
            // If we couldn't equip the item we bring forth the switchequipment window
            switchEquipOptions.SetActive(true);

            // We also hide the equipment window
            equipOptions.SetActive(false);
        }

        UpdateItems();
    }

    // Cancels to take away the item options menu
    public void CancelButton()
    {
        // Deactivates the item options menu, the equip options menu and the switchequip menu
        itemOptions.SetActive(false);
        equipOptions.SetActive(false);
        switchEquipOptions.SetActive(false);

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

        // Activates the cross button
        crossButton.GetComponent<Button>().enabled = true;

        // Sets the current item to none chosen
        currentItem = -1;

        // Makes it so you can use the buttons again
        canClick = true;
    }
    
    // Activates the info text box
    public void ActivateInfoBox()
    {
        // Activates the item info box after printing a message
        itemInfoText.transform.parent.gameObject.SetActive(true);

        // Closes the item options window
        CancelButton();
    }
    #endregion

    #region Choose Menu Methods
    // Activates the inventory so the player can use it
    public void ActivateInventory()
    {
        // Activates the 'x' button and place it in the inventory menu
        crossButton.SetActive(true);
        crossButton.transform.SetParent(inventoryHolder.transform);
        crossButton.transform.SetSiblingIndex(1);
        crossButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, -40);

        // Gets all the buttons in the inventory menu
        List<Button> invButtons = new List<Button>();
        invButtons.AddRange(inventoryHolder.GetComponentsInChildren<Button>(true));
        invButtons.Remove(inventoryHolder.GetComponent<Button>());

        // Enables all those buttons
        for (int i = 0; i < invButtons.Count; i++)
        {
            invButtons[i].enabled = true;
        }

        _eventSystem.SetSelectedGameObject(invButtons[0].gameObject);

        // Enables the inventory and equipment buttons
        inventoryHolder.GetComponent<Button>().interactable = true;
        enterInventory.SetActive(false);
        equipmentHolder.GetComponent<Button>().interactable = false;

        inventoryHolder.GetComponent<Button>().enabled = false;
        equipmentHolder.GetComponent<Button>().enabled = true;

        currentMenu = "Inventory";
    }

    // Activates the equipment inventory so the player can use it
    public void ActivateEquipment()
    {
        // Activates the 'x' button and place it in the equipment menu
        crossButton.SetActive(true);
        crossButton.transform.SetParent(equipmentHolder.transform);
        crossButton.transform.SetSiblingIndex(1);
        crossButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, -40);

        // Gets all the buttons in the equipment menu
        List<Button> equipButtons = new List<Button>();
        equipButtons.AddRange(equipmentHolder.GetComponentsInChildren<Button>(true));
        equipButtons.Remove(equipmentHolder.GetComponent<Button>());


        for (int i = 0; i < equipButtons.Count; i++)
        {
            equipButtons[i].enabled = true;
        }

        _eventSystem.SetSelectedGameObject(equipButtons[0].gameObject);

        // Disables the inventory and equipment buttons
        inventoryHolder.GetComponent<Button>().interactable = false;
        enterInventory.SetActive(false);
        equipmentHolder.GetComponent<Button>().interactable = true;

        inventoryHolder.GetComponent<Button>().enabled = true;
        equipmentHolder.GetComponent<Button>().enabled = false;

        currentMenu = "Equipment";
    }

    // Disables both the inventory and equipment menu's buttons
    public void DisableInventories()
    {
        List<Button> equipButtons = new List<Button>();
        equipButtons.AddRange(equipmentHolder.GetComponentsInChildren<Button>(true));
        equipButtons.Remove(equipmentHolder.GetComponent<Button>());

        List<Button> invButtons = new List<Button>();
        invButtons.AddRange(inventoryHolder.GetComponentsInChildren<Button>(true));
        invButtons.Remove(inventoryHolder.GetComponent<Button>());

        for (int i = 0; i < invButtons.Count; i++)
        {
            invButtons[i].enabled = false;
        }

        for (int i = 0; i < equipButtons.Count; i++)
        {
            equipButtons[i].enabled = false;
        }

        // Enables the inventory and equipment buttons
        enterInventory.SetActive(true);
        //equipmentHolder.GetComponent<Button>().interactable = true;

        inventoryHolder.GetComponent<Button>().enabled = false;
        //equipmentHolder.GetComponent<Button>().enabled = true;

        crossButton.SetActive(false);

        _eventSystem.SetSelectedGameObject(enterInventory);

        currentMenu = "Select";
    }
    #endregion

    // Checks when we hover over something
    public void ItemButtonEnter(int index)
    {
        // The code cannot update the text on itself now
        updateInfoText = false;

        // Sets the text based on the button we're currently hovering over
        itemInfoText.text = GetComponent<InventoryHandler>().items[currentItemIndexes[currentItems[index]]].infoText;
    }

    public void EquipButtonEnter(int index)
    {
        // The code cannot update the text on itself now
        updateInfoText = false;

        // Sets the text based on the button we're currently hovering over
        itemInfoText.text = GetComponent<InventoryHandler>().equippedItems[index].infoText;
    }

    public void ItemButtonExit()
    {
        // The code can manually update the text
        updateInfoText = true;

        // Resets text to default
        itemInfoText.text = "Choose an item to see info about it.";
    }
}
