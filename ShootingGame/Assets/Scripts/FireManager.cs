using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> fireObjects; // Fire ������Ʈ ����Ʈ
    public float activeRange = 10f; // Ȱ��ȭ �Ÿ�
    // Start is called before the first frame update
    void Update()
    {
        foreach (GameObject fireObject in fireObjects) // ��� Fire ������Ʈ�� ����
        {
            if (fireObject == null) // ������Ʈ�� �ı��Ǿ��ٸ�
            {
                fireObjects.Remove(fireObject); // ����Ʈ���� ����
                continue; // ���� ������Ʈ�� �Ѿ�ϴ�.
            }

            float distance = Vector3.Distance(player.transform.position, fireObject.transform.position); // �Ÿ� ���

            if (distance <= activeRange) // �Ÿ��� activeRange �̳����
            {
                fireObject.SetActive(true); // Fire ������Ʈ Ȱ��ȭ
            }
        }
    }
}
