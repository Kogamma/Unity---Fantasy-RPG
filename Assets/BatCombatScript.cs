using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCombatScript : EnemyClass {


    [SerializeField] private int confusionChance = 20;
    private bool canConfuse = false;

    // Use this for initialization
    void Start ()
    {
        if (enemyHp <= maxHP * 0.5f)
            canConfuse = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void AttackPattern()
    {
        if (canConfuse && Random.Range(0, 100 / confusionChance) == 0)
            base.ConfusionAttack();
        else
            base.NormalAttack();
    }
}
