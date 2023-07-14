using System.Collections.Generic;
using Malyrios.Dialogue;
using UnityEngine;

namespace NPCs
{
    [System.Serializable]
    public class DialogueList
    {
        public List<DialogueText> dialogTexts;
    }

    //[CreateAssetMenu(fileName = "New NPC", menuName = "RPG/NPC")]
    public class NPC : MonoBehaviour
    {
        //public GameObject npcGameObject;
        public string npcName;
        public List<DialogueList> allDialogs; // Liste von Dialogen
        public bool isAggressive;
        
        private int currentDialogState;
        private List<DialogueText> currentDialog;
        
        public int CurrentDialogState // Wir definieren eine Eigenschaft, um den aktuellen Dialogstatus zu steuern.
        {
            get 
            {
                return currentDialogState;
            }
            set
            {
                print("wizard dialog was set(1)");
                currentDialogState = value;
                // Sicherstellen, dass der Index innerhalb der gültigen Bereichsgrenzen liegt
                if (currentDialogState >= 0 && currentDialogState <= allDialogs.Count)
                {
                    // Setzen Sie currentDialogText auf den DialogText am Index currentDialogueState.
                    currentDialog = allDialogs[currentDialogState].dialogTexts; // hier nehmen wir an, dass jeder Dialog mindestens einen DialogueText hat.
                    gameObject.GetComponent<Dialogue>().DialogueText = currentDialog;
                    print("wizard dialog was set(2)");
                }
            }
        }
    }
}

