using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public int hitcount = 0; //���� �Ѿ� ��
    private float damage = 0.05f; //������ ��

    static public int fireDestory = 0;

    private void Update()
    {
        if(hitcount == 3)
        {
            fireDestory++;
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


    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water")) // �浹�� ������Ʈ�� Fire �±׸� ������ �ִ��� Ȯ���մϴ�.
        {
            fireDestory++;
            Destroy(gameObject); // �浹�� ������Ʈ�� �ı��մϴ�.
        }
    }
}
