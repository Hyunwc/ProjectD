using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed; //이동 속도
    public float jumpPower; //점프힘
    public float rotateSpeed; //회전속도

    int jumpCount; //점프 횟수

    Rigidbody rb; //플레이어의 rigidbody 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        //플레이어의 rigidboyd컴포넌트 가져와서 저장
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
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
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
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
}
