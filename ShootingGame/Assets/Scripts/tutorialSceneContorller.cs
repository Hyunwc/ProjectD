using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class tutorialSceneContorller : MonoBehaviour
{
    //튜토리얼 패널
    [SerializeField] private GameObject[] tutorialText;

    public int idx = 0;
    private float delay = 5f;
    //플레이어 참조
    private PlayerMove ply;
    private TutoPlayer tply;
    public GameObject StartDoor;
    public GameObject MiddleDoor;
    public GameObject lastDoor;
    public int DestroyMonster = 0;
    public int DestroyFire = 0;

    public AudioClip QuestSound;
    private AudioSource Questaudio;

    public Text timeText;
    private void Start()
    {
        ply = FindObjectOfType<PlayerMove>();
        tply = FindObjectOfType<TutoPlayer>();
        ply.isMove = false;
        ply.isShot = false;
        tply.isMove = false;
        tply.isShot = false;
        tutorialText[idx].SetActive(true);
        Questaudio = GetComponent<AudioSource>();
        StartCoroutine(TutorialStart());
        timeText.text = "00:00";
    }
    private void Update()
    {
        switch (idx)
        {
            case 2:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    tply.isMove = true;
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.S))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    idx++;
                    ChangeTutorialText();
                    StartDoor.SetActive(false);
                }
                break;
            case 9:
                tply.isChange = true;
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 12:
                tply.isChange = true;
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 15:
                tply.isChange = true;
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 17:
                tply.isChange = true;
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 20:
                tply.isMove = true;
                tply.isChange = true;
                MiddleDoor.SetActive(false);
                ChangeTutorialText();
                //ply.isShot = true;
                break;
            case 21:
                tply.isShot = true;
                break;
        }

        if(DestroyMonster == 3)
            lastDoor.SetActive(false);

        timeUp();
    }
    private void timeUp()
    {
        float timeSinceStart = Time.time;

        // 경과 시간을 시간/분으로 변환합니다.
        int minutes = Mathf.FloorToInt(timeSinceStart / 60F);
        int seconds = Mathf.FloorToInt(timeSinceStart - minutes * 60);

        // 변환된 시간을 표시합니다.
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator TutorialStart()
    {
        while (true)
        {
            Debug.Log("idx = " + idx + "Length = " + tutorialText.Length);
            if (idx == tutorialText.Length - 1)
                break;
            //3초 딜레이 먹히는 idx = 0,1,8,10,12
            //New 버전 idx = 0, 1, 8, 10, 11, 13, 14, 16, 18, 19
            // IDX가 2일 때는 W 키를 눌렀을 때 처리를 하므로, 여기서는 처리X
            if (idx == 0 || idx == 1 || idx == 8 || idx == 10 || idx == 11 || idx == 13 || idx == 14 || idx == 16 || idx == 18 || idx == 19)
            {
                if(idx == 10 || idx == 13 || idx == 16 || idx == 18)
                    tply.isChange = false;

                yield return new WaitForSeconds(delay);
                idx++;
                ChangeTutorialText();
            }
            else
            {
                yield return null;
            }
        }

    }

    public void ChangeTutorialText()
    {
        for (int i = 0; i < tutorialText.Length; i++)
        {
            tutorialText[i].SetActive(i == idx);
            Questaudio.PlayOneShot(QuestSound);
        }
    }
}
