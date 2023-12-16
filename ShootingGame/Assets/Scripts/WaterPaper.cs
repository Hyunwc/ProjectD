using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPaper : MonoBehaviour
{
    public Animator waterAni;
    public bool isUsing = false;
    public Text waterPaperText; //물수건 사용 여부

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
            waterPaperText.text = "젖은 수건\n사용 중";
        }
        else
        {
            waterAni.SetBool("isUse", false);
            waterPaperText.text = "젖은 수건";
        }
    }
}
