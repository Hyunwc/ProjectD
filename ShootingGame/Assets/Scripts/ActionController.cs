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

    [SerializeField] private Text FireExtText1;  // �г� �ؽ�Ʈ 1
    [SerializeField] private Text FireExtText2;  // �г� �ؽ�Ʈ 2
    [SerializeField] private Text FireExtText3;  // �г� �ؽ�Ʈ 3
    [SerializeField] private Text FireExtText4;  // �г� �ؽ�Ʈ 4 (����)
    [SerializeField] private GameObject fireextinguisher;  // �г� �ؽ�Ʈ 

    public GameObject eleText; //elevator text
    public Text eleText1;

    private QuestPanel questPanel;


    private void Start()
    {
        itemAudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        questPanel = FindObjectOfType<QuestPanel>();
    }
    void Update()
    {
        CheckItem(); // ������ Ȯ��
        TryAction(); // ������ ����

        if (eleText.activeSelf) //FIreExtPanel�� Ȱ��ȭ�� ���¿����� FŰ�� �Է¹��� 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                eleText1.text = "���������͸� ž���ϸ� �ȵ˴ϴ�!";

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                questPanel.elepoint++;
                eleText.SetActive(false);
                /*questPanel.checkbox4.SetActive(true);
                questPanel.Quest4.text = "<color=#2FFF00>" + " ���������� ž������ �ʱ� " + "</color>";
                gameManager.isQuest4Complete = true;*/
            }
        } 
 
        if (FireExtPanel.activeSelf) //FIreExtPanel�� Ȱ��ȭ�� ���¿����� FŰ�� �Է¹��� 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DisappearPanel();
                fireextinguisher.SetActive(true);
                PlayMode();
            }
        }
    }

    private void PlayMode() // Player ������ �����ϰ� �ٽ� �����̰Բ� 
    {
        actionText.enabled = true; // �ؽ�Ʈ ���̰�

        Cursor.visible = false; // Ŀ�� �����
        /*playerMove.enabled = true; // �÷��̾� ���� Ȱ��ȭ
        playerfire.enabled = true;
        cameraRotate.enabled = true; */
    }

    private void DisappearPanel() // FireExtPanel�� ��Ȱ��ȭ
    {
        FireExtPanel.SetActive(false);
        FireExt2.SetActive(false);
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
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + " (E) " + "</color>";
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

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� �߽��ϴ�."); // �κ��丮 �ֱ�

                if(hitInfo.transform.CompareTag("ele"))
                {
                    eleText.SetActive(true);
                }

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
                    //theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                    //Destroy(hitInfo.transform.gameObject);
                }

                ItemInfoDisappear();
                if (hitInfo.transform.CompareTag("FlashLight")) // Tag�� FlashLight �� ��, �÷��̾��� FlashLight�� Ȱ��ȭ
                {
                    //state = true;
                    handle.gameObject.SetActive(true);
                }
                else if (hitInfo.transform.CompareTag("Medicine")) // tag�� Medicine �� ��, ȹ��� ������ ���� �� medicount ++ 
                {
                    Destroy(hitInfo.transform.gameObject);
                    Medicine.mediCount++;
                }
                else if (hitInfo.transform.CompareTag("FireExt")) // FireExt�� �޾��� �� ��ȭ�� ��� �г� �˾�
                {
                    actionText.enabled = false; // �ؽ�Ʈ �����
                    FireExtPanel.SetActive(true);
                    FireExt2.SetActive(true);

                    FireExtText1.text = "1. ��ȭ���� ��ü�� �ܴ��� ��� ������Ų ��, �������� �̴´�.";
                    FireExtText2.text = "2. �ٶ��� ������ ��ȭ���� ȣ���� �� ������ ���ϰ� ��´�.";
                    FireExtText3.text = "3. ��ȭ������ ���� �����̸� ������. �л� ������ ������ �Ʒ��� 15�� ������ �����Ѵ�.";
                    FireExtText4.text = "�� �ܰ踦 ���������� �����ߴٸ� " + "<color=yellow>" + "(F)" + "</color>" + " ��ư�� ��������."; //����

                    Cursor.lockState = CursorLockMode.None;  // Ŀ�� ��� ���� 
                    Cursor.visible = true; // Ŀ�� ���̰�
                    playerMove.enabled = false; // �÷��̾� ���� ��Ȱ��ȭ
                    playerfire.enabled = false;
                    //cameraRotate.enabled = false;
                }

            }
        }
    }
}
