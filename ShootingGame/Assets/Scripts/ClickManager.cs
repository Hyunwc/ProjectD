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
        // Ŭ���� Ʃ�丮�� ������ ����
        LoadingSceneContorller.LoadScene("tutorialScene");
        //LoadingSceneContorller.LoadScene("Game");
        cameraRotate = FindObjectOfType<CameraRotate>();
    }

    public void ClickContinue()
    {
        Time.timeScale = 1;
        subMenu.SetActive(false);
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
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
