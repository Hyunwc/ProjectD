using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�������ΰ�hyun

    //public int damage;
    //public bool isMelee;
    //public Transform target;
    //NavMeshAgent nav;

    //private void Awake()
    //{
    //    nav = GetComponent<NavMeshAgent>();

    //}

    //private void Update()
    //{
    //    nav.SetDestination(target.position);
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Ground")
    //    {
    //        Destroy(gameObject, 3);
    //    }

    //}
    //void OnTriggerEnter(Collider other)
    //{
    //    if(!isMelee && other.gameObject.tag == "Wall")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public ParticleSystem particlePrefab; //���� ��ƼŬ
    public float speed = 10f; // �̻����� �ӵ�
    private Rigidbody rb; // �̻����� Rigidbody ������Ʈ

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� �����ɴϴ�.
    }
    private void Start()
    {
        //particlePrefab.Stop();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse); // �̻����� ���� �������� ���� ���մϴ�.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("Damaged", 10);

            //particlePrefab.Play();
            //Destroy(gameObject);
            StartCoroutine(DestroyAfterParticlePlay());

        }
        else if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {

            //particlePrefab.Play();
            //Destroy(gameObject);
            StartCoroutine(DestroyAfterParticlePlay());
        }
    }

    IEnumerator DestroyAfterParticlePlay()
    {
        Debug.Log("��ƼŬ�� ȣ��� ģ����");
        particlePrefab.Play();

        // ��ƼŬ ����� ���� ������ ��ٸ��ϴ�.
        yield return new WaitForSeconds(particlePrefab.main.duration);

        // ��ƼŬ ����� ���� �Ŀ� ���� ������Ʈ�� �ı��մϴ�.
        Destroy(gameObject);
    }


}
