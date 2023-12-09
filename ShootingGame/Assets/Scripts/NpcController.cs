using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Transform targetDestination; // �̵��� �������� Transform

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
