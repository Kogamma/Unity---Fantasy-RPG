using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float chanceToGetFreeze;
    public int enemyHp;
    public int enemyExp;
    protected int enemyArmorClass;
    protected int enemyDmg;
    protected int enemyDmgDealt;
    public string displayName = "enemy";
    public bool isStunned;

    protected Animator anim;
    CombatScript combatScript;
    GameObject combatHandler;
    CombatTextBoxHandler combatTextbox;
    public Material oldMat;
    public Color frozenColor;
    public Renderer rend;
    public Material frozenMat;

    void Start ()
    {
        //Getting animator for the enemy
        anim = GetComponent<Animator>();
        //combatHanlder will be equal to an object with the tag "CombatHandler" in the scene
        combatHandler = GameObject.FindGameObjectWithTag("CombatHandler");
        //combatScript will be equal to combatHandlers script CombatScript
        combatScript = combatHandler.GetComponent<CombatScript>();
        //combatTextbox will be equal to the combatsScript object textBox
        combatTextbox = combatScript.textBox;

        frozenMat = new Material(Shader.Find("Standard"));
        rend = (gameObject.transform.GetChild(0).GetComponent<Renderer>());
        frozenMat.CopyPropertiesFromMaterial(oldMat);

        frozenMat.SetColor("_Color", frozenColor);

        rend.material = oldMat;
    }
    //A normal attack for the enemy
    public void NormalAttack()
    {
        enemyDmgDealt = enemyDmg;

        //PLayerHealth minus the enemydmg
        PlayerSingleton.instance.playerHealth -= enemyDmg;
        //Playing the attack animation
        anim.SetTrigger("Attack");
    }


    public void PoisonAttack()
    {
        enemyDmgDealt = enemyDmg / 2;

        //PLayerHealth minus the enemydmg
        PlayerSingleton.instance.playerHealth -= enemyDmgDealt;

        // Poisons the player
        PlayerSingleton.instance.poison = true;

        //Playing the attack animation
        anim.SetTrigger("Attack2");
    }


    //This function will play when the attack animation is done
    void AttackIsDone()
    {
        //Setting the textBox and write out how much damage the player takes
        string[] text = new string[1]{""};
        text[0] = "You took " + enemyDmgDealt + " damage!";
        //Setting the camerastate to player
        combatScript.ChangeViewPort(CombatScript.cameraState.PLAYER);
        //Playing the players attacked animation
        combatScript.player.GetComponent<Animator>().SetTrigger("Attacked");
        //Check when the textbox is done and change the camerstate to main
        combatTextbox.PrintMessage(text, gameObject, "ChangeViewToMain");
        
    }

    public virtual void AttackPattern()
    {
        
    }

    void ChangeViewToMain()
    {
        combatScript.ChangeViewPort(CombatScript.cameraState.MAIN);
        combatScript.UpdateTurn("Player");
    }


}
