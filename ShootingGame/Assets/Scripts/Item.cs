using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType // 아이템 유형
    { 
        Used, // 소모품
        Ingredient, // 재료
        ETC, // 기타 
    }

    public string itemName; // 아이템 이름
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템 이미지 (인벤토리)
    public GameObject itemPrefab; // 아이템 프리팹(생성시 프리팹)

}
