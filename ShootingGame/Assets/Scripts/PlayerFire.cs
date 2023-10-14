using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //�Ѿ� �������� ��Ƶ� ����
    //public GameObject bulletPref;

    //�Ѿ� �߻��ϴ� ��
    //public float firePower;

    //�� ȿ�� ������
    public GameObject shootEffectPref;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Confined;   //���콺 Ŀ���� ���� ȭ�� �� �����
    }

    // Update is called once per frame
    void Update()
    {
        //���콺 ��Ŭ���� �Ѿ� ������ ����
        if(Input.GetMouseButtonDown(0))
        {
            //ȭ�� �Ѱ������ �����ϴ� ���� ����
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            //Ray�� ���� ��ü�� ��Ƶ� ����
            RaycastHit hit;
            //���̸� �߻��ϰ� ���̿� ���� ��ü�� hit�� ����
            if(Physics.Raycast(ray, out hit))
            {
                //���� ��ġ�� ���� ǥ���� ������ �Ǵ� ������ �� ȿ�� ������ ����
                GameObject shootEffect = Instantiate(shootEffectPref, hit.point + hit.normal* 0.01f, Quaternion.LookRotation(hit.normal));
                //�Ѿ� �ڱ��� ���� ������Ʈ�� �ڽ����� ����
                shootEffect.transform.SetParent(hit.transform);

                //���̿� ���� ��ü�� ���̶��
                if(hit.transform.tag == "Enemy")
                {
                    //������ 10��ŭ ������ �ֶ�� ����
                    hit.transform.SendMessage("Damaged", 10);
                }
            }
            

            //�÷��̾��� ��ġ���� 1 �տ�, ���Ͱ��ƴ� transform�� ������ ȸ���Ҷ��� �ùٸ� ��ġ�� �����ǰ� �ϱ� ����
            //���� �� bullet ������ �Ҵ�
            //GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);

            //�Ѿ� �������� ������ ���ư��� �������� �� �߻�
           // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
        }
    }
}
