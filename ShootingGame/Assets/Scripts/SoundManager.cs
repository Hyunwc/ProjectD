using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource 컴포넌트를 참조
    public AudioClip sound1; // 게임 시작 시 재생할 사운드
    public AudioClip sound2; // 보스 등장 시 재생할 사운드
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
        audioSource.Stop(); // 현재 재생 중인 사운드를 중지
        audioSource.clip = clip;
        audioSource.Play();
    }
}
