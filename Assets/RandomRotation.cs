using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {

	public void RotateSign()
    {
        if(this.name == "RotatingTree")
        {
            int x = Random.Range(-30, 60);
            int y = Random.Range(-30, 60);
            int z = Random.Range(-30, 60);
            Debug.Log("TRÄD");
            transform.rotation = Quaternion.Euler(x, y, z);
        }
        else
            transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
