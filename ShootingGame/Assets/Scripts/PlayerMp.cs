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
    public GameObject Co2Panel;
    void Start()
    {
        mpBar.value = mp;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CO2")) // CO2와 충돌시 
        {
            Co2Panel.SetActive(true);
            StartCoroutine(CO2zone());
        }
        else
        {
            Co2Panel.SetActive(false);
        }
    }

    private IEnumerator CO2zone()
    {
        mp += Time.deltaTime;
        mpBar.value = mp;

        if (mp >= 30 && mp <= 50) // 30 ~ 50 구간 피 감소
        {
            hpBar.value -= Time.deltaTime;
        }
        else if (mp >= 50 && mp <= 70) // 50 ~ 70 구간 피 감소
        {
            hpBar.value -= co2s[1];
            yield return new WaitForSeconds(0.6f);
        }
        else if (mp >= 70 && mp <= 100) // 70 ~ 100 구간 피 감소
        {
            hpBar.value -= co2s[2];
            yield return new WaitForSeconds(0.7f);
        }
    }
}
