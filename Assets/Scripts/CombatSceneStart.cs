using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatSceneStart : MonoBehaviour
{
    public Image blackScreen;
    bool fill = false;
    public float waitTime = 10.0f;
    [TextArea]
    private List<string> textPages;
    public CombatTextBoxHandler textBox;
    [SerializeField] GameObject enemyHolder;
    [SerializeField] GameObject UIGroup;
    [SerializeField] GameObject[] enemies = new GameObject[6];
    private GameObject enemy;

    bool doOnce = true;

    void Start ()
    {
        if (PlayerSingleton.instance.attackingEnemy == "Slime")
            enemy = Instantiate(enemies[0], enemies[0].transform.position, enemies[0].transform.rotation, enemyHolder.transform);

        else if (PlayerSingleton.instance.attackingEnemy == "Zombie")
            enemy = Instantiate(enemies[1], enemies[1].transform.position, enemies[1].transform.rotation, enemyHolder.transform);

        else if (PlayerSingleton.instance.attackingEnemy == "Bat")
            enemy = Instantiate(enemies[2], enemies[2].transform.position, enemies[2].transform.rotation, enemyHolder.transform);

        else if (PlayerSingleton.instance.attackingEnemy == "Ghost")
            enemy = Instantiate(enemies[3], enemies[3].transform.position, enemies[3].transform.rotation, enemyHolder.transform);

        else if (PlayerSingleton.instance.attackingEnemy == "Troll")
            enemy = Instantiate(enemies[4], enemies[4].transform.position, enemies[4].transform.rotation, enemyHolder.transform);

        else if (PlayerSingleton.instance.attackingEnemy == "Boss")
            enemy = Instantiate(enemies[5], enemies[5].transform.position, enemies[5].transform.rotation, enemyHolder.transform);

        textPages = new List<string>();
        textPages.Add("A " +  enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().displayName + " attacked you!");
    }


    void LateUpdate()
    {
        //When the battlescene has loaded
        //the blackscreen starts to go back
        blackScreen.fillAmount -= 1.0f / waitTime * Time.deltaTime;

        if (blackScreen.fillAmount <= 0 && doOnce)
        {
            Destroy(blackScreen.gameObject);

            //When the blackscreen is or less than 0
            //it prints out a textbox
            textBox.PrintMessage(textPages, this.gameObject, "ActivateUI");

            doOnce = false;
        }
    }


    public void ActivateUI()
    {
        UIGroup.SetActive(true);
    }
}
