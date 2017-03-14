using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class CombatInventory : MonoBehaviour
{
    private StandaloneInputModule _inputModule;
    private EventSystem _eventSystem;

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

    bool updateInfoText = false;

    // A reference to the CombatScript in the combat scene
    public CombatScript combatScript;

    public MenuManagment menuManager;

    void Start()
    {
        // Sets both components to be those that the EventSystem object in the scene has
        _inputModule = eventObj.GetComponent<StandaloneInputModule>();
        _eventSystem = eventObj.GetComponent<EventSystem>();


        _eventSystem.SetSelectedGameObject(itemButtons[0]);

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
    }

    void Update()
    {
        UpdateButtons();

        // Shows how full the inventory is
        invCountText.text = PlayerSingleton.instance.playerInventory.Count + "/" + PlayerSingleton.instance.inventorySize;

        // Vertical input for navigating the list of items
        float listInput = Input.GetAxis(_inputModule.verticalAxis);

        // Checks if we press up or down in the input module
        if (listInput != 0)
        {
            ListNavigation(listInput);
        }

        UpdateItems();
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
                if (handler.items[currentItemIndexes[currentItems[i]]].equippable)
                    itemButtons[i].GetComponent<Button>().interactable = false;
                else
                    itemButtons[i].GetComponent<Button>().interactable = true;
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

    // Moves down in the item list if we can
    public void ListNavigation(float direction)
    {
        int incrementer = 0;

        // Checks what direction of input we made
        if (direction < 0)
        {
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

        _eventSystem.SetSelectedGameObject(itemOptions.transform.GetChild(1).gameObject);

        canClick = false;
    }

    // Uses the effect of the item selected
    public void UseItem()
    {
        // If we should go to the next turn in the end of this method
        bool goToNextTurn = true;

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
            else if (textPages[0].Contains("Confusion"))
            {
                textPages.Clear();
                textPages.Add("You are not confused, you don't need any confusion healing!");
            }

            goToNextTurn = false;
        }
        // If the item we used was an antidote, we call the remove poison method in combatscript
        else if (textPages[0] == "Antidote")
        {
            combatScript.RemovePoison();
            textPages.RemoveAt(0);
        }
        else if (textPages[0] == "ConfusionHealing")
        {
            combatScript.RemoveConfusion();
            textPages.RemoveAt(0);
        }
        else if (textPages[0] == "ClairvoyancePotion")
        {
            combatScript.ActivateClairvoyance();
            textPages.RemoveAt(0);
        }
        else if (textPages[0] == "GoldenHitPotion")
        {
            combatScript.ActivateGoldenHit();
            textPages.RemoveAt(0);
        }
        // Hides the info box
        itemInfoText.transform.parent.gameObject.SetActive(false);

        // Hides the item options window
        itemOptions.SetActive(false);
       
        // Sets it so we can't click on anything while the message is printing
        canClick = false;

        // If the item was actually used or not
        if (goToNextTurn)
        {
            // Removes the item when we use it
            GetComponent<InventoryHandler>().RemoveItem(currentItemIndexes[currentItems[currentItem]]);

            // Prints the message the item usage makes and ends the turn
            textBox.PrintMessage(textPages, this.gameObject, "EndTurn");
        }
        else if(!goToNextTurn)
        {   
            // Prints the message that the item will not be used and does NOT end the turn
            textBox.PrintMessage(textPages, this.gameObject, "CloseItem");
        }
    }

    // Throws away the item selected
    public void ThrowAwayItem()
    {
        // Calls the function to remove the item, if there is no items left then in this stack we close the window also
        if (GetComponent<InventoryHandler>().RemoveItem(currentItemIndexes[currentItems[currentItem]]))
        {
            // Closes the item options window
            CancelButton();
        }
    }


    // Cancels to take away the item options menu
    public void CancelButton()
    {
        // Deactivates the item options menu, the equip options menu and the switchequip menu
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

        // Sets the current item to none chosen
        currentItem = -1;

        // Makes it so you can use the buttons again
        canClick = true;
    }

    // Ends the players turn after using an item
    public void EndTurn()
    {
        // Activates the item info box after printing a message
        itemInfoText.transform.parent.gameObject.SetActive(true);

        // Closes the item options window
        CancelButton();

        // Goes to the enemies turn
        combatScript.UpdateTurn("Enemy");

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void CloseItem()
    {
        // Activates the item info box after printing a message
        itemInfoText.transform.parent.gameObject.SetActive(true);

        // Closes the item options window
        CancelButton();
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

    public void ItemButtonExit()
    {
        // The code can manually update the text
        updateInfoText = true;

        // Resets text to default
        itemInfoText.text = "Choose an item to see info about it.";
    }
}
