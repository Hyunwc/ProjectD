using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public float damage = 10; //데미지 양
    public float damageInterval = 1.0f; //데미지를 입히는 간격

    private float lastDamageTime = 0f; //마지막으로 데미지를 입힌 시간

    ParticleSystem ps;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) //플레이어와 충돌했을때
        {
            //충돌 오브젝트가 PlayerHp 스크립트를 가지고 있다면
            PlayerHp playerHp = other.GetComponent<PlayerHp>();
            if (playerHp != null)
            {
                //마지막 데미지 시간을 비교하여, 간격보다 더 지난 경우에만 데미지를 입히도록
                if (Time.time - lastDamageTime >= damageInterval)
                {
                    //Damaged 메소드 호출하여 damage(10) 전달
                    playerHp.Damaged(damage);
                    lastDamageTime = Time.time;
                }

            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log($"Effect Collision : {other.name}");
            //공격 받은 데미지만큼 체력 감소
            Destroy(gameObject);
        }
    }
}
