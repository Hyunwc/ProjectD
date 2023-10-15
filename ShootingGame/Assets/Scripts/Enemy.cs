using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //적이 가질 수 있는 상태 
    public enum EnemyState
    {
        Idle, //기본
        Walk, //이동
        Attack, //공격
        Damaged, //피격
        Dead // 죽음
    }

    //상태를 담아둘 변수, 기본 상태로 시작
    public EnemyState eState = EnemyState.Idle;
    public float hp = 100; //적체력
    public Slider hpBar; //적 체력바
    public float speed = 3.0f;

    private Rigidbody enemyRb;
   

    Transform player;
    NavMeshAgent agent; //NavMeshAgent 컴포넌트
    float distance; //플레이어와의 거리
   // private GameObject player;

    void Damaged(float damage)
    {
        //공격 받은만큼 체력 감소
        hp -= damage;
        //감소한 체력을 체력바에 표시
        hpBar.value = hp;

        //agent.isStopped = true; //이동 중단
        //agent.ResetPath(); //경로 초기화
        //체력이 남아있다면 피격상태로
        if (hp > 0)
        {
            if (eState == EnemyState.Idle)
            {
                eState = EnemyState.Walk;
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                eState = EnemyState.Damaged;
            }
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        //적과 플레이어 사이의 거리 계산
        distance = Vector3.Distance(transform.position, player.position);

        //적과 플레이어 사이 거리 출력
        //print(distance);
        //기본,이동,공격 상태일때 할일 나누기
        switch(eState)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Walk: Walk(); break;
            case EnemyState.Attack: Attack(); break;

        }
        //Vector3 direction = player.position - transform.position;
        //direction.Normalize(); //정규화

        //transform.position += direction * speed * Time.deltaTime;
   
    }

    void Idle()
    {
        //플레이어와의 거리가 8 이하라면
        if(distance <= 8)
        {
            //걷기 상태로
            eState = EnemyState.Walk;
            agent.isStopped = false; //이동 시작
            agent.SetDestination(player.position); // 목적지 설정
        }
    }
    void Walk()
    {
        //8보다 크다면
        if(distance > 8)
        {
            eState = EnemyState.Idle;
            agent.isStopped = true; //이동 중단
            agent.ResetPath(); //경로 초기화
        }
        else if(distance <= 0.5)
        {
            eState = EnemyState.Attack; //공격상태로
            agent.isStopped = true; //이동 중단
            agent.ResetPath(); //경로 초기화
        }
        //다른 상태로 전환하지 않을 때는
        else
        {
            //플레이어의 위치를 목적지로 설정
            agent.SetDestination(player.position);
        }
    }
    void Attack()
    {
        //거리가 2보다 크면 이동상태로
        if (distance > 0.5)
        {
            eState = EnemyState.Walk;
            agent.isStopped = false; //이동시작
        }
    }
}
