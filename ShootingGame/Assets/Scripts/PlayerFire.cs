using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //총알 발사하는 힘
    public float firePower;
    public Transform fireTransform; //탄알 발사위치

    //총알 궤적 그리기 위한 렌더러
    private LineRenderer lineRenderer;
    private float fireDistance = 100f;

    public AudioClip shotClip;
    private AudioSource gunAudio;
    // Start is called before the first frame update
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    public void Shot()
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;
        gunAudio.PlayOneShot(shotClip, 1.0f);
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.SendMessage("Damaged", 20);
            }
            hitPosition = hit.point;
        }

        StartCoroutine(ShotEffect(hitPosition));
    }

    //라인렌더러 형태의 총알궤적을 생성하는 코루틴
    public IEnumerator ShotEffect(Vector3 hitposition)
    {
        lineRenderer.SetPosition(0, fireTransform.position);
        lineRenderer.SetPosition(1, hitposition);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        lineRenderer.enabled = false;
    }
}
