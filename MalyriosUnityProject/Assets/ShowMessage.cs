using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ShowMessage : MonoBehaviour
{
    // Singleton-Instanz
    public static ShowMessage Instance { get; private set; }

    // Referenz zu TextMeshProUGUI-Element
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
        StopAllCoroutines(); // Stoppt alle laufenden Coroutines, um die Nachricht zurückzusetzen
        StartCoroutine(ShowAndHideMessage(message, time));
    }

    private IEnumerator ShowAndHideMessage(string message, float time)
    {
        messageText.text = message; // Setzt den Nachrichtentext
        messageText.enabled = true; // Zeigt den Text an

        yield return new WaitForSeconds(time); // Wartet die angegebene Zeit

        messageText.enabled = false; // Versteckt den Text wieder
    }
}
