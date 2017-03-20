using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour {


    //Check if the object is being shown
    [SerializeField]
    bool showObjective = false;

    //The texture
    [SerializeField]
    Image questBox;
    [SerializeField]
    Image questBorder;
    [SerializeField]
    Text questText;

    [TextArea]
    public string questInfo;

    //Check if we have already been in the trigger
    private int collision;

    void Start ()
    {
        showObjective = false;
        
    }

    void Update()
    {
		if (Input.GetKey(KeyCode.Q) && collision == 1)
        {
            showObjective = true;
        }

        if (Input.GetKeyUp(KeyCode.Q) && collision == 1)
        {
            showObjective = false;
        }
	}

    //When you enter the trigger
    void OnTriggerEnter(Collider other)
    {
        //This if statement will run
        if (other.gameObject.tag == "Player" && showObjective == false && collision == 0)
        {
            StartCoroutine(UpdateQuest());
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
    IEnumerator UpdateQuest()
    {
        questBox.color = new Color(questBox.color.r, questBox.color.g, questBox.color.b, 0);
        questBorder.color = new Color(questBorder.color.r, questBorder.color.g, questBorder.color.b, 0);

        questText.text = questInfo;
        questBox.gameObject.SetActive(true);

        float alpha = 0;

        while(questBox.color.a < 1)
        {
            alpha += 0.001f * Time.deltaTime;

            questBox.color = new Color(questBox.color.r, questBox.color.g, questBox.color.b, alpha);
            questBorder.color = new Color(questBorder.color.r, questBorder.color.g, questBorder.color.b, alpha);
        }

        yield return new WaitForSeconds(1.5f);

        while (questBox.color.a > 0)
        {
            alpha -= 0.001f * Time.deltaTime;

            questBox.color = new Color(questBox.color.r, questBox.color.g, questBox.color.b, alpha);
            questBorder.color = new Color(questBorder.color.r, questBorder.color.g, questBorder.color.b, alpha);
        }

        questBox.gameObject.SetActive(false);

    }
	

}
