using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource ������Ʈ�� ����
    public AudioClip sound1; // ���� ���� �� ����� ����
    public AudioClip sound2; // ���� ���� �� ����� ����
    public bool bossSet = false;
    //private bool bossSoundPlayed = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMainSound(); // ������ ���� ���� ���� ���
    }


    void Update()
    {
        if (bossSet)
        {
            PlayBossSound(); // ���� ���� �� ���� ���� ���
            bossSet = false;
            //bossSoundPlayed = true;
        }
    }

    // ���� ���� ���
    public void PlayMainSound()
    {
        //audioSource.Stop(); // ���� ��� ���� ���带 ����
        audioSource.clip = sound1; // ���� �������� Ŭ�� ��ü
        audioSource.Play(); // ���
    }

    // ���� ���� ���
    public void PlayBossSound()
    {
        //audioSource.Stop(); // ���� ��� ���� ���带 ����
        audioSource.clip = sound2; // ���� �������� Ŭ�� ��ü
        audioSource.Play(); // ���
    }

}
