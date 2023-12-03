using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medicine : MonoBehaviour
{
    private PlayerHp playerHp;
    public Text mediText;
    public int mediCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = FindObjectOfType<PlayerHp>(); 
    }

    void Update()
    {
        UpdateUI();
    }
    public void UseMedicine()
    {
        playerHp.Heal(20f);
        mediCount--;
    }
    
    void UpdateUI()
    {
        mediText.text = "ȸ�� �˾�\n���� ���� : \n " + mediCount + "��";
    }
}
