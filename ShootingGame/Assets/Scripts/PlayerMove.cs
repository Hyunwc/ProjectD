using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed; //�̵� �ӵ�
    public float jumpPower; //������
    public float rotateSpeed; //ȸ���ӵ�
    public GameObject subMenu; //����޴�

    int jumpCount; //���� Ƚ��

    public PlayerFire gun;

    
    Rigidbody rb; //�÷��̾��� rigidbody ������Ʈ
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
        //�÷��̾��� rigidboyd������Ʈ �����ͼ� ����
        rb = GetComponent<Rigidbody>();
        //gun = GetComponent<PlayerFire>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        if(Input.GetMouseButtonDown(0))
        {
            gun.Shot();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            subMenu.SetActive(true);
            //�÷��̾� ��� �ߴ�
            GetComponent<PlayerMove>().enabled = false; //������ ����
            //GetComponent<PlayerFire>().enabled = false; //��� ����
            GetComponentInChildren<CameraRotate>().enabled = false;  //ī�޶� ȸ�� ����

            //���� ���� ��� ���� ��� �ߴ�
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.enabled = false;
            }
        }
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

        // ���콺�� �¿� ������ �Է��� ���ڷ� �޾Ƽ� ����
        float mouseMoveX = Input.GetAxis("Mouse X");
        //���콺�� ������ ��ŭ Y�� ȸ��
        transform.Rotate(0, mouseMoveX * rotateSpeed * Time.deltaTime, 0);
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
        }
    }

    //� ��ü�� �浹�� ������ ������ ȣ��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //����Ƚ�� �ʱ�ȭ
            jumpCount = 0;
        }
    }
}
