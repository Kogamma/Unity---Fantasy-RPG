using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : EnemyClass {

    int attacked = Animator.StringToHash("Attacked");

    public int hp = 5;
    public int dmg = 4;
    public int armorClass = 1;
    public int exp = 30;

    // Use this for initialization
    void Awake ()
    {
        //Setting the exp, hp, dmg and armorclass for the enemy
        enemyExp = exp;
        enemyHp = hp;
        enemyDmg = dmg;
        enemyArmorClass = armorClass;
    }
	
	void Update()
    {

    }

    public override void AttackPattern()
    {
        base.NormalAttack();
    }

    void EndAttacked()
    {
        //combatSystem.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.MAIN);
    }
    void DeadEvent()
    {
        //combatSystem.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.MAIN);
    }

}
