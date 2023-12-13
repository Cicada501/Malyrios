using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsoleUI : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private string log;

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        log += logString + "\n";
        debugText.text = log;
    }
}