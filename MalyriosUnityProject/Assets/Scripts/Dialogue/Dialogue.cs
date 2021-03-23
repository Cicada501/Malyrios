using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private string nameOfNpc = null;
        [SerializeField] private List<DialogueText> dialogueText = null;

        public string NameOfNpc => this.nameOfNpc;
        public List<DialogueText> DialogueText => this.dialogueText;
    }
}