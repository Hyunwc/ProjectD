using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickManager : MonoBehaviour
{
    public GameObject subMenu;
    public GameObject player;
    public GameObject RealQuit;
    private CameraRotate cameraRotate;
    public void ClickStart()
    {
        Time.timeScale = 1;
        // 클릭시 튜토리얼 씬으로 시작
        LoadingSceneContorller.LoadScene("tutorialScene");
        //LoadingSceneContorller.LoadScene("Game");
        cameraRotate = FindObjectOfType<CameraRotate>();
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
    public void TutoReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("tutorialScene");
    }
    
    public void Ch1ReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        subMenu.SetActive(false);
        RealQuit.SetActive(true);
    }
    public void RePause()
    {
        RealQuit.SetActive(false);
        subMenu.SetActive(true);
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
