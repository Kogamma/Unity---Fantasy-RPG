using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {


    //Check if the object is being shown
    [SerializeField]
    bool showObjective = false;

    //The texture
    [SerializeField]
    Texture objective;

    //Check if we have already been in the trigger
    [SerializeField]
    private int collision;

    void Start ()
    {
        showObjective = false;
	}

    //When you enter the trigger
    void OnTriggerEnter(Collider other)
    {
        //This if statement will run
        if (other.gameObject.tag == "Player" && showObjective == false && collision == 0)
        {
            showObjective = true;
        }
    }

    //When you exit the trigger
    void OntriggerExit(Collider other)
    {
        //This if statement will run
        if (other.gameObject.tag == "Player")
        {
            showObjective = false;
        }

        //This will set collision to false
        collision = 1;
    }
	
    //This is the size of the trigger
    void OnGUI()
    {
        if (showObjective == true)
        {
            GUI.DrawTexture(new Rect(Screen.width / 1.5f, Screen.height / 1.4f, 178, 178), objective);
        }
    }
	
	void Update()
    {
		if (Input.GetButton("showObj")&& collision == 1)
        {
            showObjective = true;
        }

        if (Input.GetButtonUp("showObj")&& collision == 1)
        {
            showObjective = false;
        }
	}
}
