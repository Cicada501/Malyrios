using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogWindow : MonoBehaviour
{
    
    [SerializeField] private GameObject questWindow;
    public List<Quest> quests = new List<Quest>();


    public void ToggleQuestWindow()
    {
        questWindow.SetActive(!questWindow.activeSelf);
    }
}

[Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    
}