using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewTargetMissile : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    private Transform target;

    NavMeshAgent nav;

    public int hitCount = 0;
   
    void Start()
    {
        target = FindObjectOfType<PlayerMove>().transform;
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            //Instantiate flash effect on projectile position
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            //Destroy flash effect depending on particle Duration time
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, 5);
    }
    private void Update()
    {

        nav.SetDestination(target.position);

        if (hitCount == 2)
            Destroy(gameObject);
    }
    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;
            //transform.position += transform.forward * (speed * Time.deltaTime);         
        }
    }

    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    void OnCollisionEnter(Collision collision)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("Damaged", 10);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            // 보스와의 충돌 처리
            // ...
            return;  // 보스와 충돌했을 때는 여기서 메서드를 종료
        }

        // If not collided with Player, Wall, or Ground
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Wall") && !collision.gameObject.CompareTag("Ground"))
        {
            // Skip the rest of the code, do not destroy the missile
            return;
        }

    
        Destroy(gameObject);
    }
}
