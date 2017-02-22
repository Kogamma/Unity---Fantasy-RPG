﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour {

    // The textbox
    public TextBoxHandler textBox;

    // Name of the thing you're talking to
    public string messagerName;

    // The text that will be sent to the textbox
    [TextArea]
    public string[] textPages;

    // If you want to call a method after the textbox is done
    public GameObject methodHolder;
    public string methodName;

	public void OnInteract()
    {
        textBox.StartMessage(textPages, messagerName, methodHolder, methodName);
    }
}
