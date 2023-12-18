using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C2Quest : MonoBehaviour
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
    private LastBoss last;
    private LastManager lastManager;

    public ActionController actionController;
    public int npcCount = 0;

    //private GameManager gameManager;

    //public AudioClip QuestSound;
    //private AudioSource Questaudio;
    //private AudioSource firebell;
    //private bool firebellPlayed = false;

    bool isQuest1Complete = false;
    bool isQuest2Complete = false;
    bool isQuest3Complete = false;


    void Start()
    {
        //QuestText();
        //firebell = GetComponent<AudioSource>();
        Questaudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        lastManager = FindObjectOfType<LastManager>();
        actionController = FindObjectOfType<ActionController>();
        last = FindObjectOfType<LastBoss>();

    }

    void Update()
    {
        QuestText();
    }

    private void QuestText()
    {
        Quest1.text = " 모든 시민 구출하기 ";
        Quest2.text = " 모든 몬스터 제압하기 ";
        Quest3.text = " 보스 몬스터 제압하기";


        if (npcCount == 0 && isQuest1Complete == false )
        {
            Questaudio.PlayOneShot(QuestSound);
            lastManager.CompleteCount++;
            checkbox1.SetActive(true);
            isQuest1Complete = true;
        }
        if (Enemy.enemyDestroy >= 12 && isQuest2Complete == false)
        {
            Questaudio.PlayOneShot(QuestSound);
            lastManager.CompleteCount++;
            checkbox2.SetActive(true);
            isQuest2Complete = true;
        }
        if (lastManager.lastbossDie == true && isQuest3Complete == false)
        {
            Questaudio.PlayOneShot(QuestSound);
            lastManager.CompleteCount++;
            checkbox3.SetActive(true);
            isQuest3Complete = true;
        }



    }
}
