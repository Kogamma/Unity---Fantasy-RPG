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
    float dmg;
    int meeleAttack = Animator.StringToHash("MeeleAttack");
    public GameObject playerComabat;


	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void MeeleAttack()
    {
        notes = 4;
        noteSpeed = 10;
        interval = 0.5f;
        critchance = 10;

        //playerComabat.GetComponent<ComboSystem>().ActivateCombo(notes, noteSpeed, interval, critchance);
        //mainCamera.enabled = false;
        //playerCamera.enabled = true;

        anim.SetTrigger(meeleAttack);

        dmg = PlayerSingleton.instance.playerDmg + (1.4f * (float) PlayerSingleton.instance.playerStr);

        Mathf.FloorToInt(dmg);
        Debug.Log("hey");

        PlayerSingleton.instance.currentDmg = (int)dmg;



 PlayerSingleton.instance.playerAttacked = true;
            playerComabat.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.ENEMY);
        
    }
    public void IceAttack()
    {

    } 
       
        
}
