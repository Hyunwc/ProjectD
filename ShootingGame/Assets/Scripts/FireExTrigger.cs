using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireExTrigger : MonoBehaviour
{
    private Enemy enemy;
    private Boss boss;
    public FireManager fireManager;
    //public EnemeManager enemeManager;
    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        boss = FindObjectOfType<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Fire"))
        {
            Debug.Log("불과 충돌");
            fireManager.fireObjects.Remove(other.gameObject);
            Destroy(other.gameObject, 0.5f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.Damaged(1f);
            }
        }
        else if(other.CompareTag("Boss"))
        {
            boss.Damaged(1f);
        }
    }

}
