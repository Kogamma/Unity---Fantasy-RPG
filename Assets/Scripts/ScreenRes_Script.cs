using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRes_Script : MonoBehaviour
{
    public GameObject[] setOffButton;

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void SetResolution(int index)
    {
        if(index == 0)
        {
            Screen.SetResolution (640, 480, Screen.fullScreen);
        }
        if (index == 1)
        {
            Screen.SetResolution(800, 600, Screen.fullScreen);
        }
        if (index == 2)
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
        if (index == 3)
        {
            Screen.SetResolution(1600, 900, Screen.fullScreen);
        }
        if (index == 4)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }

        for (int i = 0; i < setOffButton.Length; i++)
        {
            setOffButton[i].GetComponent<Button>().interactable = true;
        }

        setOffButton[index].GetComponent<Button>().interactable = false;

        if (index < setOffButton.Length - 1)
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(setOffButton[index + 1]);
        else
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(setOffButton[index - 1]);
    }
}
