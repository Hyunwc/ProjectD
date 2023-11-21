using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fire : MonoBehaviour
{
    public int count;
    public float damage = 10; //������ ��
    public float damageInterval = 1.0f; //�������� ������ ����

    private float lastDamageTime = 0f; //���������� �������� ���� �ð�

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")) //�÷��̾�� �浹������
        {
            //�浹 ������Ʈ�� PlayerHp ��ũ��Ʈ�� ������ �ִٸ�
            PlayerHp playerHp = other.GetComponent<PlayerHp>();
            if(playerHp != null)
            {
                //������ ������ �ð��� ���Ͽ�, ���ݺ��� �� ���� ��쿡�� �������� ��������
                if(Time.time - lastDamageTime >= damageInterval)
                {
                    //Damaged �޼ҵ� ȣ���Ͽ� damage(10) ����
                    playerHp.Damaged(damage);
                    lastDamageTime = Time.time;
                }
                
            }
        }
    }
}
