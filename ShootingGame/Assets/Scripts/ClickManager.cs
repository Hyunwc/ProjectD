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
        // Ŭ���� Ʃ�丮�� ������ ����
        //LoadingSceneContorller.LoadScene("tutorialScene");
        LoadingSceneContorller.LoadScene("Game");

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

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
