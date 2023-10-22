using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag("Enemy"))
        //{
        //    other.gameObject.SendMessage("Damaged", 20);

        //    Destroy(gameObject);
        //}

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Damaged", 10);

            Destroy(gameObject);
        }
    }
}
