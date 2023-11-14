using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;  // 아이템 습득이 가능한 최대 거리
    [SerializeField] private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트만 획득.
    [SerializeField] private Text actionText;  // 행동을 보여 줄 텍스트
    [SerializeField] private Inventory theInventory;  // Inventory 스크립트

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    private RaycastHit hitInfo;  // 충돌체 정보 저장
    public GameObject handle;
    private bool state = false;
    public Slider hpBar;

    public GameObject FireExtPanel;
    public GameObject FireExt2;

    [SerializeField] private PlayerMove playerMove;  // PlayerMove
    [SerializeField] private PlayerFire playerfire;  // PlayerFire
    [SerializeField] private CameraRotate cameraRotate;  // CameraRotate


    void Update()
    {
        CheckItem(); // 아이템 확인
        TryAction(); // 아이템 습득
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
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
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
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 했습니다."); // 인벤토리 넣기
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
                if (hitInfo.transform.CompareTag("FlashLight")) // Tag가 FlashLight 일 때, 플레이어의 FlashLight를 활성화
                {
                    state = true;
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
                    FireExt2.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;  // 커서 잠금 해제 
                    Cursor.visible = true; // 커서 보이게
                    playerMove.enabled = false; // 플레이어 조작 비활성화
                    playerfire.enabled = false;
                    cameraRotate.enabled = false;

                }
            } 
        }
    }
}
