//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuestPanel : MonoBehaviour
//{
//    [SerializeField] private Text Quest1;
//    [SerializeField] private Text Quest2;
//    [SerializeField] private Text Quest3;

//    [SerializeField] private GameObject checkbox1;
//    [SerializeField] private GameObject checkbox2;
//    [SerializeField] private GameObject checkbox3;

//    private AudioSource firebell;
//    private bool firebellPlayed = false;

//    void Start()
//    {
//        //QuestText();
//        firebell = GetComponent<AudioSource>();

//    }

//    void Update()
//    {
//        QuestText();
//    }

//    private void QuestText()
//    {
//        Quest1.text = " 화재경보기 누르기 ";
//        Quest2.text = " 화재 진압하기 " + Fire.fireDestory + " / 10 ";
//        Quest3.text = " 몬스터 제압하기 " + Enemy.enemyDestroy + " / 3 ";

//        if (!firebellPlayed)
//        {
//            firebellPlayed = true;
//            Quest1.text = "<color=#2FFF00>" + " 화재경보기 누르기 " + "</color>";
//            checkbox1.SetActive(true);
//        }
//        if (Fire.fireDestory >= 10) // Enemy Destroy 3 카운트되면 체크
//        {
//            Quest2.text = "<color=#2FFF00>" + " 화재 진압하기 10 / 10 " + "</color>";
//            checkbox2.SetActive(true);
//        }
//        if (Enemy.enemyDestroy >= 3) // Enemy Destroy 3 카운트되면 체크
//        {
//            Quest3.text = "<color=#2FFF00>" + " 몬스터 제압하기 3 / 3" + "</color>";
//            checkbox3.SetActive(true);
//        }
//        //if() 화재경보기 소리가 1번 울리면 체크, Fire 진압 10회 카운트 체크

//    }

//}
