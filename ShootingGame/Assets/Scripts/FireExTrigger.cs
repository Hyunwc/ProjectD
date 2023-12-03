using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Destroy(other);
        }
    }

}
