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
        Damaged, //�ǰ�
        Special //�ʻ��
    }

    //���¸� ��Ƶ� ����, �⺻ ���·� ����
    public BossState bossState = BossState.Idle;
    public float hp = 500; //����ü��
    public Slider hpBar; //���� ü�¹�
    public float speed = 3.0f;
    //public GameObject bossBullet;
    private Rigidbody bossRb;
    //private float bulletSpeed = 10f;

    //�Ѿ� �������� ��Ƶ� ����
    public GameObject bulletPref;
    public float firePower = 10f;
    Transform player;
    NavMeshAgent agent; //NavMeshAgent ������Ʈ
    float distance; //�÷��̾���� �Ÿ�
    public GameObject ExitCube;
    
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
        }
        else
        {
            //eState = EnemyState.Dead;
            Destroy(gameObject);
            ExitCube.SetActive(false);
         
        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("AttackPlayer", 2f, 2f);
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
        if (distance <= 30)
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
        if (distance > 30)
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

    void AttackPlayer()
    {

        // �÷��̾ �ٶ󺸵��� ȸ����ŵ�ϴ�.
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // �ҷ� ���� �� �߻��մϴ�.
        GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
        ////�÷��̾��� ��ġ���� 1 �տ�, ���Ͱ��ƴ� transform�� ������ ȸ���Ҷ��� �ùٸ� ��ġ�� �����ǰ� �ϱ� ����
        ////���� �� bullet ������ �Ҵ�
        //GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);

        ////�Ѿ� �������� ������ ���ư��� �������� �� �߻�
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
    }
}
