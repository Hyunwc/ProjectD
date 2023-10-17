using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//using static Enemy; // < ?

public class Boss : MonoBehaviour
{
    //������ ���� �� �ִ� ���� 
    public enum BossState
    {
        Idle, //�⺻
        Walk, //�̵�
        Attack, //����
        Damaged //�ǰ�
    }

    //���¸� ��Ƶ� ����, �⺻ ���·� ����
    public BossState bossState = BossState.Idle;
    public float hp = 2000; //����ü��
    public Slider hpBar; //���� ü�¹�
    public float speed = 3.0f;

    private Rigidbody bossRb;

    Transform player;
    NavMeshAgent agent; //NavMeshAgent ������Ʈ
    float distance; //�÷��̾���� �Ÿ�
    void Damaged(float damage)
    {
        //���� ������ŭ ü�� ����
        hp -= damage;
        //������ ü���� ü�¹ٿ� ǥ��
        hpBar.value = hp;

        //agent.isStopped = true; //�̵� �ߴ�
        //agent.ResetPath(); //��� �ʱ�ȭ
        //ü���� �����ִٸ� �ǰݻ��·�
        if (hp > 0)
        {
            if (bossState == BossState.Idle)
            {
                bossState = BossState.Walk;
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            //else
            //{
            //    bossState = BossState.Damaged;
            //}
        }
        else
        {
            //eState = EnemyState.Dead;
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        print(distance);

        switch (bossState)
        {
            case BossState.Idle: Idle(); break;
            case BossState.Walk: Walk(); break;
            case BossState.Attack: Attack(); break;
        }
    }
    void Idle()
    {
        //�÷��̾���� �Ÿ��� 8 ���϶��
        if (distance <= 8)
        {
            //�ȱ� ���·�
            bossState = BossState.Walk;
            agent.isStopped = false; //�̵� ����
            agent.SetDestination(player.position); // ������ ����
        }
    }

    void Walk()
    {
        //�÷��̾�� 8�̻� ��������
        if (distance > 8)
        {
            //�⺻���·� ����
            bossState = BossState.Idle;
            agent.isStopped = true; //�̵� �ߴ�
            agent.ResetPath(); //��� �ʱ�ȭ
        }
        else if (distance <= 0.5) //0.5���ϸ�
        {
            bossState = BossState.Attack; //���ݻ��·�
            agent.isStopped = true; //�̵� �ߴ�
            agent.ResetPath(); //��� �ʱ�ȭ
        }
        //�ٸ� ���·� ��ȯ���� ���� ����
        else
        {
            //�÷��̾��� ��ġ�� �������� ����
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        //�Ÿ��� 2���� ũ�� �̵����·�
        if (distance > 0.5)
        {
            bossState = BossState.Walk;
            agent.isStopped = false; //�̵�����
        }
    }
}
