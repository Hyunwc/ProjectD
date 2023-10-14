using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float hp = 100; //�÷��̾� ü��
    public Slider hpBar; //�÷��̾��� ü�¹�

    public GameObject diePanel;

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
            //�÷��̾� ��� �ߴ�
            GetComponent<PlayerMove>().enabled = false; //������ ����
            GetComponent<PlayerFire>().enabled = false; //��� ����
            GetComponentInChildren<CameraRotate>().enabled = false;  //ī�޶� ȸ�� ����

            //���� ���� ��� ���� ��� �ߴ�
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach(var enemy in enemies)
            {
                enemy.enabled = false;
            }
        }
    }
    
}
