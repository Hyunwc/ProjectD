using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public FireEx fireEx;
    public WaterPaper waterPaper;
    public Medicine medicine;
    public AudioSource moveSound; // 발소리 사운드
    bool isMoving; // 플레이어가 움직이는지 여부
    public AudioSource jumpSound;//점프 사운드
    bool isJumping = false; // 점프 중 여부 확인

    private PlayerMp plymp;

    private float gunCount; //남은 총알
    //private float maxgunCount = 8; //최대 총알
    public bool isReload = false; //재장전중인지 재장전중이면 true
    //public GameObject waterGunPanel;
    public Text bulletCountText; //총알수 표시
   
    public bool isGun = false;
    public bool isFireEx = false;
    public bool isWaterPaper = false;
    public bool isMedi = false;
    public bool isMove = true; // 플레이어 움직임 bool타입
    public bool isShot = true; // true일때만 총알 나가게
    
    Rigidbody rb; //플레이어의 rigidbody 컴포넌트
    private CameraRotate rotateToMouse;
    //[SerializeField] private GameObject FirePanel;
    private NewInventory newInven;
   

    public bool GetFireExt = false;
   
    void Start()
    {
        Cursor.visible = false; //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서가 게임 화면 못 벗어나게
        //플레이어의 rigidboyd컴포넌트 가져와서 저장
        rb = GetComponent<Rigidbody>();
        gunCount = 10;
        //gun = GetComponent<PlayerFire>();
        rotateToMouse = GetComponent<CameraRotate>();
        newInven = FindObjectOfType<NewInventory>();
        plymp = GetComponent<PlayerMp>();
        //reloadSource = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            Move();
            Jump();
            UpdateRotate();
        }

        InputEvent();
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

        //플레이어가 움직일 때만 사운드 재생
        ManageMoveSound(Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);
    
    }
    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CalculateRotation(mouseX, mouseY);
    }

    void InputEvent()
    {
        //여기는 일단 하드코딩
        //여기 14주차에 배열로 처리할겁니다 ㅠㅠ (태현)
        //총알수가 0보다 크고 재장전상태가 아닐때
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            newInven.ChangeSlot(0);
            gun.gameObject.SetActive(true);
            waterPaper.gameObject.SetActive(false);
            //waterGunPanel.SetActive(true);
            fireEx.gameObject.SetActive(false);
            medicine.gameObject.SetActive(false);
            isGun = true;
            isFireEx = false;
            isWaterPaper = false;
            isMedi = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GetFireExt == true)
        {
            newInven.ChangeSlot(1);
            gun.gameObject.SetActive(false);
            //waterGunPanel.SetActive(false);
            waterPaper.gameObject.SetActive(false);
            fireEx.gameObject.SetActive(true);
            medicine.gameObject.SetActive(false);
            isGun = false;
            isFireEx = true;
            isWaterPaper = false;
            isMedi = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            newInven.ChangeSlot(2);
            gun.gameObject.SetActive(false);
            fireEx.gameObject.SetActive(false);
            medicine.gameObject.SetActive(false);
            waterPaper.gameObject.SetActive(true);
            isGun = false;
            isFireEx = false;
            isWaterPaper = true;
            isMedi = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            newInven.ChangeSlot(3);
            gun.gameObject.SetActive(false);
            fireEx.gameObject.SetActive(false);
            medicine.gameObject.SetActive(true);
            waterPaper.gameObject.SetActive(false);
            isGun = false;
            isFireEx = false;
            isWaterPaper = false;
            isMedi = true;
        }

        if (isGun && Input.GetMouseButtonDown(0) && gunCount > 0 && !isReload)
        {
            if (isShot)
            {
                gun.Shot();
                gunCount--;
                UpdateBulletUI();
                Debug.Log("현재 총알수 : " + gunCount);
            }
        }

        
        else if (Input.GetMouseButtonDown(1) && isWaterPaper) // 오른쪽 마우스 버튼을 눌렀을 때
        {
            plymp.isWater = !plymp.isWater;
            waterPaper.isUsing = !waterPaper.isUsing;
        }
        
        else if (Input.GetMouseButton(0) && isFireEx)
        {
            if (isShot)
            {
                fireEx.Shot();
            }
        }
        else if (Input.GetMouseButtonDown(0) && isMedi)
        {
            medicine.UseMedicine();
        }

        if (gunCount >= 0)
        {
            Reload();
        }

        if (gunCount == 0 && !isReload)
        {
            StartCoroutine(ReloadCoroutine());
            isReload = true;
        }

        //if (gunCount == 0)
        //{
        //    bulletCountText.text = "WaterGun\n현재 총알 : 없음\n재장전 요망(R)";
        //    Reload();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rotateToMouse.isPause = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            subMenu.SetActive(true);
        }

        if(Medicine.mediCount == 0)
        {
            medicine.gameObject.SetActive(false);
            isMedi = false;
        }
    }
    //IEnumerator ShotDelay()
    //{
    //    yield return new WaitForSeconds(0.5f);
       
    //}
    void ManageMoveSound(bool isMoving)
    {
       
        if (isJumping) // 만약 점프 중이라면
        {
            // 점프 중일 때는 무브 사운드를 중지
            if (moveSound.isPlaying)
            {
                moveSound.Stop();
            }
        }
        else // 점프 중이 아니라면
        {
            if (isMoving)
            {
                // 플레이어가 움직이기 시작할 때 사운드 재생
                if (!moveSound.isPlaying)
                {
                    moveSound.Play();
                }
            }
            else
            {
                // 플레이어가 멈출 때 사운드 중지
                if (moveSound.isPlaying)
                {
                    moveSound.Stop();
                }
            }
        }
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
            if (jumpSound != null) jumpSound.Play(); // 점프 사운드 시작
            isJumping = true;// 점프 상태변경
        }
    }
    
    //어떤 물체와 충돌을 시작한 순간에 호출
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;// 점프 상태변경
            jumpCount = 0;
            ManageMoveSound(isMoving);//발소리 사운드 메서드를 호출
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
        bulletCountText.text = "장전 중...";
        yield return new WaitForSeconds(3f);
        gun.ReLoadSound();
        
        gunCount = 10;
        UpdateBulletUI(); // 장전 후에 UI 업데이트
       
        // 코루틴이 실행되고 난 후에 isReload를 false로 설정하여 재장전이 끝났음을 표시합니다.
        isReload = false;
        
        // 코루틴 종료
    }

    void UpdateBulletUI()
    {
        bulletCountText.text = "WaterGun\n현재 총알 : \n " + gunCount + " / 10";
    }
}
