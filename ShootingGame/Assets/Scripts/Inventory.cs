using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool invectoryActivated = true;  // �κ��丮 Ȱ��ȭ ����

    [SerializeField]
    private GameObject InventoryBase; // Inventory_Panel �̹���
    [SerializeField]
    private GameObject SlotsParent;  // Grid ����

    private Slot[] slots;  // ���� �迭

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
        if (Input.GetKeyDown(KeyCode.I)) // I Ű�� ������ �� �κ��丮 Ȱ��ȭ 
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

    public void AcquireItem(Item _item, int _count = 1) // ������ ȹ���� ó�� 
    {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null) 
                {
                    if (slots[i].item.itemName == _item.itemName) // ���� ������ �������� �ִ��� ã�� �ش� ������ ���� ������Ʈ
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) // �� ���Կ� ���ο� ������ �߰�
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
