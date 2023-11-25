using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneContorller : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        progressBar.fillAmount = 0f;
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / 2f;
            progressBar.fillAmount = progress;
            yield return null;
        }

        op.allowSceneActivation = true; // �ε��� �Ϸ�Ǹ� ���� ���� ����
    }
}
