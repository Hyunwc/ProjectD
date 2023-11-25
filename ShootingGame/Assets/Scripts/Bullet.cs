using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bullet : MonoBehaviour
{
    //개발중인거hyun

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

    public ParticleSystem particlePrefab; //폭발 파티클

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Damaged", 10);

            //particlePrefab.Play();
            //Destroy(gameObject);
            StartCoroutine(DestroyAfterParticlePlay());

        }
        else if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {

            //particlePrefab.Play();
            //Destroy(gameObject);
            StartCoroutine(DestroyAfterParticlePlay());
        }
    }

    IEnumerator DestroyAfterParticlePlay()
    {
        particlePrefab.Play();

        // 파티클 재생이 끝날 때까지 기다립니다.
        yield return new WaitForSeconds(particlePrefab.main.duration);

        // 파티클 재생이 끝난 후에 게임 오브젝트를 파괴합니다.
        Destroy(gameObject);
    }


}
