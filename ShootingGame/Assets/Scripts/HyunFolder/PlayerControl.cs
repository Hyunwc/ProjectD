using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed; //�̵� �ӵ�
    public float jumpPower; //������
    public float rotateSpeed; //ȸ���ӵ�
    
    int jumpCount; //���� Ƚ��

    public AudioSource moveSound; // �߼Ҹ� ����
    public AudioSource jumpSound;//���� ����
    bool isMoving; // �÷��̾ �����̴��� ����
    bool isJumping = false; // ���� �� ���� Ȯ��

    public PlayerHp playerHp;
    public PlayerMp playerMp;

    Rigidbody rb; //�÷��̾��� rigidbody ������Ʈ
    private CameraRotate rotateToMouse;

    void Start()
    {
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
        //�÷��̾��� rigidboyd������Ʈ �����ͼ� ����
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
        //����Ű �Ǵ� wasdŰ �Է��� ���ڷ� �޾Ƽ� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //x�࿡�� h�� ����, z�࿡�� v�� ���� ���� ���� 
        Vector3 dir = new Vector3(h, 0, v);
        //��� ������ �ӵ��� �����ϵ��� ����ȭ
        dir.Normalize();

        //�÷��̾� �������� dir�� ��������
        dir = transform.TransformDirection(dir);
        //�̵��� ���⿡ ���ϴ� �ӵ� ���ϱ�
        //transform.position += dir * moveSpeed * Time.deltaTime;

        //�����ۿ��� �̿��� �̵�
        rb.MovePosition(rb.position + (dir * moveSpeed * Time.deltaTime));

        //�÷��̾ ������ ���� ���� ���
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
       
        if (isJumping) // ���� ���� ���̶��
        {
            // ���� ���� ���� ���� ���带 ����
            if (moveSound.isPlaying)
            {
                moveSound.Stop();
            }
        }
        else // ���� ���� �ƴ϶��
        {
            if (isMoving)
            {
                // �÷��̾ �����̱� ������ �� ���� ���
                if (!moveSound.isPlaying)
                {
                    moveSound.Play();
                }
            }
            else
            {
                // �÷��̾ ���� �� ���� ����
                if (moveSound.isPlaying)
                {
                    moveSound.Stop();
                }
            }
        }
    }

    void Jump()
    {
        //�����̽� ���� ����, ���� Ƚ���� 2ȸ �̸��̶��
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            //���� �������� �� �߻�
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            //�����Ҷ����� Ƚ�� ����
            jumpCount++;
            if (jumpSound != null) jumpSound.Play(); // ���� ���� ����
            isJumping = true;// ���� ���º���
        }
    }
    
    //� ��ü�� �浹�� ������ ������ ȣ��
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;// ���� ���º���
            jumpCount = 0;
            ManageMoveSound(isMoving);//�߼Ҹ� ���� �޼��带 ȣ��
        }
    }
}
