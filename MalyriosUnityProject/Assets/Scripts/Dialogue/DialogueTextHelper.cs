﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Dialogue
{
    [Serializable]
    public struct DialogueText
    {
        [Header("An answer can jump to this Id")]
        public int SentenceId;
        [TextArea(3, 10)]
        public List<string> Sentences;
        public List<DialogueAnswers> Answers;
    }

    [Serializable]
    public struct DialogueAnswers
    {
        [Header("Which Sentence ID should be jumped to")]
        public int LinkedToSentenceId;
        [TextArea(3, 10)]
        public string AnswerDescription;
        public string Decision; // in DialogManager this gets used if the decision is really taken (Button Pressed)
    }

}

