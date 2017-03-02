using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCombatScript : EnemyClass
{
    [SerializeField] private int poisonChance = 20;
    private bool canPoison = false;

    
    void Update ()
    {
        if (enemyHp <= maxHP * 0.5f)
            canPoison = true;
	}


    public override void AttackPattern()
    {
        if (canPoison && Random.Range(0, 100 / poisonChance) == 0)
            base.PoisonAttack();
        else
            base.NormalAttack();
    }
}
