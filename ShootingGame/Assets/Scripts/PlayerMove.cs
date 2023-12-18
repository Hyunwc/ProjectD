using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public WaterPaper waterPaper;
    public Medicine medicine;
    public AudioSource moveSound; // �߼Ҹ� ����
    bool isMoving; // �÷��̾ �����̴��� ����
    public AudioSource jumpSound;//���� ����
    bool isJumping = false; // ���� �� ���� Ȯ��

    private PlayerMp plymp;

    private float gunCount; //���� �Ѿ�
    //private float maxgunCount = 8; //�ִ� �Ѿ�
    public bool isReload = false; //������������ ���������̸� true
    //public GameObject waterGunPanel;
    public Text bulletCountText; //�Ѿ˼� ǥ��
   
    public bool isGun = false;
    public bool isFireEx = false;
    public bool isWaterPaper = false;
    public bool isMedi = false;
    public bool isMove = true; // �÷��̾� ������ boolŸ��
    public bool isShot = true; // true�϶��� �Ѿ� ������
    
    Rigidbody rb; //�÷��̾��� rigidbody ������Ʈ
    private CameraRotate rotateToMouse;
    //[SerializeField] private GameObject FirePanel;
    private NewInventory newInven;
    [SerializeField] private AudioClip reloadClip;
    private AudioSource reloadSource;

    public bool GetFireExt = false;
   
    void Start()
    {
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
        //�÷��̾��� rigidboyd������Ʈ �����ͼ� ����
        rb = GetComponent<Rigidbody>();
        gunCount = 10;
        //gun = GetComponent<PlayerFire>();
        rotateToMouse = GetComponent<CameraRotate>();
        newInven = FindObjectOfType<NewInventory>();
        plymp = GetComponent<PlayerMp>();
        reloadSource = GetComponent<AudioSource>();
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

    void InputEvent()
    {
        //����� �ϴ� �ϵ��ڵ�
        //���� 14������ �迭�� ó���Ұ̴ϴ� �Ф� (����)
        //�Ѿ˼��� 0���� ũ�� ���������°� �ƴҶ�
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
                Debug.Log("���� �Ѿ˼� : " + gunCount);
            }
        }

        
        else if (Input.GetMouseButtonDown(1) && isWaterPaper) // ������ ���콺 ��ư�� ������ ��
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
        //    bulletCountText.text = "WaterGun\n���� �Ѿ� : ����\n������ ���(R)";
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
        reloadSource.PlayOneShot(reloadClip);
        gunCount = 10;
        UpdateBulletUI(); // ���� �Ŀ� UI ������Ʈ
       
        // �ڷ�ƾ�� ����ǰ� �� �Ŀ� isReload�� false�� �����Ͽ� �������� �������� ǥ���մϴ�.
        isReload = false;
        
        // �ڷ�ƾ ����
    }

    void UpdateBulletUI()
    {
        bulletCountText.text = "WaterGun\n���� �Ѿ� : \n " + gunCount + " / 10";
    }
}
