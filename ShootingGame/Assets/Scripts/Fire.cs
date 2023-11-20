using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public int count;
    private float damage = 0.05f; //������ ��
 
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")) //�÷��̾�� �浹������
        {
            //�浹 ������Ʈ�� PlayerHp ��ũ��Ʈ�� ������ �ִٸ�
            PlayerHp playerHp = other.GetComponent<PlayerHp>();
            if(playerHp != null)
            {
              
               playerHp.Damaged(damage);
             
            }
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water")) // �浹�� ������Ʈ�� Fire �±׸� ������ �ִ��� Ȯ���մϴ�.
        {
            Destroy(gameObject); // �浹�� ������Ʈ�� �ı��մϴ�.
        }
    }
}
