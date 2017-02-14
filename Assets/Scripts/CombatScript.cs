using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Camera _playerCamera;
    [SerializeField] Camera _EnemyCamera;

    public enum cameraState {PLAYER , ENEMY , MAIN };
    cameraState state;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        _playerCamera.enabled = false;
        _EnemyCamera.enabled = false;
	}
	
	// Update is called once per frame
	public void ChangeViewPort (cameraState state)
    {       
        if (state == cameraState.MAIN)
        {
            
            _playerCamera.enabled = false;
            _EnemyCamera.enabled = false;
            _mainCamera.enabled = true;

        }

        else if (state == cameraState.PLAYER)
        {
            _playerCamera.enabled = true;
            _EnemyCamera.enabled = false;
            _mainCamera.enabled = false;
        }

        else if (state == cameraState.ENEMY)
        {
            _playerCamera.enabled = false;
            _mainCamera.enabled = false;
            _EnemyCamera.enabled = true;
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
        player.GetComponent<PlayerCombatLogic>().Invoke(attack, 1);
    }
}
