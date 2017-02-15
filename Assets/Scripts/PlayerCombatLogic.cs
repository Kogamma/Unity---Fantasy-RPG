using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatLogic : MonoBehaviour {

    public bool playerAttack;
    Animator anim;
    int notes;
    int noteSpeed;
    float interval;
    int critchance;
    public float hitAccuracy;
    public bool comboIsDone = false;
    float dmg;
    int meeleAttack = Animator.StringToHash("MeeleAttack");
    public GameObject playerComabat;
    [SerializeField] GameObject comboSystem;
    float timer = 0;


	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (comboIsDone)
        {
            anim.SetTrigger(meeleAttack);

            dmg = PlayerSingleton.instance.playerDmg + ((1.4f * (float)PlayerSingleton.instance.playerStr) * hitAccuracy);

            Mathf.FloorToInt(dmg);
            Debug.Log("hey");

            PlayerSingleton.instance.currentDmg = (int)dmg;

            PlayerSingleton.instance.playerAttacked = true;
        }
    }

    public void MeeleAttack()
    {
        notes = 4;
        noteSpeed = 150;
        interval = 0.5f;
        critchance = 10;

        comboSystem.SetActive(true);
        comboSystem.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);


        
    }
    public void IceAttack()
    {
        notes = 6;
        noteSpeed = 200;
        interval = 0.5f;
        critchance = 10;

        anim.SetTrigger(meeleAttack);
    } 

    void OnAttackFinished()
    {
        playerComabat.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.ENEMY);
    }

}
