using System;
using System.Collections;
using System.Collections.Generic;
using NPCs;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    /*#region Singleton

    private static NPCManager _instance;

    public static NPCManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NPCManager>();
                if (_instance == null)
                {
                    Debug.LogError("NPCManager component not found in the scene.");
                }
            }

            return _instance;
        }
    }



    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("Another instance of NPCManager already exists.");
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    #endregion*/
    public NPC healer;
    public NPC hunter;
    public NPC son;
    public NPC wizard;
    public NPC caveRat;


    private void Start()
    {
        print("NPC Manager start called");
        wizard.CurrentDialogState = 1;
    }

    public List<NPCData> SaveNPCs()
    {
        var npcDataList = new List<NPCData>();

        // Speichere die Daten jedes NPCs
        npcDataList.Add(new NPCData(wizard));
        npcDataList.Add(new NPCData(son));
        npcDataList.Add(new NPCData(hunter));
        npcDataList.Add(new NPCData(healer));

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
                npc.CurrentDialogState = npcData.CurrentDialogueState;
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
    
}
