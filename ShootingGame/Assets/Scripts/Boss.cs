using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Diagnostics;
//using static Enemy; // < ?

public class Boss : MonoBehaviour
{
    //보스가 가질 수 있는 상태 
    public enum BossState
    {
        Idle, //기본
        Walk, //이동
        Attack, //공격
        Damaged, //피격
        Died,
        Special //필살기
    }

    //상태를 담아둘 변수, 기본 상태로 시작
    public BossState bossState = BossState.Idle;
    public float hp = 500f; //보스체력
    public Slider hpBar; //보스 체력바
    public float speed = 3.0f;
    //public GameObject bossBullet;
    private Rigidbody bossRb;
    //private float bulletSpeed = 10f;

    //총알 프리팹을 담아둘 변수
    public GameObject[] bulletPref;
    public float firePower = 10f;
    Transform player;
    NavMeshAgent agent; //NavMeshAgent 컴포넌트
    float distance; //플레이어와의 거리
    //탈출구 파괴
    public GameObject ExitCube;

    private PlayerHp playerHp;
    public bool canMove = false;

    bool isAttacking = false;
    private Animator bossAni;
    public GameObject bulletSpawnPoint;
    public void Damaged(float damage)
    {
        if(canMove)
        {
            //공격 받은만큼 체력 감소
            hp -= damage;
            //감소한 체력을 체력바에 표시
            hpBar.value = hp;
            //체력이 남아있다면 피격상태로
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
                bossState = BossState.Died;
                Died();

            }
        }
        
    }


    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        agent = GetComponent<NavMeshAgent>();
        playerHp = FindObjectOfType<PlayerHp>();
        bossAni = GetComponent<Animator>();
        //hpBar.maxValue = hp; // 슬라이더의 maxValue를 hp와 동일하게 설정
        //hpBar.value = 0; // 초기 체력을 0으로 설정
        //hp = 0;
        //InvokeRepeating("AttackPlayer", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        //print(distance);
        if(canMove)
        {
            switch (bossState)
            {
                case BossState.Idle: Idle(); break;
                case BossState.Walk: Walk(); break;
                case BossState.Attack: Attack(); break;
                    //case BossState.Died: Died(); break;
            }
        }
        
    }
    void Idle()
    {
        //플레이어와의 거리가 8 이하라면
        if (distance <= 30)
        {
            //걷기 상태로
            bossState = BossState.Walk;
            agent.isStopped = false; //이동 시작
            agent.SetDestination(player.position); // 목적지 설정
            bossAni.SetBool("Walk Forward", true);
        }
    }

    void Walk()
    {
        if (!isAttacking) // 보스가 공격 중이 아니라면
        {
            StartCoroutine(AttackPeriodically()); // 코루틴을 시작하여 일정한 간격으로 공격
        }
        //플레이어와 8이상 떨어지면
        if (distance > 30)
        {
            //기본상태로 변경
            bossState = BossState.Idle;
            agent.isStopped = true; //이동 중단
            bossAni.SetBool("Walk Forward", false);
            agent.ResetPath(); //경로 초기화
        }
        else if (distance <= 0.5) //0.5이하면
        {
            bossState = BossState.Attack; //공격상태로
            agent.isStopped = true; //이동 중단
            bossAni.SetBool("Walk Forward", false);
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
            bossState = BossState.Walk;
            agent.isStopped = false; //이동시작
        }
    }
    void Died()
    {
        //Debug.Log("보스 사망");
        bossAni.SetBool("Walk Forward", false);
        bossAni.SetTrigger("Die");
        ExitCube.SetActive(false);
        //Destroy(gameObject);


        //bossState = BossState.Died; // 보스의 상태를 사망 상태로 변경하거나 필요한 처리 수행
        //StopAllCoroutines(); // 모든 코루틴 중지 또는 필요한 작업 수행
        //agent.isStopped = true; // 이동 중단 또는 필요한 작업 수행
    }

    IEnumerator AttackPeriodically()
    {
        isAttacking = true; // 공격 중임을 표시

        while (bossState == BossState.Walk) // 보스가 Walk(추적) 상태인 동안에만 반복
        {
            yield return new WaitForSeconds(3f); // 3초 대기 후에 아래 코드 실행

            AttackPlayer(); // 공격 함수 실행
        }

        isAttacking = false; // Walk(추적) 상태가 아니므로 공격 종료
    }

    void AttackPlayer()
    {
        //랜덤 인덱스
        int randomIndex = Random.Range(0, bulletPref.Length);
        // 플레이어를 바라보도록 회전시킵니다.
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // 불렛 생성 후 발사합니다.
        //  Quaternion.identity
        GameObject bullet = Instantiate(bulletPref[randomIndex], bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        //GameObject bullet = Instantiate(bulletPref[randomIndex], transform.position + transform.forward, transform.rotation);

        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);

    }

    //소화기한테 데미지를 받았을때
    //private void OnParticleCollision(GameObject other)
    //{
    //    if (other.CompareTag("Water")) // 충돌한 오브젝트가 Fire 태그를 가지고 있는지 확인합니다.
    //    {
    //        Damaged(1f);
    //    }
    //}
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {

            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();


            float force = 6f;
            Vector3 direction = collision.transform.position - transform.position;
            direction.Normalize();

            playerRb.AddForce(direction * force, ForceMode.Impulse);
            playerHp.Damaged(5f);
        }
    }

}
