using NPCs;

namespace SaveAndLoad
{
    [System.Serializable]
    public class NpcData
    {
        public string npcName;
        public int currentDialogueState;
        public bool isAggressive;
        public bool isActive;
        public NpcData(NPC npc)
        {
            npcName = npc.npcName;
            currentDialogueState = npc.CurrentDialogState;
            isActive = npc.IsActive;
            isAggressive = npc.IsAggressive;
        }

    }
}
