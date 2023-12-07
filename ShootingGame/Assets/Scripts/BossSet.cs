using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSet : MonoBehaviour
{
    public GameObject boss;
    public Camera mainCamera;
    public Camera subCamera;

    public Boss bossInfo;

    private PlayerMove ply;

    private void Start()
    {
        ply = FindObjectOfType<PlayerMove>();
        bossInfo.hp = 0;
    }

 
    IEnumerator SwitchCamera(Camera fromCamera, Camera toCamera)
    {
        float transitionTime = 3.0f;
        float elapsedTime = 0;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;

            fromCamera.transform.position = Vector3.Lerp(fromCamera.transform.position, toCamera.transform.position, elapsedTime / transitionTime);
            fromCamera.transform.rotation = Quaternion.Lerp(fromCamera.transform.rotation, toCamera.transform.rotation, elapsedTime / transitionTime);

            yield return null;
        }

  
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Can move before: " + bossInfo.canMove);
            boss.SetActive(true);
            bossInfo.canMove = false;
            Debug.Log("Can move after: " + bossInfo.canMove);

            StartCoroutine(SwitchCamera(mainCamera, subCamera));

    
            StartCoroutine(ChargeBossHP());
            Debug.Log("Coroutine started!");
            
        }
    }

    IEnumerator ChargeBossHP()
    {
        Debug.Log(bossInfo.hp);
        Debug.Log(bossInfo.hpBar.maxValue);
        while (bossInfo.hp < bossInfo.hpBar.maxValue)
        {
            Debug.Log("니가 문제냐?");
            bossInfo.hp += 10f;
            bossInfo.hpBar.value = bossInfo.hp;
            yield return new WaitForSeconds(0.1f);
        }
        bossInfo.canMove = true;
        
        StartCoroutine(SwitchCamera(subCamera, mainCamera));
        
        Destroy(gameObject);
    }

}
