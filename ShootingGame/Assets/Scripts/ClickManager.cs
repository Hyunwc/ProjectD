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
        //Ŭ���� ���Ӿ����� ����
        SceneManager.LoadScene("Game");
    }

    public void ClickContinue()
    {
        subMenu.SetActive(false);
        Cursor.visible = false; //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ���� ���� ȭ�� �� �����
                                                    //�÷��̾� ��� �ߴ�
        player.GetComponent<PlayerMove>().enabled = true; //������ ����
        //GetComponent<PlayerFire>().enabled = true; //��� ����
        player.GetComponentInChildren<CameraRotate>().enabled = true;  //ī�޶� ȸ�� ����

        //���� ���� ��� ���� ��� �ߴ�
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.enabled = true;
        }
    }

    public void ClickExit()
    {
        SceneManager.LoadScene("Start");
    }
}
