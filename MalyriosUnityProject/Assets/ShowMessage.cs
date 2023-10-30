using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ShowMessage : MonoBehaviour
{
    public static ShowMessage Instance { get; private set; }
    
    [SerializeField]
    private TextMeshProUGUI messageText;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        messageText.enabled = false;
    }

    public void Say(string message, float time = 3f)
    {
        StartCoroutine(ShowAndHideMessage(message, time));
    }

    private IEnumerator ShowAndHideMessage(string message, float time)
    {
        messageText.text = message; 
        messageText.enabled = true; 

        yield return new WaitForSeconds(time); 

        messageText.enabled = false;
    }
}
