using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPref;
    public float firePower = 10f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AttackPlayer", 2f, 2f);
    }

    void AttackPlayer()
    {

        // �÷��̾ �ٶ󺸵��� ȸ����ŵ�ϴ�.
        //Vector3 direction = (player.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // �ҷ� ���� �� �߻��մϴ�.
        GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);

    }
}
