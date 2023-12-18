using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Transform targetDestination; // 이동할 목적지의 Transform

    private NavMeshAgent navMeshAgent;
    private Animator npcAni;

    private C2Quest quest;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        npcAni = GetComponent<Animator>();
        quest = FindObjectOfType<C2Quest>();
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
           
        //    MoveToDestination();
        //}
        if (collision.gameObject.CompareTag("Exit"))
        {
            quest.npcCount--;
            gameObject.SetActive(false);
        }
    }

     public void MoveToDestination()
    {
    if (targetDestination != null)
        {
            navMeshAgent.SetDestination(targetDestination.position);
            npcAni.SetBool("isRun", true);
        }
    }
}
