using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMap : MonoBehaviour
{
    // All points for all areas
    public GameObject townPoint;
    public GameObject gatePoint;
    public GameObject hubWorldPoint;
    public GameObject forestPoint;
    public GameObject darkForestPoint;

    // Fogs for all areas
    public GameObject townFog;
    public GameObject gateFog;
    public GameObject hubWorldFog;
    public GameObject forestFog;
    public GameObject darkForestFog;

    // Arrays that the points and fogs will be in
    private GameObject[] points = new GameObject[5];
    private GameObject[] fogs = new GameObject[5];

    // All names of the scenes representing the areas
    private string[] sceneNames = new string[5];
    
    // The currently active scene
    private string currentScene;

	
    void Awake ()
    {
        // Assigns all points in the array
        points[0] = townPoint;
        points[1] = gatePoint;
        points[2] = hubWorldPoint;
        points[3] = forestPoint;
        points[4] = darkForestPoint;

        // Assigns all fogs in the array
        fogs[0] = townFog;
        fogs[1] = gateFog;
        fogs[2] = hubWorldFog;
        fogs[3] = forestFog;
        fogs[4] = darkForestFog;

        // Assigns all names in the array
        sceneNames[0] = "Town_Scene";
        sceneNames[1] = "Starting_Gate";
        sceneNames[2] = "HubWorld_Scene";
        sceneNames[3] = "Forest_Scene_1";
        sceneNames[4] = "dark_forest_1";
        
        System.Array.Resize(ref PlayerSingleton.instance.areaExplored, 5);
    }


	void Start ()
    {
        // Gets the current active scene from the singleton
        currentScene = PlayerSingleton.instance.currentScene;

        // Goes through every points
        for (int i = 0; i < points.Length; i++)
        {
            // Stops all animations
            points[i].GetComponent<Animator>().speed = 0;

            // Checks if the active scene has the same name as the one in the array
            if (currentScene == sceneNames[i])
            {
                // Starts the blinking animation of the point point with the same index as the name in the other array
                points[i].GetComponent<Animator>().speed = 1;
                // This area is now exlored
                PlayerSingleton.instance.areaExplored[i] = true;
            }
            
            // Removes the fogs for the explored areas
            if (PlayerSingleton.instance.areaExplored[i])
                fogs[i].SetActive(false);
        }
	}
}
