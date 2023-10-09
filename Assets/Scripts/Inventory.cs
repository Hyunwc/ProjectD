using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool invectoryActivated = true;  // 인벤토리 활성화 여부

    [SerializeField]
    private GameObject InventoryBase; // Inventory_Panel 이미지
    [SerializeField]
    private GameObject SlotsParent;  // Grid 세팅

    private Slot[] slots;  // 슬롯 배열

    void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        //TryOpenInventory();
    }

    /* private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) // I 키를 눌렀을 때 인벤토리 활성화 
        {
            invectoryActivated = !invectoryActivated;

            if (invectoryActivated)
                OpenInventory();
            else
                CloseInventory();

        }
    } 

    private void OpenInventory()
    {
        InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        InventoryBase.SetActive(false);
    } */

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
}
