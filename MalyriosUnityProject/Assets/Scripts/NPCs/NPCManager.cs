using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NPCs;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Dictionary<string, NPC> npcs = new(); // Dictionary für die aktiven NPCs

    public List<NPCData> allNPCData;


    public NPCDataList SaveNPCs()
    {
        return new NPCDataList(allNPCData);
    }

    private void Update()
    {
        //print(JsonUtility.ToJson(new NPCDataList(allNPCData)));
        foreach (string key in npcs.Keys)
        {
            Debug.Log(key);
        }
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
        print(existingNpc);
        if (existingNpc == null)
        {
            print($"added: {npc.npcName} to allNPCData");
            allNPCData.Add(new NPCData(npc));
            return;
        }
        print($"{npc.npcName} was loaded into the list");
    }




    public void LoadNPCs(List<NPCData> npcDataList)
    {
        
        allNPCData = npcDataList;
        
        print("Loaded NPCs");
        if (npcDataList.Count == 0)
        {
            if (LevelManager.CurrentLevelName == "HighForest")
            {
                npcs["Thrimbald"].CurrentDialogState = 1;
                npcs["Asmilda"].CurrentDialogState = 1;
                npcs["Jack"].CurrentDialogState = 1;
                npcs["Tommy"].CurrentDialogState = 1;
                npcs["Admurin"].CurrentDialogState = 1;
            }
            
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
