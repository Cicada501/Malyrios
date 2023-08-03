using UnityEditor;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Malyrios.Dialogue;

/*public class DialogueImporterWindow : EditorWindow
{
    private string path = "";
    private int selectedDialogueIndex;
    private string[] dialogueNames;

    [MenuItem("Custom/Load Dialogue Text")]
    static void Init()
    {
        DialogueImporterWindow window = (DialogueImporterWindow)EditorWindow.GetWindow(typeof(DialogueImporterWindow));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Load Dialogue Text", EditorStyles.boldLabel);

        if (GUILayout.Button("Select JSON File"))
        {
            path = EditorUtility.OpenFilePanel("Load Dialogue Text", "", "json");
        }

        if (!string.IsNullOrEmpty(path))
        {
            GUILayout.Label("Selected File: " + path);

            // Get all List<DialogueText> fields in the Decision instance
            var fields = Decision.Instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            dialogueNames = Array.FindAll(fields, field => field.FieldType == typeof(List<DialogueText>)).Select(field => field.Name).ToArray();

            // Show a dropdown menu to select a dialogue
            selectedDialogueIndex = EditorGUILayout.Popup("Select Dialogue", selectedDialogueIndex, dialogueNames);

            // Button to load the JSON content to the selected dialogue
            if (GUILayout.Button("Load Dialogue Text"))
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    DialogueTextListWrapper wrapper = JsonUtility.FromJson<DialogueTextListWrapper>(json);

                    // Set the selected dialogue
                    var selectedDialogue = fields.First(field => field.Name == dialogueNames[selectedDialogueIndex]);
                    selectedDialogue.SetValue(Decision.Instance, wrapper.dialogueTexts);
                }
                else
                {
                    Debug.LogError($"File does not exist: {path}");
                }
            }
        }
    }
}*/
