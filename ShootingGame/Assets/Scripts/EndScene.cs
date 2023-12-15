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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        creditsText.localPosition += new Vector3(0, speed * Time.deltaTime, 0);

        if (creditsText.localPosition.y >= endPosition)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
