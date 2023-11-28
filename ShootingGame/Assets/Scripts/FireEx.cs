using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fireExPt;
    public Text capacityText; //소화기 용량
    private float capacity = 100f; // 초기용량
    private PlayerMove playM;

    public AudioSource launchSound; // 발사 소리
    private bool isPlayingSound = false;

    private void Start()
    {
        fireExPt.Stop();
        capacityText.text = "소화기\n현재 용량\n" + capacity + "%";
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
        while (Input.GetMouseButton(0)) // 마우스가 눌려 있는 동안 기다립니다.
        {
            Debug.Log("Mouse button is pressed");
            yield return null;
            capacity -= 0.001f;
            capacityText.text = "소화기\n현재 용량\n" + capacity.ToString("F1") + "%"; //용량 갱신

            if(capacity <= 0)
            {
                playM.isGun = true;
                playM.isFireEx = false;
                //playM.fireEx.gameObject.SetActive(false);
                capacityText.text = "소화기\n용량 부족\n사용 불가";
                // Destroy(gameObject); //소화기 파괴
                break;
            }
        }

        fireExPt.Stop(); // 마우스가 놓여지면 파티클을 정지합니다.

        if (isPlayingSound)
        {
            launchSound.Stop();
            isPlayingSound = false;
        }
    }
}
