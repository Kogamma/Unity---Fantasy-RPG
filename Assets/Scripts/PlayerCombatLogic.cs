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
    int critchance;                                             //Using to set the critchance for the diffrent attack
    float dmg;                                                  //Using to set the damge for the player

    private CombatScript combatScript;

    //int meeleAttack = Animator.StringToHash("MeeleAttack");     //Using to get the attack animation
    [SerializeField] GameObject comboSystem;                    //Using to call the combo system and activate it
    [SerializeField] GameObject textBox;
    [SerializeField] UnityEngine.UI.Image healthBar;
    [SerializeField] GameObject iceParticle;
    [SerializeField] GameObject iceBlock;

    private AudioSource source;
    [SerializeField] AudioClip attack_sound;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        combatScript = combatHandler.GetComponent<CombatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackIsDone();
        healthBar.fillAmount = (float)((float)PlayerSingleton.instance.playerHealth / (float)PlayerSingleton.instance.playerMaxHealth);
        
    }
    //The normal attack for the player
    public void MeeleAttack()
    {
        notes = 4;          //The combo will have 4 notes
        noteSpeed = 0.3f * noteSpeedMultiplicator;    //The speed for the notes will be 150
        interval = 0.5f;    //They will come 0.5 sec after each other
        critchance = 10;    // The player will have 10% critchance


        comboSystem.SetActive(true);                                                                        //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + ((1.4f * (float)PlayerSingleton.instance.playerStr));    //Setting the damage for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);      //Calling the funtion Activatecombo, and placing the parameters in  
    }

    //The ice attack for the player
    public void IceAttack()
    {
        notes = 6;          //The combo will have 6 notes
        noteSpeed = 0.6f * noteSpeedMultiplicator;    //The speed for the notes will be 200
        interval = 0.5f;    //They will come 0.5sec after each other
        critchance = 10;    //The player will have 10% critchance

        comboSystem.SetActive(true);                                                                        //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + ((0.7f * (float)PlayerSingleton.instance.playerInt));    //Setting the damage for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);      //Calling the funtion Activatecombo, and placing the parameters in

        PlayerSingleton.instance.playerMana -= 5;
    }

    public void ConfusionAttack()
    {
        notes = 6;          //The combo will have 6 notes
        noteSpeed = 0.4f * noteSpeedMultiplicator;    //The speed for the notes will be 200
        interval = 0.3f;    //They will come 0.3sec after each other
        critchance = 15;    //The player will have 15% critchance

        comboSystem.SetActive(true);                                                                        //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + ((0.9f * (float)PlayerSingleton.instance.playerInt));    //Setting the damage for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);      //Calling the funtion Activatecombo, and placing the parameters in

        PlayerSingleton.instance.playerMana -= 10;
    }

    public void FireAttack()
    {
        notes = 8;
        noteSpeed = 0.45f * noteSpeedMultiplicator;
        interval = 0.3f;
        critchance = 20;

        comboSystem.SetActive(true);
        dmg = PlayerSingleton.instance.playerDmg + (1.7f * (float)PlayerSingleton.instance.playerInt);
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);
    }

    public void Flee()
    {
        notes = 6;
        noteSpeed = 0.4f * noteSpeedMultiplicator;
        interval = 0.5f;
        critchance = 10;

        comboSystem.SetActive(true);
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);
    }

    public void AttackIsDone()
    {
        //Check if the combo is done
        if (comboIsDone)
        {
            if (whichAttack != "Flee")
            {
                anim.SetTrigger("MeeleAttack");   //Playing the attack animation
                source.PlayOneShot(attack_sound, 1f);
            }
            dmg *= hitAccuracy;             //Multiplay the damge with the combo hit accuracy

            float rng = Random.Range(0f, 1f);

            //PlayerSingleton.instance.currentDmg = (int)dmg; //Setting the currentDmg to dmg

            switch(whichAttack)
            {
                case "IceAttack":
                    Instantiate(iceParticle, combatScript.enemyHolder.transform.GetChild(0).transform.position, Quaternion.identity);

                    if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().chanceToGetFreeze >=rng)
                    {
                        StartCoroutine(WaitForParticle());

                        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isStunned = true;
                        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isFrozen = true;
                        //Instantiate(iceBlock, combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).transform.position, Quaternion.identity);
                    }
                    break;

                case "ConfusionAttack":
                    combatScript.enemyHolder.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
                    combatScript.enemyHolder.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().loop = true;

                    if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().chanceToGetConfused >= rng)
                        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isConfused = true;
                    break;

                case "FireAttack":
                    if (!combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isFrozen)
                    {
                        if (combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().chanceToGetOnFire >= rng)
                            combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().onFire = true;
                        
                    }
                    break;
                case "Flee":
                    List<string> text2 = new List<string>();
                    if (hitAccuracy >= 0.7)
                    {
                        text2.Add("You succeed to flee!");
                        OverworldEnemySingleton.instance.fled = true;
                        OverworldEnemySingleton.instance.backFromCombat = true;
                        textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(text2,gameObject,"FleeToScene");
                    }
                     if(hitAccuracy < 0.7)
                    {
                        text2.Add("You failed to flee!");
                        textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(text2, gameObject, "ChangeViewToMain");
                    }
                    break;
                default:
                    break;
                   
            }
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

    void OnAttackFinished()
    {
        combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyHp -= (int)dmg;
        dmg = 0;

        combatScript.ChangeViewPort(CombatScript.cameraState.ENEMY); //Chaning the camerstate to Enemy!
        if (!combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().isFrozen)
        {
            combatScript.enemyHolder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attacked");
            combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().source.PlayOneShot(combatScript.enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().damage, 1f);
        }
    }
    void ChangeViewToMain()
    {
        combatHandler.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.MAIN);
        StartCoroutine(TurnDelay());

    }

    void FleeToScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Forest_Scene_1");
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
