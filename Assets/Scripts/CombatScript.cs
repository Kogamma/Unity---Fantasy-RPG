using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Camera _playerCamera;
    [SerializeField] Camera _EnemyCamera;

    public enum cameraState {PLAYER , ENEMY , MAIN };
    public GameObject player;
    public int currentState;


	// Use this for initialization
	void Start ()
    {
        player.GetComponent<Animator>();
        _playerCamera.enabled = false;
        _EnemyCamera.enabled = false;
        currentState = (int)cameraState.MAIN;
	}
	
	// Update is called once per frame
	public void ChangeViewPort (cameraState viewState)
    {

        if (viewState == cameraState.MAIN)
        {
            
            _playerCamera.enabled = false;
            _EnemyCamera.enabled = false;
            _mainCamera.enabled = true;
            currentState = (int)cameraState.MAIN;

        }

        else if (viewState == cameraState.PLAYER)
        {
            _playerCamera.enabled = true;
            _EnemyCamera.enabled = false;
            _mainCamera.enabled = false;
            currentState = (int)cameraState.PLAYER;
        }

        else if (viewState == cameraState.ENEMY)
        {
            _playerCamera.enabled = false;
            _mainCamera.enabled = false;
            _EnemyCamera.enabled = true;
            currentState = (int)cameraState.ENEMY;

        }

    }

    /*public IEnumerator PlayerAttack()
    {
        ChangeViewPort(cameraState.PLAYER);
        player.GetComponent<PlayerCombatLogic>().Invoke(attack, 4);
        yield return 0;
    }*/

    public void PlayerAttack(string attack)
    {
        ChangeViewPort(cameraState.PLAYER);
        player.GetComponent<PlayerCombatLogic>().Invoke(attack, 0.5f);
    }

}
