using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TargetMisslie : MonoBehaviour
{
    public ParticleSystem particlePrefab;
    private Transform target;
    
    NavMeshAgent nav;

    public int hitCount = 0;
    private void Awake()
    {
        target = FindObjectOfType<PlayerMove>().transform;
        nav = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        nav.SetDestination(target.position);

        if (hitCount == 2)
            Destroy(gameObject);
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
        //Debug.Log("파티클은 호출됨 친구야");
        particlePrefab.Play();

        // 파티클 재생이 끝날 때까지 기다립니다.
        yield return new WaitForSeconds(particlePrefab.main.duration);

        // 파티클 재생이 끝난 후에 게임 오브젝트를 파괴합니다.
        Destroy(gameObject);
    }
}
