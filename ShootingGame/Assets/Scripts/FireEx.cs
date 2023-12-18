using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fireExPt;
    public Text capacityText; //��ȭ�� �뷮
    public float capacity = 100f; // �ʱ�뷮
    private PlayerMove playM;

    public AudioSource launchSound; // �߻� �Ҹ�
    private bool isPlayingSound = false;
    [SerializeField] private GameObject FireTrigger;

    //public AudioClip fireExSound;
    //public AudioSource fireExaudio;

    private void Start()
    {
        // fireExaudio = GetComponent<AudioSource>();
        //fireExaudio.clip = fireExSound;
        launchSound = GetComponent<AudioSource>();
        fireExPt.Stop();
        capacityText.text = "��ȭ��\n���� �뷮\n" + capacity + "%";
        playM = FindObjectOfType<PlayerMove>();
 
    }
    
    public void Shot()
    {
 
        fireExPt.Play();
        FireTrigger.SetActive(true);
        //fireExaudio.Play();
        //Debug.Log("Playing sound: " + fireExaudio.isPlaying);
        isPlayingSound = true;
        StartCoroutine(StopParticleAfterShot());

        //if (!isPlayingSound)
        //{
        //    if (launchSound.clip != null)
        //    {
        //        //launchSound.Play();
        //        isPlayingSound = true;
        //    }
        //}

    }
    IEnumerator StopParticleAfterShot()
    {
        bool isMousePressed = true; // ���콺�� ���ȴ��� ���θ� ��Ÿ���� ����

        while (isMousePressed) // ���콺�� ���� ����
        {
            isMousePressed = Input.GetMouseButton(0); // ���콺 ��ư�� �����ִ��� Ȯ��

            if (isMousePressed)
            {
                capacity -= 0.001f;
                capacityText.text = "��ȭ��\n���� �뷮\n" + capacity.ToString("F1") + "%"; //�뷮 ����
                if (!launchSound.isPlaying && capacity > 0)
                {
                    launchSound.Play(); // �Ҹ� ���
                }

                yield return null;
            }
        }

        FireTrigger.SetActive(false);
        fireExPt.Stop(); // ���콺�� �������� ��ƼŬ�� �����մϴ�.

        if (!isMousePressed)
        {
            launchSound.Stop(); // ���콺�� ���� �Ҹ� ����
        }

        if (capacity <= 0)
        {
            playM.isGun = true;
            playM.isFireEx = false;
            capacityText.text = "��ȭ��\n�뷮 ����\n��� �Ұ�";
            // Destroy(gameObject); //��ȭ�� �ı�
        }
    }
}
