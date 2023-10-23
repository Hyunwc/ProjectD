using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMp : MonoBehaviour
{
    public float mp = 0;
    public Slider mpBar;
    public Slider hpBar;
    public float[] co2s = { 0.5f, 0.05f, 0.1f };
   

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CO2") // CO2와 충돌시 
        {
            StartCoroutine(CO2zone());
        }
    }

    private IEnumerator CO2zone()
    {
        mp += Time.deltaTime;
        mpBar.value = mp;

        if (mp >= 30 && mp <= 50) // 30 ~ 50 구간 피 감소
        {
            hpBar.value -= Time.deltaTime;
            ///Debug.Log("30");
        }
        else if (mp >= 50 && mp <= 70) // 50 ~ 70 구간 피 감소
        {
            hpBar.value -= co2s[1];
            yield return new WaitForSeconds(0.6f);
            //Debug.Log("50");
        }
        else if (mp >= 70 && mp <= 100) // 70 ~ 100 구간 피 감소
        {
            hpBar.value -= co2s[2];
            yield return new WaitForSeconds(0.7f);
            //Debug.Log("70");
        }
    }
}




/*
public class PlayerMp : MonoBehaviour
{
    public float mp = 0;

    public Slider mpBar;
    public Slider hpBar;

    public float[] co2s = { 1.0f, 0.6f, 0.3f };

    float index = 0;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CO2")
        {
            CO2zone();
        }
    }

    public void CO2zone(float co2s[])
    {
        mp += Time.deltaTime;
        //mp += co2;
        mpBar.value = mp;
        if (mp >= 30)
        {
            hpBar.value -= Time.deltaTime;
            //hp를 1초에 1씩 깎이고, 0.6초 0.3초 간격?
        }
        if (mp >= 50)
        {
            hpBar.value -= co2s[1];
            yield return new WaitForSeconds(co2s[1]);
        }
        if (mp >= 70)
        {
            hpBar.value -= co2s[2];
            yield return new WaitForSeconds(co2s[2]);
        }

    } */
