using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject interactIcon;
    private Camera camera;
    [SerializeField] private AudioClip pop;
    private bool doOnce = true;



    void Start ()
    {
        camera = Camera.main; 
    }


    void Update ()
    {
        //DrawLine(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward / 2, Color.red);

        RaycastHit hit;

        // If a raycast hits something infront of the player
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.forward, out hit, 1f))
        {
            // If the hit object is a interactable object
            if (hit.transform.gameObject.tag == "Interactable")
            {
                
                interactIcon.SetActive(true);
                interactIcon.transform.LookAt(camera.transform.position);

                if (doOnce)
                {
                    AudioHelper.PlaySound(pop);
                    doOnce = false;
                }

                // If the player presses the button for interaction
                if (Input.GetButtonDown("Interact") && PlayerSingleton.instance.canMove)
                {
                    // Activates its interaction event
                    hit.transform.gameObject.SendMessage("OnInteract");
                }
            }
        }
        else
        {
            interactIcon.SetActive(false);
            doOnce = true;
        }
	}
}
