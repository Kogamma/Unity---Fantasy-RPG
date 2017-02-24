using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

    [SerializeField] string sceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.CompareTag("Player"))
            SceneManager.LoadScene(sceneName);
    }
}
