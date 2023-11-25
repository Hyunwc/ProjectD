using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialTrigger : MonoBehaviour
{
    private tutorialSceneContorller t_idx;
    private PlayerMove ply;

    private void Start()
    {
        ply = FindObjectOfType<PlayerMove>();
        t_idx = FindObjectOfType<tutorialSceneContorller>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && t_idx.DestroyMonster < 3)
        {
            t_idx.idx++;
            ply.isMove = false;
            Destroy(gameObject);

        }
        else if(other.CompareTag("Player") && t_idx.DestroyMonster == 3)
        {
            // �÷��̾�� startCube�� ������ �ε� ������ �̵�
            LoadingSceneContorller.LoadScene("Game");
        }
        
    }
}
