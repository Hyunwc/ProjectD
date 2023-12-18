using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fireExPt;
    public Text capacityText; //소화기 용량
    public float capacity = 100f; // 초기용량
    private PlayerMove playM;

    public AudioSource launchSound; // 발사 소리
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
        capacityText.text = "소화기\n현재 용량\n" + capacity + "%";
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
        bool isMousePressed = true; // 마우스가 눌렸는지 여부를 나타내는 변수

        while (isMousePressed) // 마우스가 눌린 동안
        {
            isMousePressed = Input.GetMouseButton(0); // 마우스 버튼이 눌려있는지 확인

            if (isMousePressed)
            {
                capacity -= 0.001f;
                capacityText.text = "소화기\n현재 용량\n" + capacity.ToString("F1") + "%"; //용량 갱신
                if (!launchSound.isPlaying && capacity > 0)
                {
                    launchSound.Play(); // 소리 재생
                }

                yield return null;
            }
        }

        FireTrigger.SetActive(false);
        fireExPt.Stop(); // 마우스가 놓여지면 파티클을 정지합니다.

        if (!isMousePressed)
        {
            launchSound.Stop(); // 마우스를 떼면 소리 정지
        }

        if (capacity <= 0)
        {
            playM.isGun = true;
            playM.isFireEx = false;
            capacityText.text = "소화기\n용량 부족\n사용 불가";
            // Destroy(gameObject); //소화기 파괴
        }
    }
}
