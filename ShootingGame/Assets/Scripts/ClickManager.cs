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
    public AudioClip clickSound;
    private AudioSource clickaudio;

    private void Start()
    {
        cameraRotate = FindObjectOfType<CameraRotate>();
        clickaudio = GetComponent<AudioSource>();
        //playerMove = FindObjectOfType<PlayerMove>();
    }
    public void ClickStart()
    {
        Time.timeScale = 1;
        // Ŭ���� Ʃ�丮�� ������ ����
        LoadingSceneContorller.LoadScene("tutorialScene");
        clickaudio.PlayOneShot(clickSound);
        //LoadingSceneContorller.LoadScene("Game");
    }

    public void ClickContinue()
    {
        Time.timeScale = 1;
        clickaudio.PlayOneShot(clickSound);
        cameraRotate.isPause = false;
        subMenu.SetActive(false);
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
    }

    public void ClickExit()
    {
        clickaudio.PlayOneShot(clickSound);
        Time.timeScale = 1;
        
        cameraRotate.isPause = false;
        SceneManager.LoadScene("Start");
    }
    public void TutoReStart()
    {
        clickaudio.PlayOneShot(clickSound);
        Time.timeScale = 1;
        cameraRotate.isPause = false;
        SceneManager.LoadScene("tutorialScene");
    }
    
    public void Ch1ReStart()
    {
        clickaudio.PlayOneShot(clickSound);
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        Enemy.enemyDestroy = 0;
        Fire.fireDestroy = 0;
    }
    public void Ch2ReStart()
    {
        clickaudio.PlayOneShot(clickSound);
        Time.timeScale = 1;
        SceneManager.LoadScene("tunnel");
    }
    public void Quit()
    {
        clickaudio.PlayOneShot(clickSound);
        subMenu.SetActive(false);
        RealQuit.SetActive(true);
    }
    public void RePause()
    {
        clickaudio.PlayOneShot(clickSound);
        RealQuit.SetActive(false);
        subMenu.SetActive(true);
    }

    public void GameExit()
    {
        clickaudio.PlayOneShot(clickSound);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
