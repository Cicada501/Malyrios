using System.Collections;
using System.Collections.Generic;
using Malyrios.Dialogue;
using TMPro;
using UnityEngine;

public class ReferencesManager : MonoBehaviour
{
    public static ReferencesManager instance;

    public TextMeshProUGUI interactableText;
    public DialogueManager dialogueManager;
    private void Awake()
    {
        // Set the static instance variable to this instance of the script
        instance = this;
    }
}
