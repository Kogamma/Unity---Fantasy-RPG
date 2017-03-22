using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum completionStatusEnum { HIDDEN, ACTIVE, COMPLETED };


public class QuestDatabase : MonoBehaviour
{
    [System.NonSerialized]
    public static List<Quest> quests = new List<Quest>();

    void Awake()
    {
        quests.Add(new Quest("Go to the forest", new string[] {
            "The Mysterious Man told you that a beast inside the dark forest holds the key to The Gate Between Worlds.\nGet to the light side of the forest first.",
            "A tree is blocking the path to the forest.\nA lumberjack asked you to get his friend so that they can take care of the tree.\nHe should be in Cinderella Town.",
            "You asked Birch to go and help his friend take care of the trees. They should be done now."}));

        quests.Add(new Quest("Go to The Dark Forest", new string[] {
            "You have reached The Light Forest. Now you have to go through The Light Forest and reach The Dark Forest"}));

        quests.Add(new Quest("Defeat the mighty beast!", new string[] {
            "You have reached The Dark Forest. Now you have to find and defeat the mighty beast that holds the key to your only way home!",
        "You have found the beasts location. Confront it and get the key so you can go back home!"}));

        quests.Add(new Quest("Open The Gate Between Worlds", new string[] {
            "You have defeated the mighty Dragon Boar and have gotten the key. Now you can open The Gate Between Worlds and go back home!"}));
    }
}


public class Quest
{
    public string title;
    public string[] description;
    public int completionStatus = (int)completionStatusEnum.HIDDEN;

    public Quest(string _title, string[] _description)
    {
        title = _title;
        description = _description;
        completionStatus = (int)completionStatusEnum.HIDDEN;
    }
}
