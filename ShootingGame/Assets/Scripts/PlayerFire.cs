using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //�Ѿ� �߻��ϴ� ��
    public float firePower;
    public Transform fireTransform; //ź�� �߻���ġ

    //�Ѿ� ���� �׸��� ���� ������
    private LineRenderer lineRenderer;
    private float fireDistance = 100f;

    //public Fire fire;
    // Start is called before the first frame update
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        //fire = FindObjectOfType<Fire>();
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
            else if(hit.transform.tag == "Fire")
            {
                Debug.Log("���̾�~");
                Fire hitFire = hit.transform.GetComponent<Fire>();
                if (hitFire != null)
                {
                    hitFire.hitcount++;
                }
            }
            hitPosition = hit.point;
        }

        StartCoroutine(ShotEffect(hitPosition));
    }

    //���η����� ������ �Ѿ˱����� �����ϴ� �ڷ�ƾ
    public IEnumerator ShotEffect(Vector3 hitposition)
    {
        lineRenderer.SetPosition(0, fireTransform.position);
        lineRenderer.SetPosition(1, hitposition);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        lineRenderer.enabled = false;
    }
}
