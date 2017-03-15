using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCredits : MonoBehaviour
{
    public GameObject moveObject;

    public bool credBool = false;

    Vector3 reset;

    public float speed = 100;

    void Start()
    {
        reset = moveObject.transform.position;
    }

    void Update()
    {
        if(credBool)
        {
            moveObject.transform.Translate(Vector3.up * speed * Time.deltaTime);

            if(moveObject.transform.position.y >= 2300)
            {
                moveObject.transform.position = reset;
            }
        }
        else
        {
            moveObject.transform.position = reset;
        }

    }
}
