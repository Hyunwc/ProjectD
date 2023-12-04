using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemyObjects; // Fire ������Ʈ ����Ʈ
    public float activeRange = 10f; // Ȱ��ȭ �Ÿ�
   
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject fireObject in enemyObjects) // ��� Fire ������Ʈ�� ����
        {
            if (fireObject == null) // ������Ʈ�� �ı��Ǿ��ٸ�
            {
                enemyObjects.Remove(fireObject); // ����Ʈ���� ����
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
