using System.Collections;
using System.Collections.Generic;
using NPCs;
using UnityEngine;
[System.Serializable]
public class NPCData
{
    public string NPCName;
    public int CurrentDialogueState;
    public bool isAggressive;
    public bool isActive;
    public NPCData(NPC npc)
    {
        NPCName = npc.npcName;
        CurrentDialogueState = npc.CurrentDialogState;
        isActive = npc.IsActive;
        isAggressive = npc.IsAggressive;
    }

}
