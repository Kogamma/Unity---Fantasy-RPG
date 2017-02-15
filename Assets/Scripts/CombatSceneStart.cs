using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatSceneStart : MonoBehaviour {

    public Image blackScreen;
    bool fill = false;
    public float waitTime = 10.0f;


    void Update()
    {

            blackScreen.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        
    }
}
