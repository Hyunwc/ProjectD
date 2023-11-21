using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialSceneContorller : MonoBehaviour
{
    public string doorTag = "door";
    public Text tutorialText; // UI Text 객체를 저장할 변수

    private string initialText = " "; // 초기 텍스트를 저장할 변수

    void Start()
    {
        // 시작 시에 초기 텍스트를 저장
        initialText = tutorialText.text;
    }

    // 적이 죽었을 때 호출되는 함수
    //public void OnEnemyDeath(GameObject enemy)
    //{
    //    // Door 오브젝트를 찾아서 제거
    //    GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
    //    foreach (GameObject door in doors)
    //    {
    //        Destroy(door);
    //    }

    //    // 적 오브젝트를 제거
    //    Destroy(enemy);
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            tutorialText.text = " "; // 텍스트를 빈 문자열로 설정하여 숨깁니다.
            //tutorialText.gameObject.SetActive(false);
        }
    }
}

