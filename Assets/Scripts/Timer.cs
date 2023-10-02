using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float totalTime = 180; // 초 단위로 시간 설정
    public GameObject diePanel;

    private bool startTime = false; //시작시간 bool변수

    void Start()
    {
        Invoke("StartTimer", 20f); //시작하고 20초뒤에 스타트 타이머 함수실행
        //diePanel.SetActive(false);
        
    }
    void Update()
    {
        if(startTime) //시작시간변수 true일때 실행
        {
            totalTime -= Time.deltaTime; //현재시간 기준 초 단위로 시간 감소
            if (totalTime <= 0)
            {
                totalTime = 0;
                diePanel.SetActive(true);
            }

            UpdateTimerUI();
        }
    }

    void StartTimer()
    {
        startTime = true; //시작시간변수 true로
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
}
