using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public List<string> items = new List<string>();

    public InventoryHandler inventHanlder;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInteract()
    {
        if (name.Contains("Chest"))
            gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Open");

        for(int i = 0; i < items.Count; i++)
        {
            inventHanlder.AddItem(items[i]);
        }
        gameObject.tag = "Uninteractable";
    }
}
