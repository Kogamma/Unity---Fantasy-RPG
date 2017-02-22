using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeIceBox : MonoBehaviour {

    GameObject combatHandler;
    float scaleValue = 1f;

	// Use this for initialization
	void Start () {
        combatHandler = GameObject.FindGameObjectWithTag("CombatHandler");
	}
	
	// Update is called once per frame
	void Update () {
        //print(transform.localScale);

        Debug.Log(transform.localScale.y);

        transform.localScale = Vector3.Lerp(transform.localScale, combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyIceBlockMaxSize, 0.1f);
        
        if(combatHandler.GetComponent<CombatScript>().currentTurn == "Enemy" || combatHandler.GetComponent<CombatScript>().enemyIsDead == true)
        {
            print("current turn = Enemy");
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.2f);
            if (scaleValue >= transform.localScale.y)
            {
                combatHandler.GetComponent<CombatScript>().enemyHolder.transform.GetChild(0).GetComponent<Animator>().speed = 1;
                print("Destroy");
                Destroy(gameObject);
            }
                
        }

	}
}
