using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewInventory : MonoBehaviour
{
    [SerializeField] private Image[] images;  // ���� �迭
    private int slotnum; // �κ��丮 ���� �ε���

    void Start()
    {
        slotnum = 0;
    }



    //public void Putnum() // 1 ~ 0 Ű�� ������ ������ ������ �� �ְԲ� 
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //        ChangeSlot(0);
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //        ChangeSlot(1);
    //    else if (Input.GetKeyDown(KeyCode.Alpha3))
    //        ChangeSlot(2);
    //    else if (Input.GetKeyDown(KeyCode.Alpha4))
    //        ChangeSlot(3);
    //}

    public void ChangeSlot(int snum) // ���� ��ü
    {
        // ��� �̹����� ��Ȱ��ȭ
        foreach (Image image in images)
        {
            image.gameObject.SetActive(false);
        }

        // ������ ���Ը� Ȱ��ȭ
        images[snum].gameObject.SetActive(true);
        slotnum = snum;
    }

    /*
    public void AcquireItem(Item _item, int _count = 1) // ������ ȹ���� ó�� 
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].item != null)
            {
                if (images[i].item.itemName == _item.itemName) // ���� ������ �������� �ִ��� ã�� �ش� ������ ���� ������Ʈ
                {
                    images[i].SetSlotCount(_count);
                    return;
                }
            }
        }
    } */
}
