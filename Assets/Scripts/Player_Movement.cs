using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // The characters player controller and animator components
    private CharacterController controller;
    private Animator _anim;

    // How fast the player will be moving
    [Range(1f, 1000f)]
    public float moveSpeed;

	void Start ()
    {
        controller = GetComponent<CharacterController>();

        _anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        // Creates a vector for input
        Vector3 inputVec = Vector3.zero;
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.z = Input.GetAxis("Vertical");

        // Calculates the magnitude of the two input values we created
        float inputMagnitude = Vector3.Magnitude(inputVec);

        // Sets the parameter for the player's walk animation
        _anim.SetFloat("Velocity", inputMagnitude);

        // If the player is moving we change their rotation
        if (inputMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(inputVec);
        }

        // If the player is moving in both directions, the speed will be lowered 
        if(inputMagnitude > 1)
        {
            inputVec.x = inputMagnitude / 2;
            inputVec.x = inputMagnitude / 2;
        }

        // Moves the character with the CharacterController component
        controller.SimpleMove(inputVec * moveSpeed * Time.deltaTime);
    }
}
