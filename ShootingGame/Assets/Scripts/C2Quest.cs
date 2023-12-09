using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C2Quest : MonoBehaviour
{
    [SerializeField] private Text Quest1;
  
    [SerializeField] private GameObject checkbox1;
   
    public int NpcCount = 0;
    //private GameManager gameManager;

    //public AudioClip QuestSound;
    //private AudioSource Questaudio;
    //private AudioSource firebell;
    //private bool firebellPlayed = false;

    void Start()
    {
        //QuestText();
        //firebell = GetComponent<AudioSource>();
        //Questaudio = GetComponent<AudioSource>();
        //gameManager = FindObjectOfType<GameManager>();
       
    }

    void Update()
    {
        QuestText();
    }

    private void QuestText()
    {
        Quest1.text = " 시민 3명 구출하기 ";
    
        if (NpcCount == 3)
        {
            Quest1.text = "<color=#2FFF00>" + " 시민 3명 구출하기 " + "</color>";
            
            checkbox1.SetActive(true);
           
        }
       
        //if() 화재경보기 소리가 1번 울리면 체크, Fire 진압 10회 카운트 체크

    }
}
