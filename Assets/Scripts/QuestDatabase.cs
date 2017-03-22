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
        quests.Add(new Quest("Get to the forest", new string[] {
            "The Mysterious Man told you that a beast inside the dark forest holds the key to The Gate Between Worlds.\nGet to the light side of the forest first.",
        "A tree is blocking the path to the forest.\nA lumberjack asked you to get his friend so that they can take care of the tree.\nHe should be in Cinderella Town."}));

        quests.Add(new Quest("Get to the forest, for real", new string[] {
            "You asked the lumberjack to take care of the tree.\nIt should be removed now." }));

        UpdateQuestLog();
    }


    public static void UpdateQuestLog()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (PlayerSingleton.instance.activeQuestIndex > i)
                quests[i].completionStatus = (int)completionStatusEnum.COMPLETED;
            else if (PlayerSingleton.instance.activeQuestIndex == i)
                quests[i].completionStatus = (int)completionStatusEnum.ACTIVE;
            else if (PlayerSingleton.instance.activeQuestIndex < i)
                quests[i].completionStatus = (int)completionStatusEnum.HIDDEN;
        }
    }
}


public class Quest
{
    public string title;
    public string[] description;
    public int questStage = 0;
    public int completionStatus = (int)completionStatusEnum.HIDDEN;

    public Quest(string _title, string[] _description)
    {
        title = _title;
        description = _description;
        completionStatus = (int)completionStatusEnum.HIDDEN;
    }
}
