using System.Collections;
using System.Collections.Generic;
using NPCs;
using UnityEngine;

public class NPCData
{
    public string NPCName;
    public int CurrentDialogueState;
    public NPCData(NPC npc)
    {
        NPCName = npc.npcName;
        CurrentDialogueState = npc.CurrentDialogState;
    }

}
