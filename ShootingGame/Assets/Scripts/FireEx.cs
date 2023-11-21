using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fireEx;
    public Text capacityText; //��ȭ�� �뷮
    private float capacity = 100f; // �ʱ�뷮
    private PlayerMove playM;

    private void Start()
    {
        fireEx.Stop();
        capacityText.text = "��ȭ��\n���� �뷮\n" + capacity + "%";
        playM = FindObjectOfType<PlayerMove>();
    }
    public void Shot()
    {
        Debug.Log("�ʾƾƾ�");

        fireEx.Play();
        StartCoroutine(StopParticleAfterShot());
    }
    IEnumerator StopParticleAfterShot()
    {
        while (Input.GetMouseButton(0)) // ���콺�� ���� �ִ� ���� ��ٸ��ϴ�.
        {
            yield return null;
            capacity -= 0.001f;
            capacityText.text = "��ȭ��\n���� �뷮\n" + capacity + "%"; //�뷮 ����

            if(capacity <= 0)
            {
                playM.isGun = true;
                playM.isFireEx = false;
                Destroy(gameObject); //��ȭ�� �ı�
                break;
            }
        }
        fireEx.Stop(); // ���콺�� �������� ��ƼŬ�� �����մϴ�.
    }



    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("��ƾƾ� ������");
    //    if (other.CompareTag("Fire")) // �浹�� ������Ʈ�� Fire �±׸� ������ �ִ��� Ȯ���մϴ�.
    //    {
    //        Destroy(other); // �浹�� ������Ʈ�� �ı��մϴ�.
    //    }
    //}

}
