using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool invectoryActivated = true;  // 인벤토리 활성화 여부

    [SerializeField] private GameObject InventoryBase; // Inventory_Panel 이미지
    [SerializeField] private GameObject SlotsParent;  // Grid 세팅
    [SerializeField] private GameObject SlotCheck; // 선택시 표시되는 슬롯 이미지

    //[SerializeField] private WeaponManger WManager; 

    private Slot[] slots;  // 슬롯 배열
    private int slotnum; // 인벤토리 슬롯 인덱스

    void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
        slotnum = 0;
    }

    void Update()
    {
        Putnum();
        //UseETC();
    }

    public void AcquireItem(Item _item, int _count = 1) // 아이템 획득을 처리 
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName) // 같은 종류의 아이템이 있는지 찾고 해당 슬롯의 갯수 업데이트
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) // 빈 슬롯에 새로운 아이템 추가
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }


    private void Putnum() // 1 ~ 0 키를 누르면 슬롯을 선택할 수 있게끔 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            ChangeSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            ChangeSlot(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            ChangeSlot(8);
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            ChangeSlot(9);
    }


    private void ChangeSlot(int snum) // 슬롯 교체
    {
        SelectSlot(snum);
        //UseETC();
    }

    private void SelectSlot(int snum) // 선택된 슬롯으로 이동
    {
        slotnum = snum;

        SlotCheck.transform.position = slots[slotnum].transform.position; // 슬롯 선택 이미지로 이동 수정 
    }

    /*private void Use() // 활성화된 슬롯을 실제로 사용
    {
        if (slots[slotnum].item != null)
        {
            if (slots[slotnum].item.itemType == Item.ItemType.ETC) // 선택된 슬롯에 무기가 있다면 무기를 착용 
                StartCoroutine(WManager.ChangeWeaponCoroutine(slots[slotnum].item.weaponType, slots[slotnum].item.itemName));
            else if (slots[slotnum].item.itemType == Item.ItemType.Used)
                StartCoroutine(WManager.ChangeWeaponCoroutine("Hand", "맨손"));
            else
                StartCoroutine(WManager.ChangeWeaponCoroutine("Hand", "맨손"));
        }
        else
        {
            StartCoroutine(WManager.ChangeWeaponCoroutine("Hand", "맨손"));
        }
    } */
}
