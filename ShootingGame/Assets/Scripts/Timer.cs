using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float totalTime = 180; // �� ������ �ð� ����
    public GameObject diePanel;

    private bool startTime = false; //���۽ð� bool����

    void Start()
    {
        Invoke("StartTimer", 20f); //�����ϰ� 20�ʵڿ� ��ŸƮ Ÿ�̸� �Լ�����
        //diePanel.SetActive(false);
        
    }
    void Update()
    {
        if(startTime) //���۽ð����� true�϶� ����
        {
            totalTime -= Time.deltaTime; //����ð� ���� �� ������ �ð� ����
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
        startTime = true; //���۽ð����� true��
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
}
