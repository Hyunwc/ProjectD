using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class ItemPickUp : MonoBehaviour
{
    public Item item; // cs 에셋 할당용
    private AudioSource itemAudio; // 아이템의 AudioSource 컴포넌트

    void Start()
    {
        Debug.Log("ItemPickUp 스크립트의 Start 메서드 호출");
        itemAudio = GetComponent<AudioSource>(); // 아이템의 AudioSource 가져오기
    }

    private void PlayItemSound()
    {
        Debug.Log("PlayItemSound() 호출");

        if (itemAudio != null && item.itemType == ItemType.AudioItem)
        {
            Debug.Log("itemAudio 및 ItemType.AudioItem 확인");
            AudioClip itemSound = item.itemSound;

            if (itemSound != null)
            {
                Debug.Log("itemSound 확인");
                itemAudio.clip = itemSound;
                itemAudio.Play();
            }
            else
            {
                Debug.LogError("아이템 소리를 찾을 수 없습니다.");
            }
        }
    }
}
