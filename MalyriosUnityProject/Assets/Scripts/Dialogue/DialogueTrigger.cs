using UnityEngine;
using TMPro;

namespace Malyrios.Dialogue
{
    public class DialogueTrigger : MonoBehaviour, IInteractable
    {
        [SerializeField] private float talkRadius = 0.2f;
        TextMeshProUGUI interactableText;
        private DialogueManager manager;
        private Dialogue dialogue;
        public static bool triggered;
        private LayerMask whatCanTalkToMe;
        bool turnedButtonOff = false;

        private void Awake()
        {

            //this.manager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            manager = ReferencesManager.instance.dialogueManager;
            //print(man2 +" "+ manager);
            this.dialogue = GetComponent<Dialogue>();
            interactableText = ReferencesManager.instance.interactableText;
        }

        void Start()
        {
            this.whatCanTalkToMe = LayerMask.GetMask("Player");
        }

        void IInteractable.Interact()
        {
            this.manager.StartDialogue(this.dialogue);
        }

        private void Update()
        {
            bool inTalkRange = Physics2D.OverlapCircle(transform.position, this.talkRadius, this.whatCanTalkToMe);
            if (inTalkRange)
            {
                interactableText.text = $"Talk";
                interactableText.gameObject.SetActive(true);
                turnedButtonOff = false;
            }
            else
            {
                //Turn interactable text off only once, not every update
                if (!turnedButtonOff)
                {
                    interactableText.gameObject.SetActive(false);
                    turnedButtonOff = true;
                    manager.EndDialogue();
                }
            }
            /* if (this.triggered && Player.interactInput && inTalkRange)
            {
                Player.interactInput = false;
                this.manager.StartDialogue(this.dialogue);
               
            } */
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            triggered = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            triggered = false;
            //manager.EndDialogue();
        }
    }
}