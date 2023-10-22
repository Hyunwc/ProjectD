using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotateSpeed; //ȸ�� �ӵ�

    //eulerAngles.x�� ���� ��Ƶ� ����
    float tempX;
    
    // Update is called once per frame
    void Update()
    {
        //���콺�� ���Ʒ� ������ �Է��� ���ڷ� �޾Ƽ� ����
        float mouseMoveY = Input.GetAxis("Mouse Y");

        if (Mathf.Approximately(mouseMoveY, 0))
        {
            return;
        }
        
        //���콺�� ������ ��ŭ X�� ȸ��
        transform.Rotate(-mouseMoveY * rotateSpeed * Time.deltaTime, 0, 0);

        //x�� ������ 180�� �Ѵ´ٸ�
        if (transform.eulerAngles.x > 180)
        {
            //360�� ���� ������ ����
            tempX = transform.eulerAngles.x - 360;
        }
        //x�� ������ 180�� ���� �ʴ´ٸ�
        else
        {
            //�״�� ����
            tempX = transform.eulerAngles.x;
        }

        //������ ������ x�� ������ -30~30���� ����
        tempX = Mathf.Clamp(tempX, -45, 45);
        //���ѵ� ���� eulerAngles.x�� ����
        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
