using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour {

    Animator anim;
    AnimationClip deathAnim;
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
		if(PlayerSingleton.instance.playerAttacked)
        {
            anim.SetTrigger(attacked);
            slimeHp -= PlayerSingleton.instance.currentDmg;
            PlayerSingleton.instance.playerAttacked = false;
        }

        if(slimeHp <= 0)
        {
            anim.SetBool("isDead", true);
            Destroy(gameObject);
        }          
	}
}
