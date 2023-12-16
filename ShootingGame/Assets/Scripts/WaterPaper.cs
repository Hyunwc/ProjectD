using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPaper : MonoBehaviour
{
    public Animator waterAni;
    public bool isUsing = false;
    public Text waterPaperText; //������ ��� ����

    // Start is called before the first frame update
    void Start()
    {
        waterAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    public void Animate()
    {
        if(isUsing)
        {
            waterAni.SetBool("isUse", true);
            waterPaperText.text = "���� ����\n��� ��";
        }
        else
        {
            waterAni.SetBool("isUse", false);
            waterPaperText.text = "���� ����";
        }
    }
}
