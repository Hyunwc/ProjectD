using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LastManager : MonoBehaviour
{
    public bool lastbossDie = false;
    private CameraRotate cameraRotate;
    public GameObject ClearPanel;

    [SerializeField] private GameObject[] gameText;
    [SerializeField] private GameObject gamePanel;

    public int idx = 0;
    private PlayerMove ply;
    private float delay = 3f;

    public GameObject[] goldStar;
    public GameObject[] silverStar;
    public int CompleteCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        cameraRotate = FindObjectOfType<CameraRotate>();
        ply = FindObjectOfType<PlayerMove>();
        ply.isMove = false;
        ply.isShot = false;
        gameText[idx].SetActive(true);
        StartCoroutine(GameScene());
    }

    // Update is called once per frame
    void Update()
    {

        if (idx == 1)
        {
            ply.isMove = true;
            ply.isShot = true;
            StartCoroutine(DisablePanelAfterDelay(5f));
        }

        if (lastbossDie)
        { 
            StartCoroutine(ShowVictoryPanelAfterDelay(8.0f));
        }

        ActiveStar();
    }

    IEnumerator GameScene()
    {
        while (true)
        {
            Debug.Log("idx = " + idx + "Length = " + gameText.Length);
            if (idx == gameText.Length - 1)
                break;

            if (idx == 0)
            {
                ply.isMove = false;
                ply.isShot = false;
                yield return new WaitForSeconds(delay);
                idx++;
                ChangeTutorialText();
            }
            else if (idx == 1)
            {
                ply.isMove = true;
                ply.isShot = true;
                //yield return new WaitForSeconds(delay);
                idx++;
                ChangeTutorialText();
            }
            else
            {
                yield return null;
            }
        }
        
    }

    void ActiveStar()
    {
        for (int i = 0; i < CompleteCount; i++)
        {
            silverStar[i].SetActive(false);
            goldStar[i].SetActive(true);
        }
    }

    void ChangeTutorialText()
    {
        for (int i = 0; i < gameText.Length; i++)
        {
            gameText[i].SetActive(i == idx);
        }
    }
    //특정 조건때 원하는 시간 후에 패널을 비활성화 시키는 코루틴
    IEnumerator DisablePanelAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gamePanel.SetActive(false);
    }
    IEnumerator ShowVictoryPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        cameraRotate.isPause = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ClearPanel.SetActive(true);
    }
}
