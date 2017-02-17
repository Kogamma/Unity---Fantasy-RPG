using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {

	public void RotateSign()
    {
        if (name == "Player Character")
        {
            transform.localScale *= 2;
            StartCoroutine(ScaleBack());
        }
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4);
    }

    public IEnumerator ScaleBack()
    {
        yield return new WaitForSeconds(5f);
        transform.localScale /= 2;
    }
}
