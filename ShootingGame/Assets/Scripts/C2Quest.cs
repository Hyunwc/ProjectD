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
        Quest1.text = " �ù� 3�� �����ϱ� ";
    
        if (NpcCount == 3)
        {
            Quest1.text = "<color=#2FFF00>" + " �ù� 3�� �����ϱ� " + "</color>";
            
            checkbox1.SetActive(true);
           
        }
       
        //if() ȭ��溸�� �Ҹ��� 1�� �︮�� üũ, Fire ���� 10ȸ ī��Ʈ üũ

    }
}
