using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHp : MonoBehaviour
{
    public float hp = 100; //플레이어 체력
    public Slider hpBar; //플레이어의 체력바

    public GameObject diePanel;
    public GameObject ClearPanel;

    private CameraRotate cameraRotate;
    // Start is called before the first frame update
    void Start()
    {
        cameraRotate = GetComponent<CameraRotate>();
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
            cameraRotate.isPause = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            Time.timeScale = 0;
            
        }
    }
    public void Heal(float heal)
    {
        hp += heal;
        hpBar.value = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Exit"))
        {
            cameraRotate.isPause = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ClearPanel.SetActive(true);
        }
    }

}
