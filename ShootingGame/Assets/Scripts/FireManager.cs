using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> fireObjects; // Fire 오브젝트 리스트
    public float activeRange = 10f; // 활성화 거리
    // Start is called before the first frame update
    void Update()
    {
        foreach (GameObject fireObject in fireObjects) // 모든 Fire 오브젝트에 대해
        {
            if (fireObject == null) // 오브젝트가 파괴되었다면
            {
                fireObjects.Remove(fireObject); // 리스트에서 제거
                continue; // 다음 오브젝트로 넘어갑니다.
            }

            float distance = Vector3.Distance(player.transform.position, fireObject.transform.position); // 거리 계산

            if (distance <= activeRange) // 거리가 activeRange 이내라면
            {
                fireObject.SetActive(true); // Fire 오브젝트 활성화
            }
        }
    }
}
