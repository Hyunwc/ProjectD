using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public Transform target;
    NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        nav.SetDestination(target.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject, 3);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if(!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    //public GameObject particlePrefab; //폭발 파티클
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        other.gameObject.SendMessage("Damaged", 10);

    //        // 파티클 프리팹을 인스턴스화합니다.
    //        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

    //        // 파티클이 재생된 후에 파괴합니다.
    //        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);

    //        Destroy(gameObject);

    //    }
    //    else if (other.gameObject.CompareTag("Ground"))
    //    {
    //        // 파티클 프리팹을 인스턴스화합니다.
    //        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

    //        // 파티클이 재생된 후에 파괴합니다.
    //        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
    //        Destroy(gameObject);
    //    }
    //}
}
