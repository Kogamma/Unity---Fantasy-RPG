using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Player_Camera : MonoBehaviour
{
    public GameObject player;       //Public variable to store a reference to the player game object

    public Vector3 offset;         //Private variable to store the offset distance between the player and camera


    // LateUpdate is called after Update each frame
    void Update()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;

        transform.LookAt(player.transform.position);
    }
}
