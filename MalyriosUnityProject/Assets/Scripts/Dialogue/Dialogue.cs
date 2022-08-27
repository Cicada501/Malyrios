using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        public static bool Enabled;
        [SerializeField] private string nameOfNpc = null;
        [SerializeField] private List<DialogueText> dialogueText = null;

        public string NameOfNpc => this.nameOfNpc;
        public List<DialogueText> DialogueText
        {
            get => this.dialogueText;
            set => this.dialogueText = value;
        }
    }

    
}