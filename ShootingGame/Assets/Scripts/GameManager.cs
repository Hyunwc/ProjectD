using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameText;
    [SerializeField] private GameObject gamePanel;
    public int idx = 0;
    private PlayerMove ply;
    private float delay = 3f;

    public bool isQuest1Complete = false;
    public bool isQuest2Complete = false;
    public bool isQuest3Complete = false;
    public bool isQuest4Complete = false;
    public bool isQuest5Complete = false;

  
    public bool bellCheck = false;
    //public Text timeText;

    public GameObject[] goldStar;
    public GameObject[] silverStar;
    public int CompleteCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        ply = FindObjectOfType<PlayerMove>();
        ply.isMove = false;
        ply.isShot = false;
        gameText[idx].SetActive(true);
        StartCoroutine(GameScene());
        //timeText.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        switch (idx)
        {
            case 1:
                {
                    ply.isMove = true;
                    ply.isShot = true;

                    if (bellCheck)
                        idx++;
                    ChangeTutorialText();
                }
                
                break;
            case 2:
                StartCoroutine(DisablePanelAfterDelay(5f));
                break;
        }

        //timeUp();

        ActiveStar();
    }

    void ActiveStar()
    {
        for(int i = 0; i < CompleteCount; i++)
        {
            silverStar[i].SetActive(false);
            goldStar[i].SetActive(true);
        }
    }
    IEnumerator GameScene()
    {
        while (true)
        {
            Debug.Log("idx = " + idx + "Length = " + gameText.Length);
            if (idx == gameText.Length - 1)
                break;

            if (idx == 0 || idx == 2)
            {
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
    //특정 조건때 원하는 시간 후에 패널을 비활성화 시키는 코루틴
    IEnumerator DisablePanelAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gamePanel.SetActive(false); 
    }
    void ChangeTutorialText()
    {
        for (int i = 0; i < gameText.Length; i++)
        {
            gameText[i].SetActive(i == idx);
        }
    }

    //private void timeUp()
    //{
    //    float timeSinceStart = Time.time;

    //    // 경과 시간을 시간/분으로 변환합니다.
    //    int minutes = Mathf.FloorToInt(timeSinceStart / 60F);
    //    int seconds = Mathf.FloorToInt(timeSinceStart - minutes * 60);

    //    // 변환된 시간을 표시합니다.
    //    timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    //}

}
