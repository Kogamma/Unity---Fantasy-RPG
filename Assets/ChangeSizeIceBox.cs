using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeIceBox : MonoBehaviour {

    GameObject enemyHolder;

	// Use this for initialization
	void Start () {
        enemyHolder = GameObject.FindGameObjectWithTag("EnemyHolder");

	}
	
	// Update is called once per frame
	void Update () {
        print(transform.localScale);

        transform.localScale = Vector3.Lerp(transform.localScale, enemyHolder.transform.GetChild(0).GetComponent<EnemyClass>().enemyIceBlockMaxSize, 0.1f);

	}
}
