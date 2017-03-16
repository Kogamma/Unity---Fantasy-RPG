using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossScript : EnemyClass
{
    [SerializeField]
    int fireAttack = 100;
    bool canFireAttack = false;
    bool haveHealed = false; 

	
	// Update is called once per frame
	void Update ()
    {
        if (enemyHp <= maxHP * 0.75f)
            canFireAttack = true;
	}

    public override void AttackPattern()
    {
        if (enemyHp < 15 && !haveHealed)
        {
            base.Healing();
            haveHealed = true;
        }
        else if (canFireAttack && Random.Range(0, 100 / fireAttack) == 0)
            base.FireAttack();
        else
            base.NormalAttack();
    }
}
