﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // The characters player controller and animator components
    private CharacterController controller;
    private Animator _anim;

    private AudioSource source;
    public AudioClip grassStep;

    // How fast the player will be moving
    [Range(1f, 10f)]
    public float moveSpeed = 0f;

	void Start ()
    {
        controller = GetComponent<CharacterController>();

        _anim = GetComponent<Animator>();

        source = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        // Creates a vector for input
        Vector3 inputVec = Vector3.zero;
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.z = Input.GetAxis("Vertical");dasd

        // Calculates the magnitude of the two input values we created
        float inputMagnitude = Vector3.Magnitude(inputVec);

        // Sets the parameter for the player's walk animation
        _anim.SetFloat("Velocity", inputMagnitude);

        // If the player is moving we change their rotation
        if (inputMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(inputVec);

            if(inputVec.x > 0 && inputVec.z > 0)
            {
                inputVec.x *= inputMagnitude * 0.5f;
                inputVec.z *= inputMagnitude * 0.5f;
            }
        }
        
        // Moves the character with the CharacterController component
        controller.Move(inputVec * moveSpeed * Time.deltaTime);
    }

    void FootStep()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(grassStep, 0.4f);

    }
}
