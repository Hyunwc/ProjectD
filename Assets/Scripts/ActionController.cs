using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 

    private RaycastHit hitInfo;  // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트만 획득.

    [SerializeField]
    private Text actionText;  // 행동을 보여 줄 텍스트

    [SerializeField]
    private Inventory theInventory;  // Inventory 스크립트

    public GameObject handle;

    private bool state = false;

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
            if (hitInfo.transform.tag == "Item" | hitInfo.transform.tag == "FlashLight" ) // tag가 Item인 경우에만 획득 가능하다는 text 출력
            {
                ItemInfoAppear();
            }
        }
        else
            ItemInfoDisappear(); // Item이 아닐 시 text 출력x
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }


    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 했습니다."); // 인벤토리 넣기
                if (hitInfo.transform.tag == "FlashLight") // Tag가 FlashLight 일 때, 플레이어의 FlashLight를 활성화
                {
                    state = true;
                    handle.gameObject.SetActive(true);
                }
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            } 
        }
    }
}
