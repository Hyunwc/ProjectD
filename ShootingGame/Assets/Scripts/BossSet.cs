using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSet : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boss.SetActive(true);
            Destroy(gameObject);
        }
    }

}
   