using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fireEx;


    private void Start()
    {
        fireEx.Stop();
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
