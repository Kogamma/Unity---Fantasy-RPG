using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatScript : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Camera _playerCamera;
    [SerializeField] Camera _EnemyCamera;

    public string currentTurn = "Player";

    public enum cameraState {PLAYER , ENEMY , MAIN };
    public GameObject player;
    public GameObject playerMenu;
    public GameObject returnToWorldButton;
    public int currentState;
    public CombatTextBoxHandler textBox;
    public GameObject enemyHolder;
    public GameObject playerHealth;

	// Use this for initialization
	void Start ()
    {
        player.GetComponent<Animator>();
        _playerCamera.enabled = false;
        _EnemyCamera.enabled = false;
        returnToWorldButton.SetActive(false);
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
            playerHealth.SetActive(true);

        }

        else if (viewState == cameraState.PLAYER)
        {
            _playerCamera.enabled = true;
            _EnemyCamera.enabled = false;
            _mainCamera.enabled = false;
            currentState = (int)cameraState.PLAYER;
            playerHealth.SetActive(false);
        }

        else if (viewState == cameraState.ENEMY)
        {
            _playerCamera.enabled = false;
            _mainCamera.enabled = false;
            _EnemyCamera.enabled = true;
            currentState = (int)cameraState.ENEMY;
            playerHealth.SetActive(false);
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
                    string[] text = new string[2] { "The enemy defeated you and you died!", "GAME OVER" };
                    player.GetComponent<Animator>().SetTrigger("Dead");
                    textBox.PrintMessage(text, gameObject, "LoadGameOver");
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
    public void ShowVictoryMenu()
    {
        returnToWorldButton.SetActive(true);
    }

    public void ReturnToTheWorld()
    {
        SceneManager.LoadScene("Abraham_Test_Scene");
    }

    public void LoadGameOver()
    {
        Debug.Log("adsadasdadasd");
        SceneManager.LoadScene("DeathScene");
    }
}
