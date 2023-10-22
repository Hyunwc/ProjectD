using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //�Ѿ� �������� ��Ƶ� ����
    //public GameObject bulletPref;

    //�Ѿ� �߻��ϴ� ��
    public float firePower;
    public Transform fireTransform; //ź�� �߻���ġ
    //�� ȿ�� ������
    //public GameObject shootEffectPref;
    //�Ѿ� ���� �׸��� ���� ������
    private LineRenderer lineRenderer;
    private float fireDistance = 100f;
    // Start is called before the first frame update
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    public void Shot()
    {
       
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.SendMessage("Damaged", 20);
            }
            hitPosition = hit.point;
        }

        StartCoroutine(ShotEffect(hitPosition));
    }

    //�Ѿ� �߻� ������Ʈ �Լ�

    // Update is called once per frame
    //void Update()
    //{
    //    //���콺 ��Ŭ���� �Ѿ� ������ ����
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        ////ȭ�� �Ѱ������ �����ϴ� ���� ����
    //        //Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
    //        ////Ray�� ���� ��ü�� ��Ƶ� ����
    //        //RaycastHit hit;
    //        ////���̸� �߻��ϰ� ���̿� ���� ��ü�� hit�� ����
    //        //if(Physics.Raycast(ray, out hit))
    //        //{
    //        //    //���� ��ġ�� ���� ǥ���� ������ �Ǵ� ������ �� ȿ�� ������ ����
    //        //    GameObject shootEffect = Instantiate(shootEffectPref, hit.point + hit.normal* 0.01f, Quaternion.LookRotation(hit.normal));
    //        //    //�Ѿ� �ڱ��� ���� ������Ʈ�� �ڽ����� ����
    //        //    shootEffect.transform.SetParent(hit.transform);

    //        //    //���̿� ���� ��ü�� ���̶��
    //        //    if(hit.transform.tag == "Enemy")
    //        //    {
    //        //        //������ 10��ŭ ������ �ֶ�� ����
    //        //        hit.transform.SendMessage("Damaged", 10);
    //        //    }
    //        //}


    //        //�÷��̾��� ��ġ���� 1 �տ�, ���Ͱ��ƴ� transform�� ������ ȸ���Ҷ��� �ùٸ� ��ġ�� �����ǰ� �ϱ� ����
    //        //���� �� bullet ������ �Ҵ�
    //        GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);

    //        //�Ѿ� �������� ������ ���ư��� �������� �� �߻�
    //        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
    //    }
    //}

    public IEnumerator ShotEffect(Vector3 hitposition)
    {
        lineRenderer.SetPosition(0, fireTransform.position);
        lineRenderer.SetPosition(1, hitposition);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        lineRenderer.enabled = false;
    }
}
