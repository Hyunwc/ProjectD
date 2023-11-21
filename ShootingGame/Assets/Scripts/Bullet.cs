using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bullet : MonoBehaviour
{
    //�������ΰ�hyun

    //public int damage;
    //public bool isMelee;
    //public Transform target;
    //NavMeshAgent nav;

    //private void Awake()
    //{
    //    nav = GetComponent<NavMeshAgent>();

    //}

    //private void Update()
    //{
    //    nav.SetDestination(target.position);
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Ground")
    //    {
    //        Destroy(gameObject, 3);
    //    }

    //}
    //void OnTriggerEnter(Collider other)
    //{
    //    if(!isMelee && other.gameObject.tag == "Wall")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public ParticleSystem particlePrefab; //���� ��ƼŬ

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Damaged", 10);

            particlePrefab.Play();
            Destroy(gameObject);



        }
        else if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {

            particlePrefab.Play();
            Destroy(gameObject);

        }
    }


}
