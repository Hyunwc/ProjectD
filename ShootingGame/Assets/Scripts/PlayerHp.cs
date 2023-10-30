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

    // Start is called before the first frame update
    void Start()
    {
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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //�÷��̾� ��� �ߴ�
            //GetComponent<PlayerMove>().enabled = false; //������ ����
            //GetComponent<PlayerFire>().enabled = false; //��� ����
            //GetComponentInChildren<CameraRotate>().enabled = false;  //ī�޶� ȸ�� ����

            ////���� ���� ��� ���� ��� �ߴ�
            //Enemy[] enemies = FindObjectsOfType<Enemy>();
            //foreach(var enemy in enemies)
            //{
            //    enemy.enabled = false;
            //}
            Time.timeScale = 0;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Exit"))
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ClearPanel.SetActive(true);
        }
    }

}
