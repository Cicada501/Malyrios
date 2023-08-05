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

    private void Start()
    {
        questWindow.SetActive(false);
        AddQuest("Test Title", "Testing the test description, Testing the test description, Testing the test description");
        //AddQuest("Test Title", "Testing the test description");
        
    }

    public void ToggleQuestWindow()
    {
        questWindow.SetActive(!questWindow.activeSelf);
        
    }



    public void AddQuest(string title, string description)
    {
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



        public void UpdateQuestDescription(string title, string newDescription)
        {
            // Durchlaufen Sie alle Kinder von "listOfQuests"
            for (int i = 0; i < listOfQuests.transform.childCount; i++)
            {
                GameObject child = listOfQuests.transform.GetChild(i).gameObject;

                // Finden Sie das "title+marker/title"-GameObject und lesen Sie seinen Text aus
                TextMeshProUGUI titleText = child.transform.Find("title+marker/title").GetComponent<TextMeshProUGUI>();
        
                // Überprüfen Sie, ob der Text mit dem übergebenen Titel übereinstimmt
                if (titleText.text == title)
                {
                    // Finden Sie das "desc+offsetimg/description"-GameObject und aktualisieren Sie seinen Text
                    TextMeshProUGUI descriptionText = child.transform.Find("desc+offsetimg/description").GetComponent<TextMeshProUGUI>();
                    descriptionText.text = newDescription;

                    // Aktualisieren Sie auch die Beschreibung in der Quest-Liste
                    var quest = quests.Find(q => q.questName == title);
                    if (quest != null)
                    {
                        quest.questDescription = newDescription;
                    }

                    // Verlassen Sie die Schleife, da Sie die gesuchte Quest bereits gefunden haben
                    break;
                }
            }
        }


    public void RemoveQuest(string title, string description)
    {
        
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
}

[Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    
}