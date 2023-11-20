using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public int count;
    private float damage = 0.05f; //데미지 양
 
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")) //플레이어와 충돌했을때
        {
            //충돌 오브젝트가 PlayerHp 스크립트를 가지고 있다면
            PlayerHp playerHp = other.GetComponent<PlayerHp>();
            if(playerHp != null)
            {
              
               playerHp.Damaged(damage);
             
            }
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water")) // 충돌한 오브젝트가 Fire 태그를 가지고 있는지 확인합니다.
        {
            Destroy(gameObject); // 충돌한 오브젝트를 파괴합니다.
        }
    }
}
