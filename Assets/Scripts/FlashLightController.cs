using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    private Light flashlight;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = false; // 손전등의 불이 꺼져 있는 상태로 시작
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            flashlight.enabled = !flashlight.enabled; // 우클릭 누를 때마다 손전등을 켜고 끔
        }
    }
}