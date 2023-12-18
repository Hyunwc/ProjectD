using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossSet : MonoBehaviour
{
    public GameObject boss;
    public Camera mainCamera;
    public Camera subCamera;

    public LastBoss bossInfo;
    public GameObject bossHP;
    private PlayerMove ply;
    private SoundManager soundManager;
    public bool last;
    private void Start()
    {

        ply = FindObjectOfType<PlayerMove>();
        soundManager = FindObjectOfType<SoundManager>();
        bossInfo.hp = 0;
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundManager.bossSet = true;
            bossHP.SetActive(true);
            boss.SetActive(true);
            bossInfo.canMove = false;

            mainCamera.enabled = false;
            subCamera.enabled = true;
            //StartCoroutine(SwitchCamera(mainCamera, subCamera));
            Debug.Log("메인>서브");

            StartCoroutine(ChargeBossHP());

        }
    }

    IEnumerator ChargeBossHP()
    {
        Debug.Log(bossInfo.hp);
        Debug.Log(bossInfo.hpBar.maxValue);
        while (bossInfo.hp < bossInfo.hpBar.maxValue)
        {

            bossInfo.hp += 10f;
            bossInfo.hpBar.value = bossInfo.hp;
            yield return new WaitForSeconds(0.1f);
        }
        bossInfo.canMove = true;

        subCamera.enabled = false;
        mainCamera.enabled = true;

        //StartCoroutine(SwitchCamera(subCamera, mainCamera));
        Debug.Log("서브 > 메인");
        Destroy(gameObject);
    }

}
