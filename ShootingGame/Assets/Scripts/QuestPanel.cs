using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public Text Quest1;
     public Text Quest2;
     public Text Quest3;
     public Text Quest4;
    public Text Quest5;

    public GameObject checkbox1;
    public GameObject checkbox2;
     public GameObject checkbox3;
     public GameObject checkbox4;
    public GameObject checkbox5;

    private GameManager gameManager;

    public AudioClip QuestSound;
    private AudioSource Questaudio;

    public ActionController actionController;
    //private AudioSource firebell;
    //private bool firebellPlayed = false;

    public int elepoint =0;

    void Start()
    {
        //QuestText();
        //firebell = GetComponent<AudioSource>();
        Questaudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        actionController = FindObjectOfType<ActionController>();

    }

    void Update()
    {
        QuestText();
    }

    private void QuestText()
    {
        Quest1.text = " ȭ��溸�� ������ ";
        Quest2.text = " ȭ�� �����ϱ� " + Fire.fireDestroy + " / 10 ";
        Quest3.text = " ���� �����ϱ� " + Enemy.enemyDestroy + " / 3 ";
        Quest4.text = " ���������� ž������ �ʱ� ";
        Quest5.text = " ��ȭ�� ȹ���ϱ� ";

        if (gameManager.bellCheck == true && gameManager.isQuest1Complete == false)
        {
            Quest1.text = "<color=#2FFF00>" + " ȭ��溸�� ������ " + "</color>";
            Questaudio.PlayOneShot(QuestSound);
            checkbox1.SetActive(true);
            gameManager.isQuest1Complete = true;
        }
        if (Fire.fireDestroy >= 10 && gameManager.isQuest2Complete == false) // Enemy Destroy 3 ī��Ʈ�Ǹ� üũ
        {
            Quest2.text = "<color=#2FFF00>" + " ȭ�� �����ϱ� 10 / 10 " + "</color>";
            Questaudio.PlayOneShot(QuestSound);
            checkbox2.SetActive(true);
            gameManager.isQuest2Complete = true;
        }
        if (Enemy.enemyDestroy >= 3 && gameManager.isQuest3Complete == false) // Enemy Destroy 3 ī��Ʈ�Ǹ� üũ
        {
            Quest3.text = "<color=#2FFF00>" + " ���� �����ϱ� 3 / 3" + "</color>";
            Questaudio.PlayOneShot(QuestSound);
            checkbox3.SetActive(true);
            gameManager.isQuest3Complete = true;
        }
        if ( elepoint >= 1 && gameManager.isQuest4Complete == false)
        {
            Questaudio.PlayOneShot(QuestSound);
            checkbox4.SetActive(true);
            gameManager.isQuest4Complete = true;
        }
        if (actionController.hitInfo.transform.CompareTag("FireExt") && gameManager.isQuest5Complete == false)
        {
            checkbox5.SetActive(true);
            gameManager.isQuest5Complete = true;
        }
        //if() ȭ��溸�� �Ҹ��� 1�� �︮�� üũ, Fire ���� 10ȸ ī��Ʈ üũ

    }

}
