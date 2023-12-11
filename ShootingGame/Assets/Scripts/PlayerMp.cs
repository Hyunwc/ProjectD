using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMp : MonoBehaviour
{
    public float mp = 0;
    public Slider mpBar;
    public Slider hpBar;
    private float[] hps = { 1f, 2f, 2.5f };
    public GameObject Co2Panel;
    public bool isWater = false;
    public PlayerHp playerhp;
    void Start()
    {
        mpBar.value = mp;
        playerhp = FindObjectOfType<PlayerHp>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CO2") && !isWater) //CO2와 충돌시 
        {

            Co2Panel.SetActive(true);
            StartCoroutine(CO2zone());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CO2")) // CO2를 벗어날 때
        {
            Co2Panel.SetActive(false);
            StopCoroutine("CO2zone");
        }
    }

    IEnumerator CO2zone()
    {
        mp += Time.deltaTime;
        mpBar.value = mp;

        playerhp.hpBar.value = playerhp.hp;
        playerhp.hp -= Time.deltaTime * 0.5f;

        if (mp >= 30 && mp <= 50) // 30 ~ 50 구간 피 감소
        {
            playerhp.hp -= Time.deltaTime;
        }
        else if (mp >= 50 && mp <= 70) // 50 ~ 70 구간 피 감소
        {
            playerhp.hp -= Time.deltaTime * hps[1];
        }
        else if (mp >= 70) // 70 ~ 100 구간 피 감소
        {
            playerhp.hp -= Time.deltaTime * hps[2];
        }
        playerhp.die();
        yield return new WaitForSeconds(playerhp.hp);
    }
}
