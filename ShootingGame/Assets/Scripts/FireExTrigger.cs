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
            Fire.fireDestroy++;
            Debug.Log("불과 충돌");
            fireManager.fireObjects.Remove(other.gameObject);
            Destroy(other.gameObject, 0.5f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            TutorialEnemy t_enemy = other.GetComponent<TutorialEnemy>();
            if (enemy != null)
            {
                enemy.Damaged(1f);
            }
            else if (t_enemy != null)
            {
                t_enemy.Damaged(1f);
            }
        }
        
        else if(other.CompareTag("Boss"))
        {
            boss.Damaged(1f);
        }
    }

}
