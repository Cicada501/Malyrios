using System.Collections;
using System.Collections.Generic;
using NPCs;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPC Healer { get; set; }
    public NPC Hunter { get; set; }
    public NPC Son { get; set; }
    public NPC Wizard { get; set; }
    
    public List<NPCData> SaveNPCs()
    {
        var npcDataList = new List<NPCData>();

        // Speichere die Daten jedes NPCs
        npcDataList.Add(new NPCData(Wizard));
        npcDataList.Add(new NPCData(Son));
        npcDataList.Add(new NPCData(Hunter));
        npcDataList.Add(new NPCData(Healer));

        return npcDataList;
    }



    public void LoadNPCs(List<NPCData> npcDataList)
    {
        // Lade die Daten jedes NPCs
        foreach (var npcData in npcDataList)
        {
            // Finde das entsprechende NPC-Objekt
            var npc = FindNPCByName(npcData.NPCName);
            if (npc != null)
            {
                // Aktualisiere das NPC-Objekt mit den geladenen Daten
                npc.currentDialogueState = npcData.CurrentDialogueState;
                // Weitere Eigenschaften...
            }
        }
    }

    private NPC FindNPCByName(string name)
    {
        // Gebe das entsprechende NPC-Objekt zurück, basierend auf dem Namen
        switch (name)
        {
            case "Thrimbald":
                return Wizard;
            case "Tommy":
                return Son;
            case "Jack":
                return Hunter;
            case "Asmilda":
                return Healer;
            default:
                return null;
        }
    }
    
}
