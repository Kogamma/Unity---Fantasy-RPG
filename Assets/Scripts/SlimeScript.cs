using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour {

    Animator anim;
    public GameObject combatSystem;
    bool isStunned;
    int slimeHp = 20;
    bool stunnded = false;
    int attacked = Animator.StringToHash("Attacked");

    AnimationEvent evt = new AnimationEvent();

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(slimeHp);
		if(combatSystem.GetComponent<CombatScript>().currentState == 1)
        {
            anim.SetTrigger(attacked);
            slimeHp -= PlayerSingleton.instance.currentDmg;
            combatSystem.GetComponent<CombatScript>().currentState = 2;

        }

        if(slimeHp <= 0)
        {
            anim.SetBool("isDead", true);
            combatSystem.GetComponent<CombatScript>().currentState = 2;
        }        
	}
    void EndAttacked()
    {
        combatSystem.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.MAIN);
    }
    void DeadEvent()
    {
        combatSystem.GetComponent<CombatScript>().ChangeViewPort(CombatScript.cameraState.MAIN);
    }

}
