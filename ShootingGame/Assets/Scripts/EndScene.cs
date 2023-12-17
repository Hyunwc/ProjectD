using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public float speed = 50f;
    public RectTransform creditsText;
    public float endPosition = 100f;
    [SerializeField] private Text skipText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        creditsText.localPosition += new Vector3(0, speed * Time.deltaTime, 0);

        Debug.Log(creditsText.localPosition.y);

        if (creditsText.localPosition.y >= endPosition)
        {
            SceneManager.LoadScene("Start");
        }
        //조절해도 됩니다
        StartCoroutine(ShowEscapeText(10));
        
    }

    IEnumerator ShowEscapeText(int delay)
    {
        yield return new WaitForSeconds(delay);
        skipText.gameObject.SetActive(true);
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
    }
}
