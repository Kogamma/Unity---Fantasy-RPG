using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollScript : EnemyClass
{
    int attacked = Animator.StringToHash("Attacked");

    public int hp = 5;
    public int dmg = 4;
    public int armorClass = 1;
    public int exp = 30;
    public float freeze = 1f;
    [SerializeField] private int confusionChance = 20;
    private bool canConfuse = false;

    // Use this for initialization
    void Awake()
    {
        //Setting the exp, hp, dmg and armorclass for the enemy
        enemyExp = exp;
        enemyHp = hp;
        enemyDmg = dmg;
        enemyArmorClass = armorClass;
        chanceToGetFreeze = freeze;
    }

    void Update()
    {
        if (enemyHp <= hp * 0.75f)
            canConfuse = true;
    }

    public override void AttackPattern()
    {
        if (canConfuse && Random.Range(0, 100 / confusionChance) == 0)
            base.ConfusionAttack();
        else
            base.NormalAttack();
    }
}
