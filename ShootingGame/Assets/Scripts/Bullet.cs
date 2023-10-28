using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particlePrefab; //폭발 파티클
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Damaged", 10);

            // 파티클 프리팹을 인스턴스화합니다.
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // 파티클이 재생된 후에 파괴합니다.
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);

            Destroy(gameObject);

        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            // 파티클 프리팹을 인스턴스화합니다.
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // 파티클이 재생된 후에 파괴합니다.
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }
    }
}
