using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;  // ������ ������ ������ �ִ� �Ÿ�
    [SerializeField] private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ȹ��.
    [SerializeField] private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ
    [SerializeField] private Inventory theInventory;  // Inventory ��ũ��Ʈ
    private GameManager gameManager;

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 
    public RaycastHit hitInfo;  // �浹ü ���� ����
    public GameObject handle;
    //private bool state = false;
    public Slider hpBar;

    public GameObject FireExtPanel;
    public GameObject FireExt2;

    public AudioClip firebellSound;

    [SerializeField] private PlayerMove playerMove;  // PlayerMove
    [SerializeField] private PlayerFire playerfire;  // PlayerFire

    [SerializeField] private Text FireExtText1;  // �г� �ؽ�Ʈ 1
    [SerializeField] private Text FireExtText2;  // �г� �ؽ�Ʈ 2
    [SerializeField] private Text FireExtText3;  // �г� �ؽ�Ʈ 3
    [SerializeField] private Text FireExtText4;  // �г� �ؽ�Ʈ 4 (����)

    public GameObject CheckText;

    public GameObject eleText; //elevator text
    public Text eleText1;
    public bool eletextpoint = false;
    public GameObject Fail;


    private QuestPanel questPanel;
    public FireEx fireEx;

    public bool fireExtFirst = false;
    private NpcController npcController;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        questPanel = FindObjectOfType<QuestPanel>();
        npcController = FindObjectOfType<NpcController>();
    }
    void Update()
    {
        CheckItem(); // ������ Ȯ��
        TryAction(); // ������ ����

        if (eleText.activeSelf) //FIreExtPanel�� Ȱ��ȭ�� ���¿����� FŰ�� �Է¹��� 
        {

            if (Input.GetKeyDown(KeyCode.Alpha1) && eletextpoint == true)
            {
                eleText1.text = "ȭ�� ��Ȳ����\n ���������͸� ž���ϸ� �ȵ˴ϴ�!";
                Fail.SetActive(true);
                Invoke("DeactivateEleText", 3f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && eletextpoint == true)
            {
                questPanel.elepoint++;
                eleText.SetActive(false);
            }
        }

        if (FireExtPanel.activeSelf) //FIreExtPanel�� Ȱ��ȭ�� ���¿����� FŰ�� �Է¹��� 
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                CheckText.SetActive(true);
            } if (Input.GetKeyDown(KeyCode.F) && CheckText.activeSelf)
        {
                Invoke("DisappearPanel", 1f);
        }
        }
        
    }

  
    void DeactivateEleText()
    {
        eleText.SetActive(false);
    }

    private void DisappearPanel()
    {
        if (FireExtPanel != null)
        {
            FireExtPanel.SetActive(false);
        }
        if (FireExt2 != null)
        {
            FireExt2.SetActive(false);
        }
        if(CheckText != null)
        {
            CheckText.SetActive(false);
        }
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        {
            actionText.text = null;

            ItemInfoDisappear(); // Item�� �ƴ� �� text ���x
        }

    }
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        if (actionText != null)
        {
            actionText.gameObject.SetActive(true);
            ItemPickUp itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
            if (itemPickUp != null && itemPickUp.item != null)
            {
                actionText.text = itemPickUp.item.itemName + "<color=yellow>" + " (E) " + "</color>";
            }
        }
    }
    private void ItemInfoDisappear()
    {
        pickupActivated = false;

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

                if(hitInfo.transform.CompareTag("ele") && eletextpoint == false)
                {
                    eletextpoint = true;
                    eleText.SetActive(true);
                    eleText1.text = "���������͸� ž���Ͻðڽ��ϱ� ?\n1. Ÿ�� ��������                2. Ÿ�� �ʴ´�";
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
                else if (hitInfo.transform.CompareTag("FireExt") ) // FireExt�� �޾��� �� ��ȭ�� ��� �г� �˾�
                {
                    //actionText.enabled = false; // �ؽ�Ʈ �����
                    
                    fireEx.capacity = 100f;
                    fireEx.capacityText.text = "��ȭ��\n���� �뷮\n" + fireEx.capacity + "%";
                    Destroy(hitInfo.transform.gameObject);

                    if (fireExtFirst == false)
                    {
                        fireExtFirst = true;
                        playerMove.GetFireExt = true;

                        FireExtPanel.SetActive(true);
                        FireExt2.SetActive(true);

                        FireExtText1.text = "1. ��ȭ���� ��ü�� �ܴ��� ��� ������Ų ��, �������� �̴´�.";
                        FireExtText2.text = "2. �ٶ��� ������ ��ȭ���� ȣ���� �� ������ ���ϰ� ��´�.";
                        FireExtText3.text = "3. ��ȭ������ ���� �����̸� ������. �л� ������ ������ �Ʒ��� 15�� ������ �����Ѵ�.";
                        FireExtText4.text = "��ȭ�� ������ �����ߴٸ�\n" + "<color=yellow>" + "(F)" + "</color>" + " ��ư�� ���� â�� �ݾ��ּ���."; //����


                    }
                    //Cursor.lockState = CursorLockMode.None;  // Ŀ�� ��� ���� 
                    //Cursor.visible = true; // Ŀ�� ���̰�
                    //playerMove.enabled = false; // �÷��̾� ���� ��Ȱ��ȭ
                    //playerfire.enabled = false;
                    //cameraRotate.enabled = false;

                }
                //else if (hitInfo.transform.CompareTag("npc"))
                //{
                //    npcController.MoveToDestination();
                //}
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                float rayDistance = 5.0f; // �� ���� �ʿ信 ���� �������ּ���.
                RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.CompareTag("npc"))
                    {
                        hit.transform.GetComponent<NpcController>().MoveToDestination();
                    }
                }
            }

            }
        }
    }
