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
    [SerializeField] private Text[] texts;
    private int randomIndex;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        progressBar.fillAmount = 0f;
        randomIndex = Random.Range(0, texts.Length);
        texts[randomIndex].gameObject.SetActive(true);
        StartCoroutine(LoadSceneProcess());
        Enemy.enemyDestroy = 0;
        Fire.fireDestroy = 0;
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / 4f;
            progressBar.fillAmount = progress;
            yield return null;
        }

        op.allowSceneActivation = true; // 로딩이 완료되면 다음 씬을 시작
    }
}
