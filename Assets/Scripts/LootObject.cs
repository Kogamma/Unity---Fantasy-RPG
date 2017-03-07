using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public List<string> items = new List<string>();

    public InventoryHandler inventHanlder;

    public TextBoxHandler textBox;

    public void OnInteract()
    {
        List<string> text = new List<string>();
        int hpPotions = 0;
        int manaPotions = 0;
        int antidote = 0;

       int currentItems = items.Count;

        Debug.Log(currentItems);

        if (name.Contains("Chest"))
            gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Open");

        text.Add("You got ");
        for (int i = 0; i < items.Count; i++)
        {
            if (inventHanlder.AddItem(items[i]))
            {
                if (items[i] == "HealingPotion")
                    hpPotions++;

                else if (items[i] == "ManaPotion")
                    manaPotions++;

                items.RemoveAt(i);
                i--;
            }
        }

        Debug.Log(items.Count);

        Debug.Log(currentItems);
        if (hpPotions > 0)
            text[0] += "[" + hpPotions + "] Potion of healing ";
        if (manaPotions > 0)
            text[0] += "[" + manaPotions + "] Mana Potion ";

        if (currentItems == items.Count)
            text[0] = "";

        if (items.Count != 0)
        {
            hpPotions = 0;
            manaPotions = 0;
            text[0] += (" Your inventory is full, this items will be left behind");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == "HealingPotion")
                    hpPotions++;
                else if (items[i] == "ManaPotion")
                    manaPotions++;
            }

            if (hpPotions > 0)
                text[0] += "[" + hpPotions + "] Potion of healing ";
            if (manaPotions > 0)
                text[0] += "[" + manaPotions + "] Mana Potion ";
            textBox.StartMessage(text.ToArray(), "Chest", gameObject, "ReversAnim");
        }

        else
        {
            gameObject.tag = "Uninteractable";
            textBox.StartMessage(text.ToArray(), "Chest", null, null);
        }

        

        
    }

    void ReversAnim()
    {
        gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Close");
    }
}
