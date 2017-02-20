using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour {

    public int enemyHp;
    public int enemyExp;
    protected int enemyArmorClass;
    protected int enemyDmg;
    protected bool isStunned;

    protected Animator anim;
    CombatScript combatScript;
    GameObject combatHandler;
    CombatTextBoxHandler combatTextbox;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        combatHandler = GameObject.FindGameObjectWithTag("CombatHandler");
        combatScript = combatHandler.GetComponent<CombatScript>();
        combatTextbox = combatScript.textBox;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void NormalAttack()
    {
        PlayerSingleton.instance.playerHealth -= enemyDmg;
        anim.SetTrigger("Attack");
    }

    void AttackIsDone()
    {
        string[] text = new string[1]{""};
        text[0] = "You took " + enemyDmg + " damage!";
        combatScript.ChangeViewPort(CombatScript.cameraState.PLAYER);
        combatScript.player.GetComponent<Animator>().SetTrigger("Attacked");
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
