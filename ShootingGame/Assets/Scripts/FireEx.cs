using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fireExPt;
    public Text capacityText; //��ȭ�� �뷮
    private float capacity = 100f; // �ʱ�뷮
    private PlayerMove playM;

    private void Start()
    {
        fireExPt.Stop();
        capacityText.text = "��ȭ��\n���� �뷮\n" + capacity + "%";
        playM = FindObjectOfType<PlayerMove>();
    }
    public void Shot()
    {
        fireExPt.Play();
        StartCoroutine(StopParticleAfterShot());
    }
    IEnumerator StopParticleAfterShot()
    {
        while (Input.GetMouseButton(0)) // ���콺�� ���� �ִ� ���� ��ٸ��ϴ�.
        {
            yield return null;
            capacity -= 0.001f;
            capacityText.text = "��ȭ��\n���� �뷮\n" + capacity.ToString("F1") + "%"; //�뷮 ����

            if (capacity <= 0)
            {
                playM.isGun = true;
                playM.isFireEx = false;
                //playM.fireEx.gameObject.SetActive(false);
                capacityText.text = "��ȭ��\n�뷮 ����\n��� �Ұ�";
                // Destroy(gameObject); //��ȭ�� �ı�
                break;
            }
        }

        fireExPt.Stop(); // ���콺�� �������� ��ƼŬ�� �����մϴ�.
    }
}
