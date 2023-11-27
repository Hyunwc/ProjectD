using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickManager : MonoBehaviour
{
    public GameObject subMenu;
    public GameObject player;
    public void ClickStart()
    {
        Time.timeScale = 1;
        // 클릭시 튜토리얼 씬으로 시작
        //LoadingSceneContorller.LoadScene("tutorialScene");
        LoadingSceneContorller.LoadScene("Game");

    }

    public void ClickContinue()
    {
        Time.timeScale = 1;
        subMenu.SetActive(false);
        Cursor.visible = false; //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서가 게임 화면 못 벗어나게
    }

    public void ClickExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
