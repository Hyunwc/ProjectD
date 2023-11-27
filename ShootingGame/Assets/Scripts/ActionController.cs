using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;  // ������ ������ ������ �ִ� �Ÿ�
    [SerializeField] private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ȹ��.
    [SerializeField] private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ
    [SerializeField] private Inventory theInventory;  // Inventory ��ũ��Ʈ
    private GameManager gameManager;

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 
    private RaycastHit hitInfo;  // �浹ü ���� ����
    public GameObject handle;
    //private bool state = false;
    public Slider hpBar;

    public GameObject FireExtPanel;
    public GameObject FireExt2;

    public AudioClip firebellSound;
    private AudioSource itemAudio;

    [SerializeField] private PlayerMove playerMove;  // PlayerMove
    [SerializeField] private PlayerFire playerfire;  // PlayerFire
    [SerializeField] private CameraRotate cameraRotate;  // CameraRotate
    [SerializeField] private AudioClip fireBellSoundClip;//fireBellSound

    [SerializeField] private Text FireExtText1;  // �г� �ؽ�Ʈ 1
    [SerializeField] private Text FireExtText2;  // �г� �ؽ�Ʈ 2
    [SerializeField] private Text FireExtText3;  // �г� �ؽ�Ʈ 3
    [SerializeField] private Text FireExtText4;  // �г� �ؽ�Ʈ 4 (����)

    [SerializeField] private GameObject fireextinguisher;  // �г� �ؽ�Ʈ 

    private void Start()
    {
        itemAudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        CheckItem(); // ������ Ȯ��
        TryAction(); // ������ ����

        if (FireExtPanel.activeSelf) //FIreExtPanel�� Ȱ��ȭ�� ���¿����� FŰ�� �Է¹��� 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DisappearPanel();
                fireextinguisher.SetActive(true);
                PlayMode();

            }
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Firstslot.SetActive(true);
            //ETC(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Hand.SetActive(false);
            slots[1].SetActive(true);
            //ETC(1);
        }
        // ETC(); */
    }



    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // EŰ �Է� ��
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag != null) // �ش��ϴ� tag�� ��쿡�� ȹ�� �����ϴٴ� text ���, CompareTag("")
            {
                ItemInfoAppear();
            }
        }
        else
            ItemInfoDisappear(); // Item�� �ƴ� �� text ���x
    }

    private void ItemInfoAppear() // ������ ���� text ��� 
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);


        if (actionText != null)
        {
            actionText.gameObject.SetActive(false);
        }
    }
    private void DisappearPanel() // FireExtPanel�� ��Ȱ��ȭ
    {
        FireExtPanel.SetActive(false);
        FireExt2.SetActive(false);
    }

    private void PlayMode() // Player ������ �����ϰ� �ٽ� �����̰Բ� 
    {
        actionText.enabled = true; // �ؽ�Ʈ ���̰�

        Cursor.visible = false; // Ŀ�� �����
        /*playerMove.enabled = true; // �÷��̾� ���� Ȱ��ȭ
        playerfire.enabled = true;
        cameraRotate.enabled = true; */
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� �߽��ϴ�."); // �κ��丮 �ֱ�

                if (hitInfo.transform.CompareTag("FireBell"))
                {
                    gameManager.bellCheck = true;
                    // AudioSource�� ã�ƿͼ� ���带 ���
                    AudioSource fireBellAudio = hitInfo.transform.GetComponent<AudioSource>();
                    if (fireBellAudio != null && fireBellAudio.clip != null)
                    {
                        fireBellAudio.Play();
                    }
                }
                else
                {
                    theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                    Destroy(hitInfo.transform.gameObject);
                }

                ItemInfoDisappear();

                if (hitInfo.transform.CompareTag("FlashLight")) // Tag�� FlashLight �� ��, �÷��̾��� FlashLight�� Ȱ��ȭ
                {
                    //state = true;
                    handle.gameObject.SetActive(true);
                }
                else if (hitInfo.transform.CompareTag("Medicine")) // tag�� Medicine �� ��, ȹ��� hp 15ȸ��
                {
                    hpBar.value += 15;
                }
                else if (hitInfo.transform.CompareTag("FireExt")) // FireExt�� �޾��� �� ��ȭ�� ��� �г� �˾�
                {
                    actionText.enabled = false; // �ؽ�Ʈ �����
                    FireExtPanel.SetActive(true);


                    Cursor.lockState = CursorLockMode.None;  // Ŀ�� ��� ���� 
                    Cursor.visible = true; // Ŀ�� ���̰�
                    /*playerMove.enabled = false; // �÷��̾� ���� ��Ȱ��ȭ
                    playerfire.enabled = false;
                    cameraRotate.enabled = false; */

                    FireExt2.SetActive(true);

                    FireExtText1.text = "1. ��ȭ���� ��ü�� �ܴ��� ��� ������Ų ��, �������� �̴´�.";
                    FireExtText2.text = "2. �ٶ��� ������ ��ȭ���� ȣ���� �� ������ ���ϰ� ��´�.";
                    FireExtText3.text = "3. ��ȭ������ ���� �����̸� ������. �л� ������ ������ �Ʒ��� 15�� ������ �����Ѵ�.";

                    FireExtText4.text = "�� �ܰ踦 ���������� �����ߴٸ� " + "<color=yellow>" + "(F)" + "</color>" + " ��ư�� ��������."; //����


                }

            }
        }
    }
}