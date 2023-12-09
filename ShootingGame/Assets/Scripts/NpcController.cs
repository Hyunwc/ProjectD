using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Transform targetDestination; // 이동할 목적지의 Transform

    private NavMeshAgent navMeshAgent;
    private Animator npcAni;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        npcAni = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveToDestination();
        }
    }

    void MoveToDestination()
    {
        if (targetDestination != null)
        {
            navMeshAgent.SetDestination(targetDestination.position);
            npcAni.SetBool("Run", true);
        }
    }
}
