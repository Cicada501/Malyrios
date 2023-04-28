using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Malyrios.Dialogue;
using Malyrios.Items;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class DialogueTextListWrapper
{
    public List<DialogueText> dialogueTexts;
}

public class Decision : MonoBehaviour
{
    //public static to use them in the SetDecision() Function
    public static bool BigRatAttack;
    public static bool LearnedFireball;
    public static int WizardDialogueState = 1;
    public static int SonDialogueState = 1;
    public static int HunterDialogState = 1;
    public static int HealerDialogState = 1;

    //public static bool JackToldPlayerAboutTommy;
    public static bool SmallWerewolfAttack;
    public static bool BringAntiWerewolfPotion;


    //fireball + wizard
    public GameObject fireballButton;

    [Header("Gets set during runtime (in LevelManager), when HighForest gets loaded")]
    //Wizard (Thrimbald)
    public Dialogue wizzardDialog;

    public List<DialogueText> wizzardDialogText2;
    public List<DialogueText> wizzardDialogText1;

    //Hunter (Jack)
    public Dialogue hunterDialog;
    public List<DialogueText> hunterDialogText2;
    public List<DialogueText> hunterDialogText1;

    //Son (Tommy)
    public Dialogue sonDialog;
    public List<DialogueText> sonDialogText2;
    public List<DialogueText> sonDialogText1;

    //Healer (Asmilda)
    public Dialogue healerDialog;
    public List<DialogueText> healerDialogText2;
    public List<DialogueText> healerDialogText1;

    //big rat
    [Header("Gets set during runtime (in LevelManager), when Cave gets loaded")]
    public GameObject bigRatNpc;

    public GameObject bigRatEnemy;
    public GameObject smallWerewolfNpc;
    public GameObject smallWerewolfEnemy;
    private static BaseItem apple;
    private static BaseItem schattenRose;
    private static BaseItem werwolfBlut;
    private static BaseItem schirmlinge;
    
    private void SaveToJsonFile()
    {
        // Wrap the list in a serializable object
        DialogueTextListWrapper wrapper = new DialogueTextListWrapper { dialogueTexts = healerDialogText1 };

        // Convert the object to a JSON string
        string json = JsonUtility.ToJson(wrapper, prettyPrint: true);

        // Save the JSON string to a file on your desktop
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "hunterDialogText1.json");
        File.WriteAllText(filePath, json);
    }

    private void Start()
    {
        schattenRose = ItemDatabase.GetItem(30);
        werwolfBlut = ItemDatabase.GetItem(31);
        schirmlinge = ItemDatabase.GetItem(32);
    }

    private void Update()
    {
        fireballButton.SetActive(LearnedFireball);
        if (LevelManager.CurrentLevelName == "Cave")
        {
            if (BigRatAttack)
            {
                bigRatNpc.SetActive(false);
                bigRatEnemy.SetActive(true);
            }
            else
            {
                bigRatNpc.SetActive(true);
                bigRatEnemy.SetActive(false);
            }
        }
        else if (LevelManager.CurrentLevelName == "HighForest")
        {
            switch (WizardDialogueState)
            {
                case 1:
                    wizzardDialog.DialogueText = wizzardDialogText1;
                    break;
                case 2:
                    wizzardDialog.DialogueText = wizzardDialogText2;
                    break;
                case 3:
                    break;
            }

            switch (HunterDialogState)
            {
                case 1:
                    hunterDialog.DialogueText = hunterDialogText1;
                    break;
                case 2:
                    hunterDialog.DialogueText = hunterDialogText2;
                    break;
                case 3:
                    break;
            }

            switch (SonDialogueState)
            {
                case 1:
                    sonDialog.DialogueText = sonDialogText1;
                    break;
                case 2:
                    sonDialog.DialogueText = sonDialogText2;
                    break;
                case 3:
                    break;
            }

            switch (HealerDialogState)
            {
                case 1:
                    healerDialog.DialogueText = healerDialogText1;
                    break;
                case 2:
                    healerDialog.DialogueText = healerDialogText2;
                    break;
            }

            if (Inventory.CountOccurrences(schattenRose) > 0 && Inventory.CountOccurrences(werwolfBlut) > 0 &&
                Inventory.CountOccurrences(schirmlinge) > 1)
            {
                ReplaceDialogueTextSubstring(healerDialogText1, ", du gehst wohl gerne klettern?", " und was hast du denn da für schöne sachen dabei?");
                print(("Replaced Dialog text"));
            }

            if (SmallWerewolfAttack)
            {
                smallWerewolfNpc.SetActive(false);
                smallWerewolfEnemy.SetActive(true);
            }
            else
            {
                smallWerewolfNpc.SetActive(true);
                smallWerewolfEnemy.SetActive(false);
            }
        }
    }

    public void ReplaceDialogueTextSubstring(List<DialogueText> dialogueList, string searchString, string replacementString)
    {
        // Iterate through the dialogue list
        for (int i = 0; i < dialogueList.Count; i++)
        {
            // Iterate through the sentences in the current DialogueText
            for (int j = 0; j < dialogueList[i].Sentences.Count; j++)
            {
                // If the current sentence contains the search string, replace the substring with the replacement string
                if (dialogueList[i].Sentences[j].Contains(searchString))
                {
                    dialogueList[i].Sentences[j] = dialogueList[i].Sentences[j].Replace(searchString, replacementString);
                    return; 
                }
            }
        }
    }



    public void ResetAllDecisions()
    {
        BigRatAttack = false;
        LearnedFireball = false;
        WizardDialogueState = 1;
    }

    public static void SetDecision(string answerDecision)
    {
        switch (answerDecision)
        {
            case "":
                return;
            case "learn Fireball":
                LearnedFireball = true;
                break;
            case "BigRatAttack":
                BigRatAttack = true;
                break;
            case "Wizzard2":
                WizardDialogueState = 2;
                break;
            case "get apples":
                apple = ItemDatabase.GetItem(10);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                break;
            case "JackToldPlayerAboutTommy":
                SonDialogueState = 2;
                break;
            case "smallWerewolfAttack":
                SmallWerewolfAttack = true;
                break;
            case "BringAntiWerewolfPotion":
                BringAntiWerewolfPotion = true;
                HunterDialogState = 2;
                HealerDialogState = 2;
                break;
            case "craftAntiWerewolfPotion":
                if (Inventory.CountOccurrences(schattenRose) > 0 && Inventory.CountOccurrences(werwolfBlut) > 0 &&
                    Inventory.CountOccurrences(schirmlinge) > 1)
                {
                    Inventory.Instance.Remove(schattenRose);
                    Inventory.Instance.Remove(werwolfBlut);
                    Inventory.Instance.Remove(schirmlinge);
                    Inventory.Instance.Remove(schirmlinge);
                    Inventory.Instance.AddItem(ItemDatabase.GetItem(33));
                }

                break;
        }
    }


    public void UpdateDecisionData(DecisionData gameDataLoadedDecisionData)
    {
        BigRatAttack = gameDataLoadedDecisionData.bigRatAttack;
        LearnedFireball = gameDataLoadedDecisionData.learnedFireball;
        WizardDialogueState = gameDataLoadedDecisionData.wizardDialogueState;
        SonDialogueState = gameDataLoadedDecisionData.sonDialogueState;
        HunterDialogState = gameDataLoadedDecisionData.hunterDialogState;
        SmallWerewolfAttack = gameDataLoadedDecisionData.smallWerewolfAttack;
        HealerDialogState = gameDataLoadedDecisionData.healerDialogState;
    }
}