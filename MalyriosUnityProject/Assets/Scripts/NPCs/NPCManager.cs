using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NPCs;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    //Loaded in High Forest
    public NPC healer;
    public NPC hunter;
    public NPC son;
    public NPC wizard;
    //Loaded in Cave
    public NPC caveRat;

    public List<NPCData> allNPCData;


    public NPCDataList SaveNPCs()
    {

        return new NPCDataList(allNPCData);
    }

    private void Update()
    {
        print(JsonUtility.ToJson(new NPCDataList(allNPCData)));
    }

    public void UpdateNPCData(NPC npc)
    {
        // Suche nach dem NPC in der Liste
        var npcData = allNPCData.FirstOrDefault(n => n.NPCName == npc.npcName);
        if (npcData != null)
        {
            // Wenn der NPC in der Liste gefunden wurde, aktualisiere seine Daten
            npcData.isActive = npc.IsActive;
            npcData.isAggressive = npc.IsAggressive;
            npcData.CurrentDialogueState = npc.CurrentDialogState;
        }
        else
        {
            // Wenn der NPC nicht in der Liste gefunden wurde, füge ihn hinzu
            allNPCData.Add(new NPCData(npc));
        }
    }
    
    public void AddNpc(NPC npc)
    {
        var existingNpc = allNPCData.FirstOrDefault(n => n.NPCName == npc.npcName);
        if (existingNpc == null)
        {
            // Füge den NPC zur Liste hinzu, wenn er noch nicht existiert
            allNPCData.Add(new NPCData(npc));
        }
    }




    public void LoadNPCs(List<NPCData> npcDataList)
    {
        if (npcDataList.Count == 0)
        {
            wizard.CurrentDialogState = 1;
            healer.CurrentDialogState = 1;
            hunter.CurrentDialogState = 1;
            son.CurrentDialogState = 1;
            print(" dialog states set");
        }
        // Lade die Daten jedes NPCs
        foreach (var npcData in npcDataList)
        {
            print(npcData.NPCName + npcData.CurrentDialogueState);
            // Finde das entsprechende NPC-Objekt
            var npc = FindNPCByName(npcData.NPCName);
            if (npc != null)
            { 
                npc.IsActive = npcData.isActive;
                npc.IsAggressive = npcData.isAggressive;
                if (npcData.CurrentDialogueState > 0)
                {
                    npc.CurrentDialogState = npcData.CurrentDialogueState;
                    
                    //print($"{npc.npcName} dialog state set to {npcData.CurrentDialogueState}");
                }
                else
                {
                    npc.CurrentDialogState = 1;
                }
            }
        }
    }

    private NPC FindNPCByName(string name)
    {
        // Gebe das entsprechende NPC-Objekt zurück, basierend auf dem Namen
        switch (name)
        {
            case "Thrimbald":
                return wizard;
            case "Tommy":
                return son;
            case "Jack":
                return hunter;
            case "Asmilda":
                return healer;
            default:
                return null;
        }
    }
    
    [System.Serializable]
    public class NPCDataList //wrapper klasse, da JsonUtlility keine Listen direkt speichern kann
    {
        public List<NPCData> npcData;

        public NPCDataList(List<NPCData> data)
        {
            npcData = data;
        }
    }
    
}
