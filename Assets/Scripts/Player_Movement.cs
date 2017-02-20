using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // The characters player controller and animator components
    private CharacterController controller;
    private Animator _anim;

    // How fast the player will be moving
    [Range(1f, 10f)]
    public float moveSpeed = 0f;
    // Modifier to adjust animation speed
    const float speedMod = 0.4375f;

    // Determines how fast the character falls
    [Range(1f, 10f)]
    public float weight = 1;
    // Gravity constant
    const float gravity = 9.8f;

    // Vector for movement input
    Vector3 inputVec = Vector3.zero;
    // For applyng gravity to the character
    private float m_ySpeed = 0;

    void Start ()
    {
        controller = GetComponent<CharacterController>();

        _anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (PlayerSingleton.instance.canMove)
        {
            // Creates a vector for input            
            inputVec.x = Input.GetAxis("Horizontal");
            inputVec.z = Input.GetAxis("Vertical");

            // Calculates the magnitude of the two input values we created
            float inputMagnitude = Vector3.Magnitude(new Vector3(inputVec.x, 0, inputVec.z));

            _anim.SetFloat("WalkModifier", speedMod * moveSpeed);

            // Sets the parameter for the player's walk animation
            _anim.SetFloat("Velocity", inputMagnitude);

            // If the player is moving we change their rotation
            if (inputMagnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.z));

                if (inputVec.x != 0 && inputVec.z != 0)
                {
                    inputVec.x = inputVec.x * inputMagnitude / 2;
                    inputVec.z = inputVec.z * inputMagnitude / 2;
                }
            }

            // If the character is already grounded we set their y speed to zero
            if (controller.isGrounded)
                m_ySpeed = 0;

            m_ySpeed = -gravity * weight * Time.deltaTime;
            inputVec.y = m_ySpeed;

            // Moves the character with the CharacterController component
            controller.Move(inputVec * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Sets the parameter for the player's walk animation to make it stop whilst not moving
            _anim.SetFloat("Velocity", 0);
        }
    }

    /*
    // Code for playing a footstep sound on an animation event, currently unused
    void FootStep()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(grassStep, 0.4f);

    }
    */
}
