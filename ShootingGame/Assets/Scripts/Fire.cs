using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public int hitcount = 0; //���� �Ѿ� ��
    private float damage = 0.1f; //������ ��

    private void Update()
    {
        if(hitcount == 3)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) //�÷��̾�� �浹������
        {
            //�浹 ������Ʈ�� PlayerHp ��ũ��Ʈ�� ������ �ִٸ�
            PlayerHp playerHp = other.GetComponent<PlayerHp>();
            if (playerHp != null)
            {

                playerHp.Damaged(damage);

            }
        }
    }
}
