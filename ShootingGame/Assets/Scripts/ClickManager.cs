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
        //클릭시 게임씬으로 시작
        SceneManager.LoadScene("Game");
    }

    public void ClickContinue()
    {
        Time.timeScale = 1;
        subMenu.SetActive(false);
        Cursor.visible = false; //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서가 게임 화면 못 벗어나게
                                                    //플레이어 기능 중단
        player.GetComponent<PlayerMove>().enabled = true; //움직임 중지
        //GetComponent<PlayerFire>().enabled = true; //사격 중지
        player.GetComponentInChildren<CameraRotate>().enabled = true;  //카메라 회전 중지

        //게임 내의 모든 적의 기능 중단
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.enabled = true;
        }
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
