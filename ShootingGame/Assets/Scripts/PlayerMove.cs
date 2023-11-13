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

    private float gunCount; //���� �Ѿ�
    //private float maxgunCount = 8; //�ִ� �Ѿ�
    public bool isReload = false; //������������ ���������̸� true
    public Text bulletCountText; //�Ѿ˼� ǥ��

    public AudioClip ReloadClip;
    private AudioSource playerAudio;
    Rigidbody rb; //�÷��̾��� rigidbody ������Ʈ

    ParticleSystem ps;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    private float flameDamage = 0.2f;
    public PlayerHp playerHp;

    bool isFireEx = true;
    bool isGun = false;

    //��ȭ���
    public GameObject WaterSpawner;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
        //�÷��̾��� rigidboyd������Ʈ �����ͼ� ����
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
        //�Ѿ˼��� 0���� ũ�� ���������°� �ƴҶ�
        if (isGun && Input.GetMouseButtonDown(0) && gunCount > 0 && !isReload)
        {
            gun.Shot();
            gunCount--;
            UpdateBulletUI();
            Debug.Log("���� �Ѿ˼� : " + gunCount);
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
        yield return new WaitForSeconds(3f);
        playerAudio.PlayOneShot(ReloadClip, 1.0f);
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
        bulletCountText.text = "���� �Ѿ� : \n " + gunCount + "/ 8";
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
