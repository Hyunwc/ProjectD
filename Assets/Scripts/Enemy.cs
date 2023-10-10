using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;

    private Rigidbody enemyRb;
    private Transform player;
   // private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        // player = GameObject.Find("Player");
        //플레이어 태그를 찾음
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize(); //정규화

        transform.position += direction * speed * Time.deltaTime;
    //    Vector3 lookDirection =
    //    (player.transform.position - transform.position).normalized;
    //    enemyRb.AddForce(lookDirection * speed);
    }
}
