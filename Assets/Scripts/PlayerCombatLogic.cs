using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatLogic : MonoBehaviour {

    public GameObject combatHandler;                            //Using this to set the diffrent camera states
    public float hitAccuracy;                                   //Using to see the accuarcy for the player
    public bool comboIsDone = false;                            //Using to check if the combo is done
    Animator anim;                                              //Using the get the animator player
    int notes;                                                  //Using to set how many notes
    float noteSpeed;                                              //Using to set the notespeed for the attacks
    float interval;                                             //Using to set the interval for the notes on the diffrent attacks
    int critchance;                                             //Using to set the critchance for the diffrent attack
    float dmg;                                                  //Using to set the damge for the player
    //int meeleAttack = Animator.StringToHash("MeeleAttack");     //Using to get the attack animation
    [SerializeField] GameObject comboSystem;                    //Using to call the combo system and activate it
    [SerializeField] GameObject textBox;
    [SerializeField] UnityEngine.UI.Image healthBar;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
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
        noteSpeed = 0.3f;    //The speed for the notes will be 150
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
        noteSpeed = 0.6f;    //The speed for the notes will be 200
        interval = 0.5f;    //They will come 0.5sec after each other
        critchance = 10;    //The player will have 10% critchance

        comboSystem.SetActive(true);                                                                        //Setting the combo system ui to true
        dmg = PlayerSingleton.instance.playerDmg + ((2.5f * (float)PlayerSingleton.instance.playerInt));    //Setting the damage for the player
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);      //Calling the funtion Activatecombo, and placing the parameters in

        PlayerSingleton.instance.playerMana -= 2;
    }

    public void AttackIsDone()
    {
        //Check if the combo is done
        if (comboIsDone)
        {
            anim.SetTrigger("MeeleAttack");   //Playing the attack animation
            dmg *= hitAccuracy;             //Multiplay the damge with the combo hit accuracy

            PlayerSingleton.instance.currentDmg = (int)dmg; //Setting the currentDmg to dmg

            string[] text = new string[1] {""};
            text[0] = "You did " + PlayerSingleton.instance.currentDmg + " damage to the enemy!";
            textBox.GetComponent<CombatTextBoxHandler>().PrintMessage(text, gameObject, "ChangeViewToMain");

            comboIsDone = false;                            //Setting combo is done to false;
        }
    }

    void OnAttackFinished()
    {
        combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyHp -= (int)dmg;
        combatHandler.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.ENEMY); //Chaning the camerstate to Enemy!
        combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attacked");
    }
    void ChangeViewToMain()
    {
        combatHandler.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.MAIN);
        StartCoroutine(TurnDelay());

    }
    IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(1);
        combatHandler.GetComponent<CombatScript>().UpdateTurn("Enemy");
    }

}
