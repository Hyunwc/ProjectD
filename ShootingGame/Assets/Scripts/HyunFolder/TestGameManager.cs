using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class TestGameManager : MonoBehaviour
{
    public static TestGameManager Instance { get; set; } //ΩÃ±€≈Ê ∆–≈œ

    private PlayerControl m_player;

    


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<PlayerControl>();
   

    }

    // Update is called once per frame
    void Update()
    {
      
    }

}
