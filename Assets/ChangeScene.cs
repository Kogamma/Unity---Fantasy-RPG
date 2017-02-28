using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Vector3 spawnRot;


    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.CompareTag("Player"))
        {
            PlayerSingleton.instance.entryPos = spawnPos;
            PlayerSingleton.instance.entryRot = spawnRot;
            SceneManager.LoadScene(sceneName);
        }
    }
}
