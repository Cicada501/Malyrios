using System.Collections;
using System.Collections.Generic;
using NPCs;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    private NPC thrimbald;
    [SerializeField]
    private NPC hunter;
    [SerializeField]
    private NPC son;
    [SerializeField]
    private NPC asmilda;

    // Start is called before the first frame update
    void Start()
    {
        thrimbald.CurrentNpcState = Decision.WizardDialogueState;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
