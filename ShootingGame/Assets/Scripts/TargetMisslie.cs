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
        //Debug.Log("��ƼŬ�� ȣ��� ģ����");
        particlePrefab.Play();

        // ��ƼŬ ����� ���� ������ ��ٸ��ϴ�.
        yield return new WaitForSeconds(particlePrefab.main.duration);

        // ��ƼŬ ����� ���� �Ŀ� ���� ������Ʈ�� �ı��մϴ�.
        Destroy(gameObject);
    }
}
