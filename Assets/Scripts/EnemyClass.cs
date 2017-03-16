using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float chanceToGetFreeze;
    public float chanceToGetConfused;
    public float chanceToGetOnFire;
    public int enemyHp;
    protected int maxHP;
    public int enemyExp;
    public int enemyArmorClass;
    public int enemyDmgMax;
    public int enemyDmgMin;
    public int enemyHeal;
    protected int enemyDmgDealt;
    public string displayName = "enemy";
    public bool isStunned;
    public bool isFrozen = false;
    public bool isConfused = false;
    public bool onFire = false;

    protected Animator anim;
    CombatScript combatScript;
    GameObject combatHandler;
    CombatTextBoxHandler combatTextbox;
    public Vector3 enemyIceBlockMaxSize;

    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip damage;
    public AudioClip death;
    public AudioClip healingSound;

    public GameObject healingParticle;
    public GameObject fireParticle;

    void Start ()
    {
        maxHP = enemyHp;

        //Getting animator for the enemy
        anim = GetComponent<Animator>();
        //combatHanlder will be equal to an object with the tag "CombatHandler" in the scene
        combatHandler = GameObject.FindGameObjectWithTag("CombatHandler");
        //combatScript will be equal to combatHandlers script CombatScript
        combatScript = combatHandler.GetComponent<CombatScript>();
        //combatTextbox will be equal to the combatsScript object textBox
        combatTextbox = combatScript.textBox;


    }
    //A normal attack for the enemy
    public void NormalAttack()
    {
        int enemyRngDmg = Random.Range(enemyDmgMin, enemyDmgMax + 1);
        enemyDmgDealt = enemyRngDmg;

        if (isConfused)
            enemyDmgDealt = Mathf.RoundToInt(enemyDmgDealt * 0.67f);

        //PLayerHealth minus the enemydmg
        PlayerSingleton.instance.playerHealth -= enemyDmgDealt;
        //Playing the attack animation
        anim.SetTrigger("Attack");

        AudioHelper.PlaySound(attack1);
    }


    public void PoisonAttack()
    {
        int enemyRngDmg = Random.Range(enemyDmgMin, enemyDmgMax + 1);
        enemyDmgDealt = enemyRngDmg / 2;

        if (isConfused)
            enemyDmgDealt = Mathf.RoundToInt(enemyDmgDealt * 0.67f);

        //PLayerHealth minus the enemydmg
        PlayerSingleton.instance.playerHealth -= enemyDmgDealt;

        // Poisons the player
        PlayerSingleton.instance.poisoned = true;

        //Playing the attack animation
        anim.SetTrigger("Attack2");

        AudioHelper.PlaySound(attack2);
    }


    public void ConfusionAttack()
    {
        int enemyRngDmg = Random.Range(enemyDmgMin, enemyDmgMax + 1);
        // Calculates what damage to deal
        enemyDmgDealt = Mathf.RoundToInt(enemyRngDmg * 0.75f);

        if (isConfused)
            enemyDmgDealt = Mathf.RoundToInt(enemyDmgDealt * 0.67f);

        // Decreases player health with damage dealt
        PlayerSingleton.instance.playerHealth -= enemyDmgDealt;

        // The player is now confused
        PlayerSingleton.instance.confused = true;

        // Plays second attack animation
        anim.SetTrigger("Attack2");

        if (displayName == "Troll")
        {
            AudioHelper.PlayDelayed(attack2, 1f);
        }
        else
            AudioHelper.PlaySound(attack2);

    }

    public void FireAttack()
    {
        int enemyRngDmg = Random.Range(enemyDmgMin, enemyDmgMax + 1);
        // Calculates what damage to deal
        enemyDmgDealt = Mathf.RoundToInt(enemyRngDmg * 0.75f);

        if (isConfused)
            enemyDmgDealt = Mathf.RoundToInt(enemyDmgDealt * 0.67f);

        // Decreases player health with damage dealt
        PlayerSingleton.instance.playerHealth -= enemyDmgDealt;

        // Plays second attack animation
        anim.SetTrigger("Attack2");

        fireParticle.GetComponent<ParticleSystem>().Play();

        PlayerSingleton.instance.onFire = true;

        AudioHelper.PlaySound(attack2);
    }

    public void Healing()
    {
        enemyHp += enemyHeal;

        Instantiate(healingParticle, transform.position, Quaternion.identity);
        List<string> text = new List<string>();
        text.Add("The Dragon Boar healed for " + enemyHeal);

        AudioHelper.PlaySound(healingSound);

        combatTextbox.PrintMessage(text, gameObject, "ChangeViewToMain");
    }

    //This function will play when the attack animation is done
    void AttackIsDone()
    {
        //Setting the textBox and write out how much damage the player takes
        List<string> text = new List<string>();
        text.Add("You took " + enemyDmgDealt + " damage!");
        //Setting the camerastate to player
        combatScript.ChangeViewPort(CombatScript.cameraState.PLAYER);
        //Playing the players attacked animation
        combatScript.player.GetComponent<Animator>().SetTrigger("Attacked");
        //Check when the textbox is done and change the camerstate to main
        combatTextbox.PrintMessage(text, gameObject, "ChangeViewToMain");
        if (tag == "DragonBoar")
        {
            if (fireParticle.GetComponent<ParticleSystem>().isPlaying)
                fireParticle.GetComponent<ParticleSystem>().Stop();
        }
    }

    public virtual void AttackPattern()
    {
        
    }

    void ChangeViewToMain()
    {
        combatScript.ChangeViewPort(CombatScript.cameraState.MAIN);
        combatScript.UpdateTurn("Player");
        if (tag == "DragonBoar")
        {
            if (healingParticle.GetComponent<ParticleSystem>().isPlaying)
                Destroy(healingParticle);
        }
    }
}
