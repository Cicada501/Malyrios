using System.Collections.Generic;
using System.Linq;
using SaveAndLoad;
using UnityEngine;

namespace NPCs
{
    public class NPCManager : MonoBehaviour
    {
        public Dictionary<string, NPC> npcs = new(); // all NPCs in the current level (with GameObjects)

        public List<NpcData> allNpcData; //Data off all NPCs that the player has seen so far (seen = was in the level, that contains them)


        public NpcDataList SaveNpCs()
        {
            return new NpcDataList(allNpcData);
        }

        public void UpdateNpcData(NPC npc) //when a npc changes, this method is used to make sure the npcData list is updated respectively
        {
            // find npcData that represents this npc (this variable npcData is a link to the object in the list. Therefore changing it also changes the list allNpcData automatically)
            var npcData = allNpcData.FirstOrDefault(n => n.npcName == npc.npcName);
            if (npcData != null)
            {
                //if available, update the npcs properties
                npcData.isActive = npc.IsActive;
                npcData.isAggressive = npc.IsAggressive;
                npcData.currentDialogueState = npc.CurrentDialogState;
            }
            else
            {
                Debug.LogError("NPC not found"); //this case should never happen, because the npcs get add to the allNpcData list in the LoadNpCs method or the AddNpc method (if never saved before)
                allNpcData.Add(new NpcData(npc));
            }
        }
        /// <summary>
        /// Adds the NPC to the allNpcData list, to make sure it gets saved correctly
        /// </summary>
        /// <param name="npc"></param>
        public void AddNpc(NPC npc)
        {
            var existingNpc = allNpcData.FirstOrDefault(n => n.npcName == npc.npcName);
            if (existingNpc == null)
            {
                allNpcData.Add(new NpcData(npc));
            }
        }




        public void LoadNpCs(List<NpcData> npcDataList)
        {
            allNpcData = npcDataList;
        
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
        public class NpcDataList //wrapper klasse, da JsonUtlility keine Listen direkt speichern kann
        {
            public List<NpcData> npcData;

            public NpcDataList(List<NpcData> data)
            {
                npcData = data;
            }
        }
    
    }
}
