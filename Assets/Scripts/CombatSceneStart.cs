using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatSceneStart : MonoBehaviour {

    public Image blackScreen;
    bool fill = false;
    public float waitTime = 10.0f;
    [TextArea]
    public string[] textPages;
    public GameObject textBox;

    void Start()
    {
        
        
    }

    void LateUpdate()
    {
            blackScreen.fillAmount -= 1.0f / waitTime * Time.deltaTime;

        if (blackScreen.fillAmount <=0)
        {
            textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(textPages);
        }
    }
}
