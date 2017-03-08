using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContinueGameButton_Script : MonoBehaviour
{
    
    
    void Start()
    {
        if(PlayerSingleton.instance.loaded == false)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }
    public void Load()
    {
        PlayerSingleton.instance.Load();
    }

}
