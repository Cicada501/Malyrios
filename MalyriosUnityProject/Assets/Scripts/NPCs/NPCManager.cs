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
        //wizard.CurrentDialogState = 1;
    }

    public NPCDataList SaveNPCs()
    {
        var npcDataList = new List<NPCData>();

        npcDataList.Add(new NPCData(wizard));
        return new NPCDataList(npcDataList);
    }




    public void LoadNPCs(List<NPCData> npcDataList)
    {
        if (npcDataList.Count == 0)
        {
            wizard.CurrentDialogState = 1;
            print("wizard dialog state set");
        }
        // Lade die Daten jedes NPCs
        foreach (var npcData in npcDataList)
        {
            // Finde das entsprechende NPC-Objekt
            var npc = FindNPCByName(npcData.NPCName);
            if (npc != null)
            {
                if (npcData.CurrentDialogueState > 1)
                {
                    npc.CurrentDialogState = npcData.CurrentDialogueState;
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
