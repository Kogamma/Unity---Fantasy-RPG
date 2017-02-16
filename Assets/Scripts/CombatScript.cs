using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Camera _playerCamera;
    [SerializeField] Camera _EnemyCamera;

    public string currentTurn = "Player";

    public enum cameraState {PLAYER , ENEMY , MAIN };
    public GameObject player;
    public GameObject playerMenu;
    public int currentState;
    public CombatTextBoxHandler textBox;
    public GameObject enemyHolder;

	// Use this for initialization
	void Start ()
    {
        player.GetComponent<Animator>();
        _playerCamera.enabled = false;
        _EnemyCamera.enabled = false;
        currentState = (int)cameraState.MAIN;
	}

    void Update()
    {
        
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

    public void UpdateTurn(string turn)
    {
        currentTurn = turn;
        switch (currentTurn)
        {
            case "Player":
                if (PlayerSingleton.instance.playerHealth <= 0)
                {
                    string[] text = new string[1] { "OHH NOOO YOU DIED" };
                    player.GetComponent<Animator>().SetTrigger("Dead");
                    textBox.PrintMessage(text,gameObject, "ShowRestartMenu");
                }

                else
                {
                    playerMenu.SetActive(true);
                }
                break;
            case "Enemy":
                if (enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyHp <= 0)
                {
                    enemyHolder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Dead");
                    string[] text = new string[1] { "You defeated the enemy, you got " + enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyExp + " exp!" };
                    textBox.PrintMessage(text, gameObject, "ShowVictoryMenu");
                }
                else
                {
                    playerMenu.SetActive(false);
                    EnemyAttack();
                }

                break;
        }
    }

    public void PlayerAttack(string attack)
    {
        playerMenu.SetActive(false);
        ChangeViewPort(cameraState.PLAYER);
        player.GetComponent<PlayerCombatLogic>().StartCoroutine(attack, 0.5f); 
    }
    
    public void EnemyAttack()
    {
        ChangeViewPort(cameraState.ENEMY);
        enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().AttackPattern();
    }
    public void ShowRestartMenu()
    {

    }
    public void ShowVictoryMenu()
    {

    }
}
