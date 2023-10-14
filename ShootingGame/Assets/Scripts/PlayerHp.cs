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
            //플레이어 기능 중단
            GetComponent<PlayerMove>().enabled = false; //움직임 중지
            GetComponent<PlayerFire>().enabled = false; //사격 중지
            GetComponentInChildren<CameraRotate>().enabled = false;  //카메라 회전 중지

            //게임 내의 모든 적의 기능 중단
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach(var enemy in enemies)
            {
                enemy.enabled = false;
            }
        }
    }
    
}
