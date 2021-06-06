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

        [SerializeField] private float writingSpeed = 15f;
        [SerializeField] private GameObject answerButton = null;
        [SerializeField] private Transform content = null;
        [SerializeField] private TextMeshProUGUI sentence = null;


        #endregion

        #region Private Variables

        private Dialogue dialogueText;
        private Queue<string> sentenceQueue;
        private int linkedId;
        #endregion

        private void Start()
        {
            this.sentenceQueue = new Queue<string>();
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// Start the dialogue.
        /// It's very important to start always with 0 as sentence id.
        /// Otherwise the complete dialog may break down.
        /// </summary>
        /// <param name="dialogueText"></param>
        public void StartDialogue(Dialogue dialogueText)
        {
            this.gameObject.SetActive(true);

            this.dialogueText = dialogueText;
            this.sentenceQueue.Clear();
            this.linkedId = 0;
            GetNextSentences(0); // Always start with 0 this is the entry point of a dialogue.
        }

        /// <summary>
        /// Search the sentence by the linked id and add it to the queue.
        /// Before showing the next sentence delete all old answers.
        /// </summary>
        /// <param name="linkedId">The id of the sentence</param>
        private void GetNextSentences(int linkedId)
        {
            // If the answer linked to a -1 close the dialogue window.
            // And do nothing.
            if (linkedId == -1)
            {
                this.gameObject.SetActive(false);
                return;
            }

            this.sentenceQueue.Clear();
            this.linkedId = linkedId;
            DialogueText dialogueText = this.dialogueText.DialogueText.SingleOrDefault(x => x.SentenceId == linkedId);
            foreach (string sentence in dialogueText.Sentences)
            {
                this.sentenceQueue.Enqueue(sentence);
            }
            DeleteOldAnswers();
            NextSentence();
        }

        /// <summary>
        /// Start the coroutine of the "animated" text and dequeue the sentence from the queue.
        /// Since it has been shown, we no longer need it.
        /// </summary>
        public void NextSentence()
        {
            //this.sentence.text = this.sentenceQueue.Peek();
            StartCoroutine(WriteSentence(this.sentenceQueue.Peek()));
            this.sentenceQueue.Dequeue();
        }

        /// <summary>
        /// Search the answers by the linked id.
        /// Instantiate for every answer a button and register an onClick event.
        /// </summary>
        private void ShowAnswers()
        {
            

        DialogueText dialogueText = this.dialogueText.DialogueText.SingleOrDefault(x => x.SentenceId == this.linkedId);
            if (dialogueText.Answers.Count == 0) return;
            foreach (DialogueAnswers answer in dialogueText.Answers)
            {
                
                GameObject go = Instantiate(this.answerButton, this.content);
                go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"> { answer.AnswerDescription }";
                go.GetComponent<Button>().onClick.AddListener(() => GetNextSentences(answer.LinkedToSentenceId));
            }
        }

        bool isWriting = false;
        /// <summary>
        /// Show one letter every frame to get a animated text.
        /// </summary>
        /// <param name="sentence">The Sentence to be written.</param>
        /// <returns></returns>
        private IEnumerator WriteSentence(string sentence)
        {   
            if(!isWriting){
                isWriting = true;
                this.sentence.text = $"{ this.dialogueText.NameOfNpc }: ";
                foreach (char letter in sentence)
                {
                    this.sentence.text += letter;
                    if(letter == '.')
                    {
                        yield return new WaitForSeconds(0.2f);
                    }
                    yield return new WaitForSeconds(1 / this.writingSpeed);
                }

                ShowAnswers();
                isWriting = false;
            }
        }

        /// <summary>
        /// Delete all child's of the content.
        /// </summary>
        private void DeleteOldAnswers()
        {
            if (this.content.childCount > 0)
            {
                foreach (Transform tr in this.content)
                {
                    Destroy(tr.gameObject);
                }
            }
        }
    }
}

