using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMp : MonoBehaviour
{
    public float mp = 0;
    public Slider mpBar;
    public Slider hpBar;
    private float[] co2s = { 2f, 2.5f, 3f };
    private float[] hps = { 1f, 2f, 3f };
    public GameObject Co2Panel;

    public PlayerHp playerhp;
    void Start()
    {
        mpBar.value = mp;
        playerhp = FindObjectOfType<PlayerHp>();
    }

    void Update()
    {
        playerhp.hp -= Time.deltaTime;
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
            StopCoroutine("CO2zone");
        }
    }

    IEnumerator CO2zone()
    {
        mp += Time.deltaTime;
        mpBar.value = mp;

        playerhp.hpBar.value = playerhp.hp;

        if (mp >= 30 && mp <= 50) // 30 ~ 50 구간 피 감소
        {
            playerhp.hp -= Time.deltaTime;
            hpBar.value -= Time.deltaTime;
            //playerhp.die();
        }
        else if (mp >= 50 && mp <= 70) // 50 ~ 70 구간 피 감소
        {
            playerhp.hp -= Time.deltaTime;
            hpBar.value -= hps[1];
            //playerhp.die();
        }
        else if (mp >= 70 && mp <= 100) // 70 ~ 100 구간 피 감소
        {
            playerhp.hp -= Time.deltaTime;
            hpBar.value -= hps[2];
            //playerhp.die();
        }
        playerhp.die();
        yield return new WaitForSeconds(playerhp.hp);
    }
}
