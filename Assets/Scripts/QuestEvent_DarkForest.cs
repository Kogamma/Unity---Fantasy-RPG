using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent_DarkForest : MonoBehaviour
{
    public GameObject gadhaGoz;
    public GameObject dragon;
    public QuestDisplay questDisplay;
    public TextBoxHandler textBox;
    public Transform player;
    public GameObject questUpdateTrigger;


	void Start ()
    {
        if (PlayerSingleton.instance.activeQuestIndex == 2)
        {
            if (PlayerSingleton.instance.questStages[2] >= 3)
            {
                player.position = new Vector3(60, 3, 182);
                player.rotation = Quaternion.Euler(0, 300, 0);
                dragon.SetActive(false);
                gadhaGoz.SetActive(true);
            }

            if (PlayerSingleton.instance.questStages[2] >= 1)
                questUpdateTrigger.SetActive(false);
        }
        else if (PlayerSingleton.instance.activeQuestIndex > 2)
            dragon.SetActive(false);
	}


    public void GadhaGozMessage()
    {
        string[] text = new string[6]
        {
            "NO! You defeated my pet, Fred The Dog!",
            "Yes, it's me, the famous Gadha Goz who also asked you to defeat the beast.",
            "Why, you may wonder?",
            "I didn't expect you to actually make it this far, I was hoping for you to meet your demise!",
            "Oh well. I am a honorable man... cat and I know when I'm defeated.",
            "Just take the key, return to your precious world, I won't stop you..."
        } ;

        textBox.PrintMessage(text, "Gadha Goz", questDisplay.gameObject, "CompleteQuest");
    }
}
