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
        //Getting the players animator
        player.GetComponent<Animator>();
        //Setting the player camera to false           
        _playerCamera.enabled = false;
        //Setting the enemy camera to false           
        _EnemyCamera.enabled = false;
        //Setting the return button to false               
        returnToWorldButton.SetActive(false);
        //Setting the currentstate to main       
        currentState = (int)cameraState.MAIN;       
	}

    //This function will change which camera that will be active
	public void ChangeViewPort (cameraState viewState)
    {
        //Check if viewstate is equal to MAIN
        if (viewState == cameraState.MAIN)
        {
            //Setting all the other cameras to false expect main, currentState will be equal to MAIN and the playerHealth bar will be set to true
            _playerCamera.enabled = false;
            _EnemyCamera.enabled = false;
            _mainCamera.enabled = true;
            currentState = (int)cameraState.MAIN;
            playerHealth.SetActive(true);

        }
        //Check if viewstate is equal to Player
        else if (viewState == cameraState.PLAYER)
        {
            //Setting all the other cameras to false expect player, currentState will be equal to PLAYER and the playerHealth bar will be set to false
            _playerCamera.enabled = true;
            _EnemyCamera.enabled = false;
            _mainCamera.enabled = false;
            currentState = (int)cameraState.PLAYER;
            playerHealth.SetActive(false);
        }
        //Check if viewstate is equal to Enemy
        else if (viewState == cameraState.ENEMY)
        {
            //Setting all the other cameras to false expect enemy, currentState will be equal to enemy and the playerHealth bar will be set to false
            _playerCamera.enabled = false;
            _mainCamera.enabled = false;
            _EnemyCamera.enabled = true;
            currentState = (int)cameraState.ENEMY;
            playerHealth.SetActive(false);
        }
    }
    //This function will update the turns in the combat
    public void UpdateTurn(string turn)
    {
        //currentTurn will be equal to turn.
        currentTurn = turn;
        switch (currentTurn)
        {
            //Check if the currentTurn is equal to Player
            case "Player":
                //Check if the players health is less or equal to zero
                if (PlayerSingleton.instance.playerHealth <= 0)
                {
                    //If it is a textbox will show, a death animation for the player will be played
                    player.GetComponent<Animator>().SetTrigger("Dead");
                    string[] text = new string[2] { "The enemy defeated you and you died!", "GAME OVER" };
                    textBox.PrintMessage(text, gameObject, "LoadGameOver");
                }
                
                else
                {
                    //Setting the player menu to true
                    playerMenu.SetActive(true);

                    enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isStunned = false;
                }
                break;
            //Check if currentTurn is equal to Enemy
            case "Enemy":
                if (enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isStunned == false)
                {
                    //Check if the enemy health is less or equal to zero
                    if (enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyHp <= 0)
                    {
                        //If it is a textbox will show, a death animation for the enamy will be played and a return to the world button will be showed 
                        enemyHolder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Dead");
                        string[] text = new string[1] { "You defeated the enemy, you got " + enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyExp + " exp!" };
                        textBox.PrintMessage(text, gameObject, "ShowVictoryMenu");
                    }

                    else
                    {
                        //Setting the player menu to false
                        playerMenu.SetActive(false);
                        //Calling the function EnemyAttack
                        EnemyAttack();
                    }
                }
                else
                {
                    string[] text = new string[1] { "The enemy got frozen! Its need to skip a trun" };
                    textBox.PrintMessage(text, null, null);
                    UpdateTurn("Player");
                }

                break;
        }
    }

    //This function will check which attack the player will use,
    public void PlayerAttack(string attack)
    {
        //Setting the player menu to false
        playerMenu.SetActive(false);
        //Changin the viewport to player
        ChangeViewPort(cameraState.PLAYER);
        player.GetComponent<PlayerCombatLogic>().whichAttack = attack;
        //Calling a the attack function from playercombatlogic, it will take 0.5 sec for it to start. 
        player.GetComponent<PlayerCombatLogic>().StartCoroutine(attack, 0.5f); 
    }
    //This function wil call the enemy attack pattern
    public void EnemyAttack()
    {
        //Change the viewport to ENEMY
        ChangeViewPort(cameraState.ENEMY);
        //Calling the child to the enemyHolder and calling AttackPattern
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
