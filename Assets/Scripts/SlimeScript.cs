﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : EnemyClass {

    public GameObject combatSystem;
    int attacked = Animator.StringToHash("Attacked");

    public int hp = 5;
    public int dmg = 4;
    public int armorClass = 1;
    public int exp = 30;

    // Use this for initialization
    void Awake ()
    {
        enemyExp = exp;
        enemyHp = hp;
        enemyDmg = dmg;
        enemyArmorClass = armorClass;
    }
	
	void Update()
    {
        Debug.Log("Enemy hp = " + enemyHp);
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
