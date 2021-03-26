using UnityEngine;
using TMPro;

namespace Malyrios.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private float talkRadius = 0.2f;
        [SerializeField] TextMeshProUGUI interactableText;
        private DialogueManager manager;
        private Dialogue dialogue;
        private bool triggered;
        private LayerMask whatCanTalkToMe;

        private void Awake()
        {
            
            this.manager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            this.dialogue = GetComponent<Dialogue>();
        }
        void Start()
        {
            this.whatCanTalkToMe = LayerMask.GetMask("Player");
        }

        private void Update()
        {
            bool inTalkRange = Physics2D.OverlapCircle(transform.position, this.talkRadius, this.whatCanTalkToMe);
            if(inTalkRange){
                interactableText.text = $"Talk";
                interactableText.gameObject.SetActive(true);
            }
            if (this.triggered && Player.interactInput)
            {
                Player.interactInput = false;
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

