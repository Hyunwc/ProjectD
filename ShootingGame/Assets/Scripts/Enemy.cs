using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //���� ���� �� �ִ� ���� 
    public enum EnemyState
    {
        Idle, //�⺻
        Walk, //�̵�
        Attack, //����
        Damaged, //�ǰ�
        Dead // ����
    }
    
    //���¸� ��Ƶ� ����, �⺻ ���·� ����
    public EnemyState eState = EnemyState.Idle;
    public float hp = 100; //��ü��
    public Slider hpBar; //�� ü�¹�
    public float speed = 3.0f;

    private Rigidbody enemyRb;
    private PlayerHp playerHp;

    Transform player;
    NavMeshAgent agent; //NavMeshAgent ������Ʈ
    float distance; //�÷��̾���� �Ÿ�

    
   // private GameObject player;

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
            if (eState == EnemyState.Idle)
            {
                eState = EnemyState.Walk;
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            //else
            //{
            //    eState = EnemyState.Damaged;
            //}
        }
        //hp�� 0�̸� ������Ʈ �ı�
        else{
            //eState = EnemyState.Dead;
            Destroy(gameObject);
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //enemyRb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMove>().transform;
        agent = GetComponent<NavMeshAgent>();
        playerHp = FindObjectOfType<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        //���� �÷��̾� ������ �Ÿ� ���
        distance = Vector3.Distance(transform.position, player.position);

        //���� �÷��̾� ���� �Ÿ� ���
        //print(distance);
        //�⺻,�̵�,���� �����϶� ���� ������
        switch(eState)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Walk: Walk(); break;
            case EnemyState.Attack: Attack(); break;

        }
        //Vector3 direction = player.position - transform.position;
        //direction.Normalize(); //����ȭ

        //transform.position += direction * speed * Time.deltaTime;
   
    }

    void Idle()
    {
        //�÷��̾���� �Ÿ��� 8 ���϶��
        if(distance <= 8)
        {
            Debug.Log("����");
            //�ȱ� ���·�
            eState = EnemyState.Walk;
            agent.isStopped = false; //�̵� ����
            agent.SetDestination(player.position); // ������ ����
        }
    }
    void Walk()
    {
        //8���� ũ�ٸ�
        if(distance > 8)
        {
            Debug.Log("����");
            eState = EnemyState.Idle;
            agent.isStopped = true; //�̵� �ߴ�
            agent.ResetPath(); //��� �ʱ�ȭ
        }
        else if(distance <= 0.5)
        {
            Debug.Log("����");
            eState = EnemyState.Attack; //���ݻ��·�
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
            eState = EnemyState.Walk;
            agent.isStopped = false; //�̵�����
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
           
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            
            float force = 4f;
            Vector3 direction = collision.transform.position - transform.position;
            direction.Normalize();

            playerRb.AddForce(direction * force, ForceMode.Impulse);
            playerHp.Damaged(5f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water")) // �浹�� ������Ʈ�� Fire �±׸� ������ �ִ��� Ȯ���մϴ�.
        {
            Damaged(1f);
        }
    }
}
