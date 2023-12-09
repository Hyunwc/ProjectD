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

    //public AudioSource launchSound; // �߻� �Ҹ�
    private bool isPlayingSound = false;
    [SerializeField] private GameObject FireTrigger;

    public AudioClip fireExSound;
    public AudioSource fireExaudio;

    private void Start()
    {
        fireExaudio = GetComponent<AudioSource>();
        fireExPt.Stop();
        capacityText.text = "��ȭ��\n���� �뷮\n" + capacity + "%";
        playM = FindObjectOfType<PlayerMove>();
        fireExaudio.clip = fireExSound;
    }
    
    public void Shot()
    {
        fireExPt.Play();
        FireTrigger.SetActive(true);
        fireExaudio.Play();
        Debug.Log("Playing sound: " + fireExaudio.isPlaying);
        isPlayingSound = true;
        StartCoroutine(StopParticleAfterShot());
       
    }
    IEnumerator StopParticleAfterShot()
    {

        while (Input.GetMouseButton(0)) // ���콺�� ���� �ִ� ���� ��ٸ��ϴ�.
        {

            Debug.Log("Mouse button is pressed");
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
        FireTrigger.SetActive(false);
        fireExPt.Stop(); // ���콺�� �������� ��ƼŬ�� �����մϴ�.
        if (!Input.GetMouseButton(0))
        {
            fireExaudio.Stop();
        }
    }
}
