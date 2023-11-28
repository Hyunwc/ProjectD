using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed; //�̵� �ӵ�
    public float jumpPower; //������
    public float rotateSpeed; //ȸ���ӵ�
    public GameObject subMenu; //����޴�

    int jumpCount; //���� Ƚ��

    public PlayerFire gun;
    public FireEx fireEx;
    public AudioSource moveSound; // �߼Ҹ� ����
    bool isMoving; // �÷��̾ �����̴��� ����
    public AudioSource jumpSound;//���� ����
    bool isJumping = false; // ���� �� ���� Ȯ��

    private float gunCount; //���� �Ѿ�
    //private float maxgunCount = 8; //�ִ� �Ѿ�
    public bool isReload = false; //������������ ���������̸� true
    public Text bulletCountText; //�Ѿ˼� ǥ��

    public bool isGun = false;
    public bool isFireEx = false;
    public bool isMove = true; // �÷��̾� ������ boolŸ��
    public bool isShot = true; // true�϶��� �Ѿ� ������
    Rigidbody rb; //�÷��̾��� rigidbody ������Ʈ
    private CameraRotate rotateToMouse;
    [SerializeField] private GameObject FirePanel;
   
    void Start()
    {
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
        //�÷��̾��� rigidboyd������Ʈ �����ͼ� ����
        rb = GetComponent<Rigidbody>();
        gunCount = 8;
        //gun = GetComponent<PlayerFire>();
        rotateToMouse = GetComponent<CameraRotate>();
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

        //�Ѿ˼��� 0���� ũ�� ���������°� �ƴҶ�
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun.gameObject.SetActive(true);
            fireEx.gameObject.SetActive(false);
            isGun = true;
            isFireEx = false;
        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            gun.gameObject.SetActive(false);
            fireEx.gameObject.SetActive(true);
            isGun = false;
            isFireEx = true;
        }

        if (isGun && Input.GetMouseButtonDown(0) && gunCount > 0 && !isReload)
        {
            if(isShot)
            {
                gun.Shot();
                gunCount--;
                UpdateBulletUI();
                Debug.Log("���� �Ѿ˼� : " + gunCount);
            }
            
        }
        else if(Input.GetMouseButton(0) && isFireEx)
        {
            if (isShot)
            {
                fireEx.Shot();
            }
            
        }

        if (gunCount >= 0)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            subMenu.SetActive(true);
            //�÷��̾� ��� �ߴ�
            //GetComponent<PlayerMove>().enabled = false; //������ ����
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

        if (collision.gameObject.tag == "Fire")
        {
            FirePanel.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            FirePanel.SetActive(false);
        }
    }


    //������
    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("������ ����");
            StartCoroutine(ReloadCoroutine());
            isReload = true;
        }
    }

    public IEnumerator ReloadCoroutine()
    {

        isReload = true;
        bulletCountText.text = "���� ��...";
        yield return new WaitForSeconds(3f);
        Debug.Log("�Ѿ� 8�߷� ����");
        gunCount = 8;
        UpdateBulletUI(); // ���� �Ŀ� UI ������Ʈ
        Debug.Log("���� �� ���� : " + gunCount);
        // �ڷ�ƾ�� ����ǰ� �� �Ŀ� isReload�� false�� �����Ͽ� �������� �������� ǥ���մϴ�.
        isReload = false;
        Debug.Log(isReload);
        // �ڷ�ƾ ����
    }

    void UpdateBulletUI()
    {
        bulletCountText.text = "Revolver\n���� �Ѿ� : \n " + gunCount + "/ 8";
    }
}
