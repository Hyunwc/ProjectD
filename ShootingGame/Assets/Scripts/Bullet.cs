using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particlePrefab; //���� ��ƼŬ
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Damaged", 10);

            // ��ƼŬ �������� �ν��Ͻ�ȭ�մϴ�.
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // ��ƼŬ�� ����� �Ŀ� �ı��մϴ�.
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);

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
