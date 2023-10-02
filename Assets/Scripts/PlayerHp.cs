using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float hp = 100; //플레이어 체력
    public Slider hpBar; //플레이어의 체력바

    public GameObject diePanel;

    // Start is called before the first frame update
    void Start()
    {
        hpBar.value = hp; //시작 시 체력바에 초기 체력 값 적용
    }

    public void Damaged(float damage)
    {
        //공격 받은 데미지만큼 체력 감소
        hp -= damage;
        //체력바에 체력 표시
        hpBar.value = hp;

        if (hp <= 0)
        {
            diePanel.SetActive(true);
            Debug.Log("사망");
        }
    }
    
}
