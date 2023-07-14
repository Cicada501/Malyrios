using Malyrios.Dialogue;
using UnityEngine;

namespace NPCs
{
    public class NPC : MonoBehaviour
    {
        public string NpcName { get; set; }
        public int CurrentNpcState { get; set; }
        public Dialogue CurrentNpcDialog { get; set; }
        
        private Dialogue[] dialogues;
    
        public void UpdateDialogAndState(int State, int DialogID)
        {
            //Set CurrentNpcDialog so that it fits to CurrentNpcState
    
        }
    }
}

