using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour {

    public GameObject playerCharacter;

	void Start ()
    {
        playerCharacter.transform.position = PlayerSingleton.instance.overWorldTransform.position;
        playerCharacter.transform.rotation = PlayerSingleton.instance.overWorldTransform.rotation;
        playerCharacter.transform.localScale = PlayerSingleton.instance.overWorldTransform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
