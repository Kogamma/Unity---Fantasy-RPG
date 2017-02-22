using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : EnemyClass
{
    int attacked = Animator.StringToHash("Attacked");

    public int hp = 5;
    public int dmg = 4;
    public int armorClass = 1;
    public int exp = 30;
    public float freeze = 1f;
    [SerializeField] private int poisonChance;
    private bool canPoison = false;

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
        if (enemyHp <= hp / 2)
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
