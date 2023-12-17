using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource 컴포넌트를 참조
    public AudioClip sound1; // 게임 시작 시 재생할 사운드
    public AudioClip sound2; // 보스 등장 시 재생할 사운드
    public bool bossSet = false;
    //private bool bossSoundPlayed = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMainSound(); // 시작할 때는 메인 음악 재생
    }


    void Update()
    {
        if (bossSet)
        {
            PlayBossSound(); // 보스 등장 시 보스 음악 재생
            bossSet = false;
            //bossSoundPlayed = true;
        }
    }

    // 메인 음악 재생
    public void PlayMainSound()
    {
        //audioSource.Stop(); // 현재 재생 중인 사운드를 중지
        audioSource.clip = sound1; // 메인 음악으로 클립 교체
        audioSource.Play(); // 재생
    }

    // 보스 음악 재생
    public void PlayBossSound()
    {
        //audioSource.Stop(); // 현재 재생 중인 사운드를 중지
        audioSource.clip = sound2; // 보스 음악으로 클립 교체
        audioSource.Play(); // 재생
    }

}
