using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHp : MonoBehaviour
{
    public float hp = 100; //�÷��̾� ü��
    public Slider hpBar; //�÷��̾��� ü�¹�

    public GameObject diePanel;
    public GameObject ClearPanel;

    private CameraRotate cameraRotate;
    // Start is called before the first frame update
    void Start()
    {
        cameraRotate = GetComponent<CameraRotate>();
        hpBar.value = hp; //���� �� ü�¹ٿ� �ʱ� ü�� �� ����
    }

    public void Damaged(float damage)
    {
        //���� ���� ��������ŭ ü�� ����
        hp -= damage;
        //ü�¹ٿ� ü�� ǥ��
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
