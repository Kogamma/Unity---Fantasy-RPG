using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatLogic : MonoBehaviour {

    //GameObject enemyHolder;
    public GameObject combatHandler;                            //Using this to set the diffrent camera states
    public float hitAccuracy;                                   //Using to see the accuarcy for the player
    public bool comboIsDone = false;                            //Using to check if the combo is done
    public string whichAttack;
    Animator anim;                                              //Using the get the animator player
    int notes;                                                  //Using to set how many notes
    float noteSpeed;                                            //Using to set the notespeed for the attacks
    public float noteSpeedMultiplicator = 1;
    float interval;                                             //Using to set the interval for the notes on the diffrent attacks
    float dmg;                                                  //Using to set the damge for the player

    private CombatScript combatScript;

    //int meeleAttack = Animator.StringToHash("MeeleAttack");     //Using to get the attack animation
    [SerializeField] GameObject comboSystem;                    //Using to call the combo system and activate it
    [SerializeField] GameObject textBox;
    [SerializeField] GameObject iceParticle;
    [SerializeField] GameObject iceBlock;

    [SerializeField] AudioClip attack_sound;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        combatScript = combatHandler.GetComponent<CombatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackIsDone();        
    }
    //The normal attack for the player
    public void MeeleAttack()
    {
        notes = 4;          //The combo will have 4 notes
        noteSpeed = 0.3f * noteSpeedMultiplicator;    //The speed for the notes will be 0.3
        interval = 0.5f;    //They will come 0.5 sec after each other

        comboSystem.SetActive(true);                                                                        //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + ((1.4f * (float)PlayerSingleton.instance.playerStr));    //Setting the damage for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval);      //Calling the funtion to actiavte the combo system 
    }

    //The ice attack for the player
    public void IceAttack()
    {
        notes = 6;          //The combo will have 6 notes
        noteSpeed = 0.6f * noteSpeedMultiplicator;    //The speed for the notes will be 0.6
        interval = 0.5f;    //They will come 0.5sec after each other 

        comboSystem.SetActive(true);                                                                        //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + ((0.7f * (float)PlayerSingleton.instance.playerInt));    //Setting the damage for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval);      //Calling the funtion to actiavte the combo system

        PlayerSingleton.instance.playerMana -= 5;
    }

    public void ConfusionAttack()
    {
        notes = 6;          //The combo will have 6 notes
        noteSpeed = 0.4f * noteSpeedMultiplicator;    //The speed for the notes will be 0.4
        interval = 0.3f;    //They will come 0.3sec after each other

        comboSystem.SetActive(true);                                                                        //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + ((0.9f * (float)PlayerSingleton.instance.playerInt));    //Setting the damage for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval);      //Calling the funtion to actiavte the combo system

        PlayerSingleton.instance.playerMana -= 10;
    }

    //The fire attack for the player
    public void FireAttack()
    {
        notes = 8;      //The combo will have 8 notes
        noteSpeed = 0.45f * noteSpeedMultiplicator;     //The speed for the notes will be 0.45
        interval = 0.3f;

        comboSystem.SetActive(true);    //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + (1.7f * (float)PlayerSingleton.instance.playerInt); //Setting the damge for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval); //Calling the funtion to actiavte the combo system

        PlayerSingleton.instance.playerMana -= 15;
    }

   
    public void Flee()
    {
        notes = 6; //The combo will have 6 notes
        noteSpeed = 0.4f * noteSpeedMultiplicator; //The speed for the the notes will be 0.4f
        interval = 0.5f; //They will come 0.5sec after each other

        comboSystem.SetActive(true);
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval);  //Calling the funtion to actiavte the combo system
    }

    public void AttackIsDone()
    {
        //Check if the combo is done
        if (comboIsDone)
        {
            if (whichAttack != "Flee")
            {
                anim.SetTrigger("MeeleAttack");   //Playing the attack animation
                AudioHelper.PlaySound(attack_sound);
            }
            dmg *= hitAccuracy;             //Multiplay the damge with the combo hit accuracy

            float rng = Random.Range(0f, 1f);

            //PlayerSingleton.instance.currentDmg = (int)dmg; //Setting the currentDmg to dmg

            switch(whichAttack)
            {
                //Check if the player used IceAttack
                case "IceAttack":

                    Instantiate(iceParticle, combatScript.enemyHolder.transform.GetChild(0).transform.position, Quaternion.identity);

                    //Check if the rng is bigger then chance to freeze the enemy
                    if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().chanceToGetFreeze >=rng)
                    {
                        StartCoroutine(WaitForParticle());

                        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isStunned = true;
                        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isFrozen = true;
                        //Instantiate(iceBlock, combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).transform.position, Quaternion.identity);
                    }
                    break;

                //Check if the player used ConfusionAttack
                case "ConfusionAttack":
                    //Check if the rng is bigger then chance to confuse the enemy
                    if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().chanceToGetConfused >= rng)
                    {
                        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isConfused = true;
                        combatScript.enemyHolder.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
                        combatScript.enemyHolder.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().loop = true;
                    }
                    break;

                //Check if the player used FireAttack
                case "FireAttack":
                    //Check if the enemy is not frozen so you can set the enemy on fire
                    if (!combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isFrozen)
                    {
                        //Check if the rng is bigger then chance to set fire on the enemy
                        if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().chanceToGetOnFire >= rng)
                        {
                            combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().onFire = true;
                            combatScript.enemyHolder.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().Play();
                            combatScript.enemyHolder.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().loop = true;
                        }
                    }
                    break;
                //Check if the player used Flee
                case "Flee":
                    List<string> text2 = new List<string>();
                    //Check if the players accuracy is bigger equal or bigger then 0.7
                    if (hitAccuracy >= 0.7)
                    {
                        OverworldEnemySingleton.instance.fled = true;
                        OverworldEnemySingleton.instance.backFromCombat = true;
                        text2.Add("You succeeded to flee!");
                        //Lets the player flee to the scene again
                        textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(text2,gameObject,"FleeToScene");
                    }
                    //Check if the failed to flee
                     if(hitAccuracy < 0.7)
                    {
                        text2.Add("You failed to flee!");
                        textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(text2, gameObject, "ChangeViewToMain");
                    }
                    break;
                default:
                    break;
                   
            }
            //Check if the player didnt use flee to write out stuff for the other attacks
            if (whichAttack != "Flee")
            {
                List<string> text = new List<string>();
                text.Add("You did " + (int)dmg + " damage to the enemy!");
                if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isStunned)
                    text.Add("The Enemy froze! It has to skip a turn!");
                else if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isConfused && whichAttack == "ConfusionAttack")
                    text.Add("The Enemy is confused! Its attacks are now weaker!");
                else if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().onFire && whichAttack == "FireAttack")
                    text.Add("The Enemy is on fire!");

                textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(text, gameObject, "ChangeViewToMain");
            }

            comboIsDone = false;                            //Setting combo is done to false;
        }
    }

    //This function will be called when the attack animation is done
    void OnAttackFinished()
    {
        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyHp -= (int)dmg;
        dmg = 0;

        combatScript.ChangeViewPort(CombatScript.cameraState.ENEMY); //Chaning the camerstate to Enemy!
        //Check if the enemy is not forzen so the attacked animation will be played 
        if (!combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isFrozen)
        {
            combatScript.enemyHolder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attacked");
            AudioHelper.PlaySound(combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().damage, 1f);
        }
    }
    void ChangeViewToMain()
    {
        combatHandler.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.MAIN);
        StartCoroutine(TurnDelay());

    }

    void FleeToScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerSingleton.instance.currentScene);
    }

    IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(1);
        combatHandler.GetComponent<CombatScript>().UpdateTurn("Enemy");
    }
    IEnumerator WaitForParticle()
    {
        yield return new WaitForSeconds(1f);
        //if(!combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isStunned)
            Instantiate(iceBlock, combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).transform.position, Quaternion.identity);
        combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).GetComponent<Animator>().speed = 0;
    }
}
