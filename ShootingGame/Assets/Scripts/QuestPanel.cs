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
//        Quest1.text = " ȭ��溸�� ������ ";
//        Quest2.text = " ȭ�� �����ϱ� " + Fire.fireDestory + " / 10 ";
//        Quest3.text = " ���� �����ϱ� " + Enemy.enemyDestroy + " / 3 ";

//        if (!firebellPlayed)
//        {
//            firebellPlayed = true;
//            Quest1.text = "<color=#2FFF00>" + " ȭ��溸�� ������ " + "</color>";
//            checkbox1.SetActive(true);
//        }
//        if (Fire.fireDestory >= 10) // Enemy Destroy 3 ī��Ʈ�Ǹ� üũ
//        {
//            Quest2.text = "<color=#2FFF00>" + " ȭ�� �����ϱ� 10 / 10 " + "</color>";
//            checkbox2.SetActive(true);
//        }
//        if (Enemy.enemyDestroy >= 3) // Enemy Destroy 3 ī��Ʈ�Ǹ� üũ
//        {
//            Quest3.text = "<color=#2FFF00>" + " ���� �����ϱ� 3 / 3" + "</color>";
//            checkbox3.SetActive(true);
//        }
//        //if() ȭ��溸�� �Ҹ��� 1�� �︮�� üũ, Fire ���� 10ȸ ī��Ʈ üũ

//    }

//}
