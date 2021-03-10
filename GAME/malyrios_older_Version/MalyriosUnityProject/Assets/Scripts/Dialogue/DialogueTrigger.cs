using UnityEngine;

namespace Malyrios.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        private DialogueManager manager;
        private Dialogue dialogue;
        private bool triggered;

        private void Awake()
        {
            this.manager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            this.dialogue = GetComponent<Dialogue>();
        }

        private void Update()
        {
            if (this.triggered && Input.GetKeyDown(KeyCode.E))
            {
                this.manager.StartDialogue(this.dialogue);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            this.triggered = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            this.triggered = false;
        }
    }
}

