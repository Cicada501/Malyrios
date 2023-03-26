using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Malyrios.Dialogue;
using TMPro;
using UnityEngine;

public class ReferencesManager : MonoBehaviour
{
    
    /* Because it is not effective to use GameObject.Find() very often, this ReferencesManager should help
     * by linking all important References here, so that i can get them with i.e. like this:
     * ReferencesManager.instance.interactableText;
     */
    
    public static ReferencesManager instance;

    public TextMeshProUGUI interactableText;
    public DialogueManager dialogueManager;
    public CinemachineVirtualCamera camera;
    private void Awake()
    {
        // Set the static instance variable to this instance of the script
        instance = this;
    }
}
