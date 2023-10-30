using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particlePrefab; //���� ��ƼŬ
    public AudioClip boomClip;
    private AudioSource bulletAudio;

    private void Start()
    {
        bulletAudio = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Damaged", 10);

            // ��ƼŬ �������� �ν��Ͻ�ȭ�մϴ�.
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // ��ƼŬ�� ����� �Ŀ� �ı��մϴ�.
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
            bulletAudio.PlayOneShot(boomClip, 1.0f);
            Destroy(gameObject);

        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            // ��ƼŬ �������� �ν��Ͻ�ȭ�մϴ�.
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // ��ƼŬ�� ����� �Ŀ� �ı��մϴ�.
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }
    }
}
