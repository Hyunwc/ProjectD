using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fireEx;


    private void Start()
    {
        fireEx.Stop();
    }
    public void Shot()
    {
        Debug.Log("초아아아");

        fireEx.Play();
        StartCoroutine(StopParticleAfterShot());
    }
    IEnumerator StopParticleAfterShot()
    {
        while (Input.GetMouseButton(0)) // 마우스가 눌려 있는 동안 기다립니다.
        {
            yield return null;
        }
        fireEx.Stop(); // 마우스가 놓여지면 파티클을 정지합니다.
    }



    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("우아아아 차가워");
    //    if (other.CompareTag("Fire")) // 충돌한 오브젝트가 Fire 태그를 가지고 있는지 확인합니다.
    //    {
    //        Destroy(other); // 충돌한 오브젝트를 파괴합니다.
    //    }
    //}

}
