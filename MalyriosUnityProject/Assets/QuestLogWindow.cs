using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogWindow : MonoBehaviour
{
    
    [SerializeField] private GameObject questWindow;
    [SerializeField] private GameObject questLogEntryPrefab;
    [SerializeField] private GameObject listOfQuests;
    
    public List<Quest> quests = new List<Quest>();
    
    private int toggleCount = 0;
    
    public void ToggleQuestWindow()
    {
        questWindow.SetActive(!questWindow.activeSelf);
        
    }



    public void AddQuest(string title, string description)
    {
        var quest = quests.Find(q => q.questName == title);
        if (quest != null)
        {
            Debug.LogWarning("quest already added to list");
            return;
        }
        GameObject newQuest = Instantiate(questLogEntryPrefab, listOfQuests.transform);
        
        TextMeshProUGUI titleText = newQuest.transform.Find("title+marker/title").GetComponent<TextMeshProUGUI>();
        titleText.text = title;

        TextMeshProUGUI descriptionText = newQuest.transform.Find("desc+offsetimg/description").GetComponent<TextMeshProUGUI>();
        descriptionText.text = description;

        Quest newQuestObj = new Quest { questName = title, questDescription = description };
        quests.Add(newQuestObj);
        SetTransparency(questWindow.transform,0);
        StartCoroutine(ActivateChildren());
        

    }
        private IEnumerator ActivateChildren()
        {
            for (int i = 0; i < 4; i++)
            {
                questWindow.SetActive(!questWindow.activeSelf);
                yield return new WaitForSeconds(.1f);
                questWindow.SetActive(!questWindow.activeSelf);
            }
            
            SetTransparency(questWindow.transform,1);
        }


        /// <summary>
        /// This Method searches for a quest with the given title "title" and replaces the description by "newDescription"
        /// </summary>
        /// <param name="title"> the title that is searched for</param>
        /// <param name="newDescription">the new description for the quest</param>
        public void UpdateQuestDescription(string title, string newDescription)
        {
            //Iterate over all child gameObjects of "listOfQuests"
            for (int i = 0; i < listOfQuests.transform.childCount; i++)
            {
                GameObject child = listOfQuests.transform.GetChild(i).gameObject;

                //find title text component
                TextMeshProUGUI titleText = child.transform.Find("title+marker/title").GetComponent<TextMeshProUGUI>();
                
                if (titleText.text == title)
                {
                    //find description component
                    TextMeshProUGUI descriptionText = child.transform.Find("desc+offsetimg/description").GetComponent<TextMeshProUGUI>();
                    descriptionText.text = newDescription;

                    //Update description also in the quests list
                    var quest = quests.Find(q => q.questName == title);
                    if (quest != null)
                    {
                        quest.questDescription = newDescription;
                    }
                    return; //return, because correct quest was found already
                }
            }
            Debug.LogWarning($"No quest with the title: {title}");
        }


        public void RemoveQuest(string title)
        {
            // Iterate over all child gameObjects of "listOfQuests"
            for (int i = 0; i < listOfQuests.transform.childCount; i++)
            {
                GameObject child = listOfQuests.transform.GetChild(i).gameObject;

                // Find title text component
                TextMeshProUGUI titleText = child.transform.Find("title+marker/title").GetComponent<TextMeshProUGUI>();

                if (titleText.text == title)
                {
                    // Remove the quest from the quests list
                    quests.RemoveAll(q => q.questName == title);

                    // Destroy the GameObject representing the quest in the scene
                    Destroy(child);

                    // Break loop, because correct quest was found already
                    break;
                }
            }
        }

    private void SetTransparency(Transform parent, float transparency)
    {
        var graphic = parent.GetComponent<Graphic>();
        if (graphic != null)
        {
            var color = graphic.color;
            color.a = transparency;
            graphic.color = color;
        }
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                SetTransparency(child, transparency);
            }
        }
    }

    public QuestList SaveQuestLog()
    {
        return new QuestList(quests);
    }
}

[Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    
}

[Serializable]
public class QuestList
{
    public List<Quest> questList;
    
    public QuestList(List<Quest> data)
    {
        questList = data;
    }
}