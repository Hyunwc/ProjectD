using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class ItemPickUp : MonoBehaviour
{
    public Item item; // cs ���� �Ҵ��
    private AudioSource itemAudio; // �������� AudioSource ������Ʈ

    void Start()
    {
        Debug.Log("ItemPickUp ��ũ��Ʈ�� Start �޼��� ȣ��");
        itemAudio = GetComponent<AudioSource>(); // �������� AudioSource ��������
    }

    private void PlayItemSound()
    {
        Debug.Log("PlayItemSound() ȣ��");

        if (itemAudio != null && item.itemType == ItemType.AudioItem)
        {
            Debug.Log("itemAudio �� ItemType.AudioItem Ȯ��");
            AudioClip itemSound = item.itemSound;

            if (itemSound != null)
            {
                Debug.Log("itemSound Ȯ��");
                itemAudio.clip = itemSound;
                itemAudio.Play();
            }
            else
            {
                Debug.LogError("������ �Ҹ��� ã�� �� �����ϴ�.");
            }
        }
    }
}
