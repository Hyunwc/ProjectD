using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;  // 아이템 습득이 가능한 최대 거리
    [SerializeField] private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트만 획득.
    [SerializeField] private Text actionText;  // 행동을 보여 줄 텍스트
    [SerializeField] private Inventory theInventory;  // Inventory 스크립트
    private GameManager gameManager;

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    private RaycastHit hitInfo;  // 충돌체 정보 저장
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

    [SerializeField] private Text FireExtText1;  // 패널 텍스트 1
    [SerializeField] private Text FireExtText2;  // 패널 텍스트 2
    [SerializeField] private Text FireExtText3;  // 패널 텍스트 3
    [SerializeField] private Text FireExtText4;  // 패널 텍스트 4 (임의)

    [SerializeField] private GameObject fireextinguisher;  // 패널 텍스트 

    private void Start()
    {
        itemAudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        CheckItem(); // 아이템 확인
        TryAction(); // 아이템 습득

        if (FireExtPanel.activeSelf) //FIreExtPanel이 활성화된 상태에서만 F키를 입력받음 
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
        if (Input.GetKeyDown(KeyCode.E)) // E키 입력 시
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag != null) // 해당하는 tag의 경우에만 획득 가능하다는 text 출력, CompareTag("")
            {
                ItemInfoAppear();
            }
        }
        else
            ItemInfoDisappear(); // Item이 아닐 시 text 출력x
    }

    private void ItemInfoAppear() // 아이템 정보 text 출력 
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
    private void DisappearPanel() // FireExtPanel을 비활성화
    {
        FireExtPanel.SetActive(false);
        FireExt2.SetActive(false);
    }

    private void PlayMode() // Player 제한을 해제하고 다시 움직이게끔 
    {
        actionText.enabled = true; // 텍스트 보이게

        Cursor.visible = false; // 커서 숨기기
        /*playerMove.enabled = true; // 플레이어 조작 활성화
        playerfire.enabled = true;
        cameraRotate.enabled = true; */
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 했습니다."); // 인벤토리 넣기

                if (hitInfo.transform.CompareTag("FireBell"))
                {
                    gameManager.bellCheck = true;
                    // AudioSource를 찾아와서 사운드를 재생
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

                if (hitInfo.transform.CompareTag("FlashLight")) // Tag가 FlashLight 일 때, 플레이어의 FlashLight를 활성화
                {
                    //state = true;
                    handle.gameObject.SetActive(true);
                }
                else if (hitInfo.transform.CompareTag("Medicine")) // tag가 Medicine 일 때, 획득시 hp 15회복
                {
                    hpBar.value += 15;
                }
                else if (hitInfo.transform.CompareTag("FireExt")) // FireExt를 받았을 때 소화기 사용 패널 팝업
                {
                    actionText.enabled = false; // 텍스트 숨기기
                    FireExtPanel.SetActive(true);


                    Cursor.lockState = CursorLockMode.None;  // 커서 잠금 해제 
                    Cursor.visible = true; // 커서 보이게
                    /*playerMove.enabled = false; // 플레이어 조작 비활성화
                    playerfire.enabled = false;
                    cameraRotate.enabled = false; */

                    FireExt2.SetActive(true);

                    FireExtText1.text = "1. 소화기의 몸체를 단단히 잡고 고정시킨 뒤, 안전핀을 뽑는다.";
                    FireExtText2.text = "2. 바람을 등지고 소화기의 호스를 불 쪽으로 향하게 잡는다.";
                    FireExtText3.text = "3. 발화지점을 향해 손잡이를 누른다. 분사 방향은 위에서 아래로 15도 각도를 유지한다.";

                    FireExtText4.text = "위 단계를 순차적으로 진행했다면 " + "<color=yellow>" + "(F)" + "</color>" + " 버튼을 누르세요."; //임의


                }

            }
        }
    }
}