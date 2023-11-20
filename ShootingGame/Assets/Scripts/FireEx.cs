using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public ParticleSystem fire, fireEx;
    // Start is called before the first frame update

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
    private void OnParticleCollision(GameObject other)
    {
        int t = other.gameObject.GetComponent<Fire>().count++;
        fire = other.gameObject.GetComponentInChildren<ParticleSystem>();
        //smoke = other.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        var fire_em = fire.emission;
        //var somke_em = smoke.emission;
        fire_em.enabled = true;

        if(t >= 110)
        {
            Debug.Log("!!!");
            fire_em.rateOverTime = Mathf.Lerp(100.0f, 0.0f, t * 5f);
        }
    }
}
