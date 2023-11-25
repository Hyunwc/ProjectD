using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float speed = 10f; // 미사일의 속도
    private Rigidbody rb; // 미사일의 Rigidbody 컴포넌트

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옵니다.
    }
    private void Start()
    {
        //particlePrefab.Stop();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse); // 미사일의 앞쪽 방향으로 힘을 가합니다.
    }

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
        Debug.Log("파티클은 호출됨 친구야");
        particlePrefab.Play();

        // 파티클 재생이 끝날 때까지 기다립니다.
        yield return new WaitForSeconds(particlePrefab.main.duration);

        // 파티클 재생이 끝난 후에 게임 오브젝트를 파괴합니다.
        Destroy(gameObject);
    }


}
