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
    public GameObject menuManager;
    public GameObject returnToWorldButton;
    public int currentState;
    public CombatTextBoxHandler textBox;
    public GameObject enemyHolder;
    public GameObject playerHealth;
    public bool enemyIsDead = false;

    private int playerPoisonedTurns = 0;
    private int enemyPoisonedTurns = 0;
    private int maxPoisonedTurns = 3;

    private int playerConfusedTurns;
    private int enemyConfusedTurns;
    private int maxConfusedTurns = 3;

    private EnemyClass enemyClass;

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

        enemyClass = enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>();
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
                    List<string> text = new List<string>();
                    text.Add("The enemy defeated you and you died!");
                    text.Add("GAME OVER");
                    textBox.PrintMessage(text, gameObject, "LoadGameOver");
                }             
                else
                {
                    List<string> text = new List<string>();
                    if (PlayerSingleton.instance.poisoned)
                    {
                        player.transform.GetChild(4).GetComponent<ParticleSystem>().Play();
                        player.transform.GetChild(4).GetComponent<ParticleSystem>().loop = true;

                        int rndDamage = Random.Range(1, 4);
                        PlayerSingleton.instance.playerHealth -= rndDamage;
                        playerPoisonedTurns++;

                        text.Add("You are poisoned, you took " + rndDamage + " damage.");

                        if (playerPoisonedTurns == maxPoisonedTurns)
                        {
                            player.transform.GetChild(4).GetComponent<ParticleSystem>().loop = false;
                            text[0] += ("\n\nYou are no longer poisoned!");
                            PlayerSingleton.instance.poisoned = false;
                            playerPoisonedTurns = 0;
                        }

                        textBox.PrintMessage(text, menuManager, "MainSelect");
                    }

                    else if (PlayerSingleton.instance.confused)
                    {
                        player.transform.GetChild(5).GetComponent<ParticleSystem>().Play();
                        player.transform.GetChild(5).GetComponent<ParticleSystem>().loop = true;

                        player.GetComponent<PlayerCombatLogic>().noteSpeedMultiplicator = 2;
                        playerConfusedTurns++;
                        if (playerConfusedTurns == maxConfusedTurns)
                        {
                            player.transform.GetChild(5).GetComponent<ParticleSystem>().loop = false;
                            PlayerSingleton.instance.confused = false;
                            playerConfusedTurns = 0;
                            player.GetComponent<PlayerCombatLogic>().noteSpeedMultiplicator = 1;
                            text.Add("You are no longer confused!");
                        }
                        else
                        {
                            text.Add("You are confused!");
                        }
                        textBox.PrintMessage(text, menuManager, "MainSelect");
                    }

                    else
                    {
                        // Resets menu to the main combat menu
                        menuManager.GetComponent<MenuManagment>().MainSelect();
                    }


                    enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isStunned = false;
                }
                break;
            //Check if currentTurn is equal to Enemy
            case "Enemy":
                //Check if the enemy health is less or equal to zero
                if (enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyHp <= 0)
                {
                    //If it is a textbox will show, a death animation for the enamy will be played and a return to the world button will be showed 
                    enemyHolder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Dead");
                    List<string> text = new List<string>();
                    text.Add("You defeated the enemy, you got " + enemyClass.enemyExp + " exp!");
                    textBox.PrintMessage(text, menuManager, "ReturnToWorldSelect");
                    enemyIsDead = true;

                    // Marks this enemy for deactivation when the player returns to the last scene
                    OverworldEnemySingleton.instance.shouldDestroy[OverworldEnemySingleton.instance.currentEnemyIndex] = true;

                    enemyClass.source.PlayOneShot(enemyClass.death, 1f);
                }
                else if (enemyClass.isStunned == false)
                {
                    //Setting the player menu to false
                    playerMenu.SetActive(false);
                    //Calling the function EnemyAttack
                    EnemyAttack();                    
                }
                else
                {
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
        enemyClass.AttackPattern();
    }


    public void ShowVictoryMenu()
    {
        
        returnToWorldButton.SetActive(true);
    }


    public void ReturnToTheWorld()
    {
        OverworldEnemySingleton.instance.backFromCombat = true;
        SceneManager.LoadScene("Forest_Scene_1");
    }


    public void LoadGameOver()
    {
        Debug.Log("adsadasdadasd");
        SceneManager.LoadScene("DeathScene");
    }
}
