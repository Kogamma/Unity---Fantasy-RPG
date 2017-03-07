using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRes_Script : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
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
    }
}
