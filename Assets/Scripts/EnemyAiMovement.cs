using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;           //  Needs for the NavMeshAgent

public class EnemyAiMovement : MonoBehaviour {

    private NavMeshAgent agent;
    private Transform ultimateGoal;
    public Transform[]goals;
    private int currentGoal = 0;

    private Transform playerTransform;

    private bool isChasing = false;

    private void Start()
    {
        ultimateGoal = goals[0];
        agent = GetComponent<NavMeshAgent>();   // Find the component "NavMeshAgent"
    }




    private void Update()
    {
        if (isChasing == true)
        {
            agent.SetDestination(playerTransform.position);    //  Set Destination to Player
        }
        else
        {
            agent.SetDestination(ultimateGoal.position);
        }

        if(Vector3.Distance(transform.position, ultimateGoal.position) < 1.0f ) // Goes to path to the next
        {
            if(ultimateGoal.transform == goals[currentGoal])
            {
                if (currentGoal == goals.Length - 1)
                    currentGoal = 0;
                else
                    currentGoal++;
                ultimateGoal = goals[currentGoal];

            }
        }
    }


    private void OnTriggerEnter(Collider obj)    // When player enter the enemy collider
    {
        if(obj.tag == "Player") //   if the target is tagged "player"
        {
            playerTransform = obj.transform;     //  Reference player's transform
            isChasing = true;
        }

    }

    private void OnTriggerExit(Collider player)     // When player exit the enemy collider
    {
        if(player.tag == "Player") // if the target is tagged "player"
        {
            isChasing = false;
        }
    }


   
}
