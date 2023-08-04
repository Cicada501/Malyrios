using NPCs;

namespace SaveAndLoad
{
    [System.Serializable]
    public class NpcData
    {
        public string npcName;
        public int currentDialogueState = 1;
        public bool isAggressive;
        public bool isActive;
        public int questStatus;
        public NpcData(NPC npc)
        {
            npcName = npc.npcName;
            currentDialogueState = npc.CurrentDialogState;
            isActive = npc.IsActive;
            isAggressive = npc.IsAggressive;
            questStatus = npc.CurrentQuestStatus;
        }

    }
}
