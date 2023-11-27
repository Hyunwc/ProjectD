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

    public AudioSource launchSound; // �߻� �Ҹ�
    private bool isPlayingSound = false;

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

        if (!isPlayingSound)
        {
            if (launchSound.clip != null)
            {
                launchSound.Play();
                isPlayingSound = true;
            }
        }

    }
    IEnumerator StopParticleAfterShot()
    {
        while (Input.GetMouseButton(0)) // ���콺�� ���� �ִ� ���� ��ٸ��ϴ�.
        {
            Debug.Log("Mouse button is pressed");
            yield return null;
            capacity -= 0.001f;
            capacityText.text = "��ȭ��\n���� �뷮\n" + capacity + "%"; //�뷮 ����

            if(capacity <= 0)
            {
                playM.isGun = true;
                playM.isFireEx = false;
                playM.fireEx.gameObject.SetActive(false);
                // Destroy(gameObject); //��ȭ�� �ı�
                break;
            }
        }

        fireExPt.Stop(); // ���콺�� �������� ��ƼŬ�� �����մϴ�.

        if (isPlayingSound)
        {
            launchSound.Stop();
            isPlayingSound = false;
        }
    }
}
