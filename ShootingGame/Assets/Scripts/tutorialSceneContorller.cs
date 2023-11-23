using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class tutorialSceneContorller : MonoBehaviour
{
    //튜토리얼 패널
    [SerializeField] private GameObject[] tutorialText;
    
    [SerializeField] private int idx = 0;
    private float delay = 3f;
    //플레이어 참조
    private PlayerMove ply;
    private void Start()
    {
        ply = FindObjectOfType<PlayerMove>();
        ply.isMove = false;
        tutorialText[idx].SetActive(true);
       
        StartCoroutine(TutorialStart());
       
    }
    private void Update()
    {
        
    }

    IEnumerator TutorialStart()
    {
        while(true)
        {
            Debug.Log("idx = " + idx + "Length = " + tutorialText.Length);
            if (idx == tutorialText.Length - 1)
                break;
            yield return new WaitForSeconds(delay);
            tutorialText[idx].SetActive(false); 
            idx++;
            tutorialText[idx].SetActive(true);
        }
        
    }
}

