using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public int hitcount = 0; //맞은 총알 수
    private float damage = 0.1f; //데미지 양

    private void Update()
    {
        if(hitcount == 3)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) //플레이어와 충돌했을때
        {
            //충돌 오브젝트가 PlayerHp 스크립트를 가지고 있다면
            PlayerHp playerHp = other.GetComponent<PlayerHp>();
            if (playerHp != null)
            {

                playerHp.Damaged(damage);

            }
        }
    }
}
