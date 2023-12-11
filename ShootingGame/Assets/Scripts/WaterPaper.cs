using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPaper : MonoBehaviour
{
    public Animator waterAni;
    public bool isUsing = false;
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
        }
        else
        {
            waterAni.SetBool("isUse", false);
        }
    }
}
