using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed; //이동 속도
    public float jumpPower; //점프힘
    public float rotateSpeed; //회전속도
    public GameObject subMenu; //서브메뉴

    int jumpCount; //점프 횟수

    public PlayerFire gun;

    private float gunCount; //남은 총알
    //private float maxgunCount = 8; //최대 총알
    public bool isReload = false; //재장전중인지 재장전중이면 true
    public Text bulletCountText; //총알수 표시

    public AudioClip ReloadClip;
    private AudioSource playerAudio;
    Rigidbody rb; //플레이어의 rigidbody 컴포넌트

    ParticleSystem ps;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    private float flameDamage = 0.2f;
    public PlayerHp playerHp;

    bool isFireEx = true;
    bool isGun = false;

    //소화기액
    public GameObject WaterSpawner;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서가 게임 화면 못 벗어나게
        //플레이어의 rigidboyd컴포넌트 가져와서 저장
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gunCount = 8;
        //gun = GetComponent<PlayerFire>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        //총알수가 0보다 크고 재장전상태가 아닐때
        if (isGun && Input.GetMouseButtonDown(0) && gunCount > 0 && !isReload)
        {
            gun.Shot();
            gunCount--;
            UpdateBulletUI();
            Debug.Log("현재 총알수 : " + gunCount);
        }
        else if (isFireEx && Input.GetMouseButton(0))
        {
            WaterSpawner.SetActive(true);
        }
        else if (isFireEx && !Input.GetMouseButton(0))
        {
            Invoke("DeactivateWaterSpawner", 0.5f);
        }

        if (gunCount >= 0)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            subMenu.SetActive(true);
            //플레이어 기능 중단
            GetComponent<PlayerMove>().enabled = false; //움직임 중지
            //GetComponent<PlayerFire>().enabled = false; //사격 중지
            GetComponentInChildren<CameraRotate>().enabled = false;  //카메라 회전 중지

            //게임 내의 모든 적의 기능 중단
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.enabled = false;
            }
        }
    }


    void Move()
    {
        //방향키 또는 wasd키 입력을 숫자로 받아서 저장
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //x축에는 h의 값을, z축에는 v의 값을 넣은 변수 
        Vector3 dir = new Vector3(h, 0, v);
        //모든 방향의 속도가 동일하도록 정규화
        dir.Normalize();

        //플레이어 기준으로 dir의 방향조절
        dir = transform.TransformDirection(dir);
        //이동할 방향에 원하는 속도 곱하기
        //transform.position += dir * moveSpeed * Time.deltaTime;

        //물리작용을 이용해 이동
        rb.MovePosition(rb.position + (dir * moveSpeed * Time.deltaTime));

        // 마우스의 좌우 움직임 입력을 숫자로 받아서 저장
        float mouseMoveX = Input.GetAxis("Mouse X");
        //마우스가 움직인 만큼 Y축 회전
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);
    }
    void Jump()
    {
        //스페이스 누른 순간, 점프 횟수가 2회 미만이라면
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            //위로 순간적인 힘 발생
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            //점프할때마다 횟수 증가
            jumpCount++;
        }
    }

    //어떤 물체와 충돌을 시작한 순간에 호출
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //점프횟수 초기화
            jumpCount = 0;
        }
    }
    //재장전
    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("재장전 실행");
            StartCoroutine(ReloadCoroutine());
            isReload = true;
        }
    }

    public IEnumerator ReloadCoroutine()
    {
        isReload = true;
        yield return new WaitForSeconds(3f);
        playerAudio.PlayOneShot(ReloadClip, 1.0f);
        Debug.Log("총알 8발로 장전");
        gunCount = 8;
        UpdateBulletUI(); // 장전 후에 UI 업데이트
        Debug.Log("장전 후 총일 : " + gunCount);
        // 코루틴이 실행되고 난 후에 isReload를 false로 설정하여 재장전이 끝났음을 표시합니다.
        isReload = false;
        Debug.Log(isReload);
        // 코루틴 종료
    }

    void UpdateBulletUI()
    {
        bulletCountText.text = "현재 총알 : \n " + gunCount + "/ 8";
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Flame"))
        {
            Debug.Log($"Effect Collision : {other.name}");
            if (playerHp != null)
            {
                playerHp.Damaged(flameDamage);
            }
        }
    }

    void DeactivateWaterSpawner()
    {
        WaterSpawner.SetActive(false);
    }
}
