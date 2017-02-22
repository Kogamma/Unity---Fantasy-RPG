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
    private string[] textPages;
    public GameObject textBox;
    [SerializeField] GameObject enemyHolder;
    [SerializeField] GameObject UIGroup;

    bool doOnce = true;


    void Start ()
    {
        textPages = new string[1];
        textPages[0] = "A wild " +  enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().displayName + " attacked you!";
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
            textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(textPages, this.gameObject, "ActivateUI");

            doOnce = false;
        }
    }


    public void ActivateUI()
    {
        UIGroup.SetActive(true);
    }
}
