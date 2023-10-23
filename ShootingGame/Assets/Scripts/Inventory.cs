using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool invectoryActivated = true;  // �κ��丮 Ȱ��ȭ ����

    [SerializeField] private GameObject InventoryBase; // Inventory_Panel �̹���
    [SerializeField] private GameObject SlotsParent;  // Grid ����
    [SerializeField] private GameObject SlotCheck; // ���ý� ǥ�õǴ� ���� �̹���

    //[SerializeField] private WeaponManger WManager; 

    private Slot[] slots;  // ���� �迭
    private int slotnum; // �κ��丮 ���� �ε���

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


    private void Putnum() // 1 ~ 0 Ű�� ������ ������ ������ �� �ְԲ� 
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


    private void ChangeSlot(int snum) // ���� ��ü
    {
        SelectSlot(snum);
        //UseETC();
    }

    private void SelectSlot(int snum) // ���õ� �������� �̵�
    {
        slotnum = snum;

        SlotCheck.transform.position = slots[slotnum].transform.position; // ���� ���� �̹����� �̵� ���� 
    }

    /*private void Use() // Ȱ��ȭ�� ������ ������ ���
    {
        if (slots[slotnum].item != null)
        {
            if (slots[slotnum].item.itemType == Item.ItemType.ETC) // ���õ� ���Կ� ���Ⱑ �ִٸ� ���⸦ ���� 
                StartCoroutine(WManager.ChangeWeaponCoroutine(slots[slotnum].item.weaponType, slots[slotnum].item.itemName));
            else if (slots[slotnum].item.itemType == Item.ItemType.Used)
                StartCoroutine(WManager.ChangeWeaponCoroutine("Hand", "�Ǽ�"));
            else
                StartCoroutine(WManager.ChangeWeaponCoroutine("Hand", "�Ǽ�"));
        }
        else
        {
            StartCoroutine(WManager.ChangeWeaponCoroutine("Hand", "�Ǽ�"));
        }
    } */
}
