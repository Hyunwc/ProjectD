using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private Text Quest1;
    [SerializeField] private Text Quest2;
    [SerializeField] private Text Quest3;

    [SerializeField] private GameObject checkbox1;
    [SerializeField] private GameObject checkbox2;
    [SerializeField] private GameObject checkbox3;

    private GameManager gameManager;

    public AudioClip QuestSound;
    private AudioSource Questaudio;
    //private AudioSource firebell;
    //private bool firebellPlayed = false;

    void Start()
    {
        //QuestText();
        //firebell = GetComponent<AudioSource>();
        Questaudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

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
        //if() ȭ��溸�� �Ҹ��� 1�� �︮�� üũ, Fire ���� 10ȸ ī��Ʈ üũ

    }

}
