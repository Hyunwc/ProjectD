using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialSceneContorller : MonoBehaviour
{
    public string doorTag = "door";
    public Text tutorialText; // UI Text ��ü�� ������ ����

    private string initialText = " "; // �ʱ� �ؽ�Ʈ�� ������ ����

    void Start()
    {
        // ���� �ÿ� �ʱ� �ؽ�Ʈ�� ����
        initialText = tutorialText.text;
    }

    // ���� �׾��� �� ȣ��Ǵ� �Լ�
    //public void OnEnemyDeath(GameObject enemy)
    //{
    //    // Door ������Ʈ�� ã�Ƽ� ����
    //    GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
    //    foreach (GameObject door in doors)
    //    {
    //        Destroy(door);
    //    }

    //    // �� ������Ʈ�� ����
    //    Destroy(enemy);
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            tutorialText.text = " "; // �ؽ�Ʈ�� �� ���ڿ��� �����Ͽ� ����ϴ�.
            //tutorialText.gameObject.SetActive(false);
        }
    }
}

