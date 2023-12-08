using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewInventory : MonoBehaviour
{
    [SerializeField] private Image[] images;  // 슬롯 배열
    private int slotnum; // 인벤토리 슬롯 인덱스

    void Start()
    {
        slotnum = 0;
    }



    //public void Putnum() // 1 ~ 0 키를 누르면 슬롯을 선택할 수 있게끔 
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

    public void ChangeSlot(int snum) // 슬롯 교체
    {
        // 모든 이미지를 비활성화
        foreach (Image image in images)
        {
            image.gameObject.SetActive(false);
        }

        // 선택한 슬롯만 활성화
        images[snum].gameObject.SetActive(true);
        slotnum = snum;
    }

    /*
    public void AcquireItem(Item _item, int _count = 1) // 아이템 획득을 처리 
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].item != null)
            {
                if (images[i].item.itemName == _item.itemName) // 같은 종류의 아이템이 있는지 찾고 해당 슬롯의 갯수 업데이트
                {
                    images[i].SetSlotCount(_count);
                    return;
                }
            }
        }
    } */
}
