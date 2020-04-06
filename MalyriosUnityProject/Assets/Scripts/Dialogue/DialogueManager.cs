using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

namespace Malyrios.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {

        #region Serialize Fields

        [SerializeField] private GameObject answerButton;
        [SerializeField] private Transform content;
        [SerializeField] private TextMeshProUGUI npcName;
        [SerializeField] private TextMeshProUGUI sentence;

        #endregion

        #region Private Variables

        private Dialogue dialogueText;
        private Queue<string> sentenceQueue;
        private int linkedId;
        private bool dialogueTriggered;

        #endregion

        private void Start()
        {
            this.sentenceQueue = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogueText)
        {
            if (dialogueTriggered) return;
            this.dialogueTriggered = true;
            this.dialogueText = dialogueText;
            this.sentenceQueue.Clear();

            GetNextSentences(0); // Always start with 0 this is the entry point of a dialogue.
            SetNpcName();
        }

        private void GetNextSentences(int linkedId)
        {
            this.sentenceQueue.Clear();
            this.linkedId = linkedId;
            DialogueText dialogueText = this.dialogueText.DialogueText.SingleOrDefault(x => x.Id == linkedId);
            foreach (string sentence in dialogueText.Sentences)
            {
                this.sentenceQueue.Enqueue(sentence);
            }
            NextSentence();
        }

        private void SetNpcName()
        {
            this.npcName.text = this.dialogueText.NameOfNpc;
        }

        public void NextSentence()
        {
            StartCoroutine(WriteSentence(this.sentenceQueue.Peek()));
            this.sentenceQueue.Dequeue();

            if (this.sentenceQueue.Count == 0)
            {
                ShowAnswers();
            }
        }

        private void ShowAnswers()
        {
            DialogueText dialogueText = this.dialogueText.DialogueText.SingleOrDefault(x => x.Id == this.linkedId);
            if (dialogueText.Answers.Count == 0) return;
            foreach (DialogueAnswers answer in dialogueText.Answers)
            {
                GameObject go = Instantiate(this.answerButton, this.content);
                go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = answer.AnswerDescription;
                go.GetComponent<Button>().onClick.AddListener(() => GetNextSentences(answer.LinksToId));
            }
        }

        private IEnumerator WriteSentence(string sentence)
        {
            this.sentence.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                this.sentence.text += letter;
                yield return null;
            }
        }
    }
}

