using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    public GameObject levelUpMenu;
    public GameObject levelUpParticle;
    // The first button chosen in the level up menu when leveling up
    public GameObject firstLevelUpButton;

    public int currentState;
    public CombatTextBoxHandler textBox;
    public GameObject enemyHolder;
    public GameObject playerHealth;
    public bool enemyIsDead = false;

    private int playerPoisonedTurns = 0;
    private int maxPoisonedTurns = 3;

    private int playerFireTurns = 0;
    private int enemyFireTurns = 0;
    private int maxFireTurns = 3;

    private int playerConfusedTurns;
    private int enemyConfusedTurns;
    private int maxConfusedTurns = 3;

    private EnemyClass enemyClass;

    public Image blackScreen;
    public float waitTimeBlackScreen;

	// Use this for initialization
	void Start ()
    {
        //Getting the players animator
        player.GetComponent<Animator>();
        //Setting the player camera to false           
        _playerCamera.enabled = false;
        //Setting the enemy camera to false           
        _EnemyCamera.enabled = false;
        //Setting the currentstate to main       
        currentState = (int)cameraState.MAIN;

        _EnemyCamera.GetComponent<LookAtEnemy>().target = enemyHolder.transform.GetChild(0).transform.position;

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
                    textBox.PrintMessage(text, menuManager, "RestartGameSelect");
                }             
                else
                {
                    List<string> text = new List<string>();
                    //Check if the player is poisoned
                    if (PlayerSingleton.instance.poisoned)
                    {
                        //Start the poisoned particlesystem
                        player.transform.GetChild(4).GetComponent<ParticleSystem>().Play();
                        player.transform.GetChild(4).GetComponent<ParticleSystem>().loop = true;

                        //Random poison damage between 1 and 4 to players health.
                        int rndDamage = Random.Range(1, 4);
                        PlayerSingleton.instance.playerHealth -= rndDamage;
                        playerPoisonedTurns++;

                        text.Add("You are poisoned, you took " + rndDamage + " damage.");

                        //Check if the player had posion for 3 turns
                        if (playerPoisonedTurns == maxPoisonedTurns)
                        {
                            //Stopping the poison particle effect
                            player.transform.GetChild(4).GetComponent<ParticleSystem>().loop = false;
                            text[0] += ("\n\nYou are no longer poisoned!");
                            //Setting poisoned to false
                            PlayerSingleton.instance.poisoned = false;
                            //resetting the playerPoisonedTurns to zero
                            playerPoisonedTurns = 0;
                        }

                        textBox.PrintMessage(text, menuManager, "MainSelect");
                    }

                    else if(PlayerSingleton.instance.onFire)
                    {
                        player.transform.GetChild(6).GetComponent<ParticleSystem>().Play();
                        player.transform.GetChild(6).GetComponent<ParticleSystem>().loop = true;

                        //Setting how much fire damage the player will take this turn.
                        int rndDmg = Random.Range(2, 5);
                        PlayerSingleton.instance.playerHealth -= rndDmg;
                        playerFireTurns++;

                        text.Add("You are on fire, you took " + rndDmg + " damage!");

                        //Check if the player have been on fire for 3 turns
                        if(playerFireTurns == maxFireTurns)
                        {
                            player.transform.GetChild(6).GetComponent<ParticleSystem>().loop = false;
                            text[0] += ("\n\nYou are no longer on fire!");

                            PlayerSingleton.instance.onFire = false;

                            playerFireTurns = 0;
                        }

                        textBox.PrintMessage(text, menuManager, "MainSelect");
                    }

                    //Check if the player is confused
                    else if (PlayerSingleton.instance.confused)
                    {
                        //Start the confused particlesystem
                        player.transform.GetChild(5).GetComponent<ParticleSystem>().Play();
                        player.transform.GetChild(5).GetComponent<ParticleSystem>().loop = true;

                        //Speed up the notes for the combo system
                        player.GetComponent<PlayerCombatLogic>().noteSpeedMultiplicator = 2;
                        playerConfusedTurns++;
                        //Check if the player had confused for 3 turns
                        if (playerConfusedTurns == maxConfusedTurns)
                        {
                            //Stop the particle effect
                            player.transform.GetChild(5).GetComponent<ParticleSystem>().loop = false;
                            PlayerSingleton.instance.confused = false;
                            playerConfusedTurns = 0;
                            //Set the notespeed to the normal speed
                            player.GetComponent<PlayerCombatLogic>().noteSpeedMultiplicator = 1;
                            text.Add("You are no longer confused!");
                        }
                        //Check if the player is still confused
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
                List<string> text2 = new List<string>();
                //Check if the enemy health is less or equal to zero
                if (enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyHp <= 0)
                {
                    //If it is a textbox will show, a death animation for the enamy will be played and a return to the world button will be showed 
                    enemyHolder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Dead");
                    text2.Add("You defeated the enemy, you got " + enemyClass.enemyExp + " exp!");
                    textBox.PrintMessage(text2, this.gameObject, "EndOfCombat");
                    enemyIsDead = true;

                    // Marks this enemy for deactivation when the player returns to the last scene
                    OverworldEnemySingleton.instance.shouldDestroy[OverworldEnemySingleton.instance.currentEnemyIndex] = true;

                    AudioHelper.PlaySound(enemyClass.death);

                    PlayerSingleton.instance.playerExp += enemyClass.enemyExp;
                }
                //Check if the is not frozen, so the other effects can start. 
                else if (enemyClass.isStunned == false)
                {
                    //Check if the enemy is confused
                    if (enemyClass.isConfused)
                    {
                        enemyConfusedTurns++;
                        //Check if the enemy have been confused in 3 turns 
                        if (enemyConfusedTurns == maxConfusedTurns)
                        {
                            //Stopping the particle system.
                            enemyHolder.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().loop = false;
                            //Reset the confused turns
                            enemyConfusedTurns = 0;
                            //Setting confused to false
                            enemyClass.isConfused = false;
                            text2.Add("The Enemy is no longer confused!");
                        }
                    }
                    //Check if the enemy is on fire
                    if (enemyClass.onFire)
                    {
                        enemyFireTurns++;
                        //Random fire damage  between 1 and 4 that multiplie with the players int
                        int rndDamage = Random.Range(1, 4) * (PlayerSingleton.instance.playerInt / 5);
                        enemyClass.enemyHp -= rndDamage;

                        text2.Add("The enemy took " + rndDamage + " fire damage!");
                        //Check if the enemy have been on fire for 3 turns
                        if (enemyFireTurns == maxFireTurns)
                        {
                            //Stop the particle system for the fire
                            enemyHolder.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().loop = false;
                            enemyFireTurns = 0;
                            enemyClass.onFire = false;
                            text2.Add("The enemy is no longer on fire!");
                        }
                    }

                    if (text2.Count > 0)
                        textBox.PrintMessage(text2, gameObject, "EnemyAttack");

                    else
                        EnemyAttack();
                    //Setting the player menu to false
                    playerMenu.SetActive(false);             
                }
                else
                {
                    UpdateTurn("Player");
                }

                break;
        }
    }
    

    public void RemovePoison()
    {
        player.transform.GetChild(4).GetComponent<ParticleSystem>().loop = false;
        PlayerSingleton.instance.poisoned = false;
        playerPoisonedTurns = 0;
    }
    

    public void RemoveConfusion()
    {
        player.transform.GetChild(5).GetComponent<ParticleSystem>().loop = false;
        PlayerSingleton.instance.confused = false;
        playerConfusedTurns = 0;
    }


    public void RemoveBurn()
    {
        player.transform.GetChild(6).GetComponent<ParticleSystem>().loop = false;
        PlayerSingleton.instance.onFire = false;
        playerFireTurns = 0;
    }


    public void ActivateClairvoyance()
    {
        player.GetComponent<PlayerCombatLogic>().notesNrMult = 0.5f;
    }


    public void ActivateGoldenHit()
    {
        player.GetComponent<PlayerCombatLogic>().goldenHitActivated = true;
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


    public void ReturnToTheWorld()
    {
        RemovePoison();
        RemoveConfusion();
        menuManager.GetComponent<MenuManagment>().returnButton.SetActive(false);
        StartCoroutine(FillBlackScreen());
    }


    public void EndOfCombat ()
    {
        RemovePoison();
        RemoveConfusion();
        player.transform.GetChild(5).GetComponent<ParticleSystem>().loop = false;
        PlayerSingleton.instance.confused = false;

        if (levelUpMenu.GetComponent<Level_Stats_Script>().PlayerLevel())
        {
            Instantiate(levelUpParticle, player.transform.position, player.transform.rotation);
            List<string> text = new List<string>();
            text.Add("You leveled up to level " + PlayerSingleton.instance.level + "!");
            textBox.PrintMessage(text, this.gameObject, "LevelUp");
        }
        else
            ReturnToTheWorld();
    }


    void LevelUp()
    {
        levelUpMenu.transform.GetChild(0).gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(firstLevelUpButton);

        menuManager.GetComponent<MenuManagment>().ReturnToWorldSelect();
    }


    IEnumerator FillBlackScreen()
    {
        blackScreen.gameObject.SetActive(true);

        while (blackScreen.fillAmount < 1)
        {
            blackScreen.fillAmount += 1.0f / waitTimeBlackScreen * Time.deltaTime;

            yield return null;
        }

        MusicHelper.Stop();

        OverworldEnemySingleton.instance.backFromCombat = true;
        SceneManager.LoadScene(PlayerSingleton.instance.currentScene);
    }
}
