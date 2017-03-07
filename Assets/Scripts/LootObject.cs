using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public List<string> items = new List<string>();

    public InventoryHandler inventHanlder;

    public TextBoxHandler textBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInteract()
    {
        List<string> text = new List<string>();
        if (name.Contains("Chest"))
            gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Open");

        text.Add("You got ");
        for(int i = 0; i < items.Count; i++)
        {

            if(inventHanlder.AddItem(items[i]))
            {
                if (items[i] == "HealingPotion")
                    text[0] += "Potion of healing ";
                else if (items[0] == "ManaPotion")
                    text[0] += "Mana Potiion ";

                items.RemoveAt(i);
            }
        }

        if(items.Count > 0)
        {
            text.Add("Your inventory is full, this items are still in the chest ");
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i] == "HealingPotion")
                    text[0] += "Potion of healing ";
                else if (items[0] == "ManaPotion")
                    text[0] += "Mana Potiion ";
            }
        }

        textBox.StartMessage(text.ToArray(), "Chest", null, null);

        gameObject.tag = "Uninteractable";
    }
}
