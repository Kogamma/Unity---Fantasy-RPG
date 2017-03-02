using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;           //  Needs for the NavMeshAgent

public class EnemyAiMovement : MonoBehaviour {

    private NavMeshAgent agent;
    private Transform ultimateGoal;
    public Transform[]goals;
    private int currentGoal = 0;
    public bool isFrozen;
    float timer = 0;
    float cd = 3f;

    private Transform playerTransform;

    private bool isChasing = false;

    private float m_originSpeed;

    private void Start()
    {
        ultimateGoal = goals[0];
        agent = GetComponent<NavMeshAgent>();   // Find the component "NavMeshAgent"

        m_originSpeed = agent.speed;
    }




    private void Update()
    {
        if (!isFrozen)
        {
            if (isChasing == true)
            {
                agent.SetDestination(playerTransform.position);    //  Set Destination to Player
            }
            else
            {
                agent.SetDestination(ultimateGoal.position);
            }

            if (Vector3.Distance(transform.position, ultimateGoal.position) < 1.0f) // Goes to path to the next
            {
                if (ultimateGoal.transform == goals[currentGoal])
                {
                    if (currentGoal == goals.Length - 1)
                        currentGoal = 0;
                    else
                        currentGoal++;
                    ultimateGoal = goals[currentGoal];

                }
            }
        }
        else
        {
            GetComponentInChildren<BoxCollider>().enabled = false;
            timer += Time.deltaTime;
            if (timer >= cd)
            {
                timer = 0;
                GetComponentInChildren<BoxCollider>().enabled = true;
                isFrozen = false;
            }
        }

        if (!PlayerSingleton.instance.canMove)
            agent.speed = 0;
        else
            agent.speed = m_originSpeed;
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
