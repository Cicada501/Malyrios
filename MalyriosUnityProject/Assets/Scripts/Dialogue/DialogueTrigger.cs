using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private float triggerRadius = 0.2f;
        [SerializeField] private LayerMask layerMask;

        private DialogueManager manager;
        private Dialogue text;
        private int triggerable;

        private void Start()
        {
            this.manager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            this.text = GetComponent<Dialogue>();
        }

        private void Update()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, this.triggerRadius, this.layerMask);

            if (colliders.Length != 0)
            {
                if (Player.interactInput)
                {
                    this.manager.StartDialogue(this.text);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, this.triggerRadius);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (Player.interactInput)
            {
                this.manager.StartDialogue(this.text);
            }
        }
    }
}

