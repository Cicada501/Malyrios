using UnityEditor;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Malyrios.Dialogue;
using NPCs;

public class DialogueImporterWindow : EditorWindow
{
    private string path = "";
    private NPC selectedNPC = null;

    [MenuItem("Custom/Load Dialogue Text")]
    static void Init()
    {
        DialogueImporterWindow window = (DialogueImporterWindow)EditorWindow.GetWindow(typeof(DialogueImporterWindow));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Load Dialogue Text", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Drag and drop NPC: ");
        selectedNPC = (NPC)EditorGUILayout.ObjectField(selectedNPC, typeof(NPC), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Select JSON File"))
        {
            path = EditorUtility.OpenFilePanel("Load Dialogue Text", "", "json");
        }

        if (!string.IsNullOrEmpty(path))
        {
            GUILayout.Label("Selected File: " + path);

            if (GUILayout.Button("Load Dialogue Text"))
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    DialogueList importedDialogue = new DialogueList();
                    var wrapper = JsonUtility.FromJson<DialogueTextListWrapper>(json);
                    Debug.Log($" first Dialog text: {wrapper.dialogueTexts[0]}");
                    importedDialogue.dialogTexts = wrapper.dialogueTexts;

                    // Make sure an NPC is selected
                    if (selectedNPC != null)
                    {
                        selectedNPC.allDialogs.Add(importedDialogue);
                    }
                    else
                    {
                        Debug.LogError("No NPC selected");
                    }
                }
                else
                {
                    Debug.LogError($"File does not exist: {path}");
                }
            }
        }
    }
}

[Serializable]
public class DialogueTextListWrapper
{
    public List<DialogueText> dialogueTexts;
}
