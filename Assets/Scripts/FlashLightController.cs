using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    private Light flashlight;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = false; // �������� ���� ���� �ִ� ���·� ����
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            flashlight.enabled = !flashlight.enabled; // ��Ŭ�� ���� ������ �������� �Ѱ� ��
        }
    }
}