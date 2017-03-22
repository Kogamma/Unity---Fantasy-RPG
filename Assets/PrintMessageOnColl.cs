using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintMessageOnColl : MonoBehaviour
{
    [TextArea]
    public string[] text;

    public TextBoxHandler textBox;

    
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            textBox.PrintMessage(text, null, null, null);
    }
}
