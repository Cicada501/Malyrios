using System.Collections.Generic;
using Malyrios.Dialogue;
using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "RPG/NPC")]
    public class NPC : ScriptableObject
    {
        public string NpcName { get; set; }
        public int currentDialogueState;
        //public Dialogue CurrentNpcDialog { get; set; }
        public Dictionary<int, Dialogue> DialogueByState { get; set; }
        
    }
}

