using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource ������Ʈ�� ����
    public AudioClip sound1; // ���� ���� �� ����� ����
    public AudioClip sound2; // ���� ���� �� ����� ����
    public bool bossSet = false;
    private bool bossSoundPlayed = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound(sound1);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossSet && !bossSoundPlayed)
        {
            PlaySound(sound2);
            bossSoundPlayed = true;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.Stop(); // ���� ��� ���� ���带 ����
        audioSource.clip = clip;
        audioSource.Play();
    }
}
