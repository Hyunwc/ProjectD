using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed; //이동 속도
    public float jumpPower; //점프힘
    public float rotateSpeed; //회전속도
    
    int jumpCount; //점프 횟수

    public AudioSource moveSound; // 발소리 사운드
    public AudioSource jumpSound;//점프 사운드
    bool isMoving; // 플레이어가 움직이는지 여부
    bool isJumping = false; // 점프 중 여부 확인

    public PlayerHp playerHp;
    public PlayerMp playerMp;

    Rigidbody rb; //플레이어의 rigidbody 컴포넌트
    private CameraRotate rotateToMouse;

    void Start()
    {
        Cursor.visible = false; //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서가 게임 화면 못 벗어나게
        //플레이어의 rigidboyd컴포넌트 가져와서 저장
        rb = GetComponent<Rigidbody>();
 
        rotateToMouse = GetComponent<CameraRotate>();    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        UpdateRotate();
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
}
