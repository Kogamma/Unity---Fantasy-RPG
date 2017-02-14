using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private CharacterController playerCon;
    public float moveSpeed;


	// Use this for initialization
	void Start ()
    {
        playerCon = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 inputVec = Vector3.zero;

        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.z = Input.GetAxis("Vertical");

        if(inputVec.x != 0 && inputVec.z != 0)
        {
            //transform.rotation = new Vector3(0,,0)
        }

        playerCon.SimpleMove(inputVec * moveSpeed * Time.deltaTime);


    }
}
