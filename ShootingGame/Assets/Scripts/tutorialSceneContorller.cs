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
    private float delay = 3f;
    //플레이어 참조
    private PlayerMove ply;
    public GameObject StartDoor;
    public GameObject lastDoor;
    public int DestroyMonster = 0;
    private void Start()
    {
        ply = FindObjectOfType<PlayerMove>();
        ply.isMove = false;
        ply.isShot = false;
        tutorialText[idx].SetActive(true);

        StartCoroutine(TutorialStart());

    }
    private void Update()
    {
        switch (idx)
        {
            case 2:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    ply.isMove = true;
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
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 11:
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    idx++;
                    ChangeTutorialText();
                }
                break;
            case 13:
                ply.isMove = true;
                ply.isShot = true;
                break;
        }

        if(DestroyMonster == 3)
            lastDoor.SetActive(false);
    }

    IEnumerator TutorialStart()
    {
        while (true)
        {
            Debug.Log("idx = " + idx + "Length = " + tutorialText.Length);
            if (idx == tutorialText.Length - 1)
                break;
            //3초 딜레이 먹히는 idx = 0,1,8,10,12
            // IDX가 2일 때는 W 키를 눌렀을 때 처리를 하므로, 여기서는 처리X
            if (idx == 0 || idx == 1 || idx == 8 || idx == 10 || idx == 12)
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

    void ChangeTutorialText()
    {
        for (int i = 0; i < tutorialText.Length; i++)
        {
            tutorialText[i].SetActive(i == idx);
        }
    }

   
}
