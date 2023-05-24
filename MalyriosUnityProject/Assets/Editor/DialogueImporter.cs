using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class DialogueImporter : MonoBehaviour
{
    [MenuItem("Custom/Load Dialogue Text")]
    static void LoadDialogueText()
    {
        // Pfad zur JSON-Datei
        string path = EditorUtility.OpenFilePanel("Load Dialogue Text", "", "json");

        // Stellt sicher, dass die Datei existiert
        if (!File.Exists(path))
        {
            Debug.LogError($"File does not exist: {path}");
            return;
        }

        // Liest den Inhalt der Datei
        string json = File.ReadAllText(path);

        // Deserialisiert den JSON-Inhalt in ein DialogueTextListWrapper-Objekt
        DialogueTextListWrapper wrapper = JsonUtility.FromJson<DialogueTextListWrapper>(json);
        Decision.Instance.hunterDialogTextTest = wrapper.dialogueTexts;
    }
}