using System;
using System.Collections.Generic;
using System.IO;
using Malyrios.Dialogue;
using Malyrios.Items;
using SaveAndLoad;
using UnityEngine;

[Serializable]
public class DialogueTextListWrapper
{
    public List<DialogueText> dialogueTexts;
}

public class Decision : MonoBehaviour
{
    #region Singleton

    private static Decision _instance;

    public static Decision Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Decision>();
                if (_instance == null)
                {
                    Debug.LogError("Decision component not found in the scene.");
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("Another instance of Decision already exists.");
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    #endregion

    //public static to use them in the SetDecision() Function
    public static bool BigRatAttack;
    public static bool LearnedFireball;
    public static int WizardDialogueState = 1;
    public static int SonDialogueState = 1;
    public static int HunterDialogState = 1;
    public static int HealerDialogState = 1;
    public static bool SmallWerewolfAttack;


    //fireball + wizard
    public GameObject fireballButton;

    #region DialogTexts

    //Wizard (Thrimbald)
    public Dialogue wizzardDialog;
    public List<DialogueText> wizzardDialogText4;
    public List<DialogueText> wizzardDialogText3;
    public List<DialogueText> wizzardDialogText2;
    public List<DialogueText> wizzardDialogText1;

    //Hunter (Jack)
    public Dialogue hunterDialog;
    public List<DialogueText> hunterDialogTextTest;
    public List<DialogueText> hunterDialogText4;
    public List<DialogueText> hunterDialogText3;
    public List<DialogueText> hunterDialogText2;
    public List<DialogueText> hunterDialogText1;

    //Son (Tommy)
    public Dialogue sonDialog;
    public List<DialogueText> sonDialogText4;
    public List<DialogueText> sonDialogText3;
    public List<DialogueText> sonDialogText2;
    public List<DialogueText> sonDialogText1;

    //Healer (Asmilda)
    public Dialogue healerDialog;
    public List<DialogueText> healerDialogText3;
    public List<DialogueText> healerDialogText2;
    public List<DialogueText> healerDialogText1;

    #endregion

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
    private static BaseItem splitterDerWeisheit;


    bool addedDialogAnswer = false;
    private bool addedDialogAnswer2;

    private void SaveToJsonFile()
    {
        // Wrap the list in a serializable object
        DialogueTextListWrapper wrapper = new DialogueTextListWrapper {dialogueTexts = healerDialogText1};

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
        splitterDerWeisheit = ItemDatabase.GetItem(50);
    }

    private void Update()
    {
        LearnedFireball = true;
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
                    wizzardDialog.DialogueText = wizzardDialogText3;
                    break;
                case 4:
                    wizzardDialog.DialogueText = wizzardDialogText4;
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
                    hunterDialog.DialogueText = hunterDialogText3;
                    break;
                case 4:
                    hunterDialog.DialogueText = hunterDialogText4;
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
                    sonDialog.DialogueText = sonDialogText3;
                    break;
                case 4:
                    sonDialog.DialogueText = sonDialogText4;
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
                case 3:
                    healerDialog.DialogueText = healerDialogText3;
                    break;
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


        if (Inventory.CountOccurrences(ItemDatabase.GetItem(33)) > 0 && !addedDialogAnswer)
        {
            var ans = new DialogueAnswers();
            ans.LinkedToSentenceId = 1;
            ans.AnswerDescription =
                "Ja, ich habe einen Weg gefunden es herzustellen. Hier ist es *übergebe Heilmittel*";
            ans.Decision = "changeSonSprite";
            sonDialogText3[0].Answers.Add(ans);
            addedDialogAnswer = true;
        }

        if (Inventory.CountOccurrences(schattenRose) > 0 && Inventory.CountOccurrences(werwolfBlut) > 0 &&
            Inventory.CountOccurrences(schirmlinge) > 1 && !addedDialogAnswer2)
        {
            var ans = new DialogueAnswers();
            ans.LinkedToSentenceId = 1;
            ans.AnswerDescription =
                "Ja, hier sind die Sachen";
            healerDialogText3[0].Answers.Add(ans);
            addedDialogAnswer2 = true;
        }

        if (Inventory.CountOccurrences(splitterDerWeisheit) > 0)
        {
            //print("i got a splitter");
            WizardDialogueState = 3;
        }
        
        //print($"WizardDialogState:{WizardDialogueState}");
    }

    public void ReplaceDialogueTextSubstring(List<DialogueText> dialogueList, string searchString,
        string replacementString)
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
                    dialogueList[i].Sentences[j] =
                        dialogueList[i].Sentences[j].Replace(searchString, replacementString);
                    return;
                }
            }
        }
    }


    public void ResetAllDecisions()
    {
        BigRatAttack = false;
        LearnedFireball = false;
        SmallWerewolfAttack = false;
        WizardDialogueState = 1;
        SonDialogueState = 1;
        HunterDialogState = 1;
        HealerDialogState = 1;
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
                HunterDialogState = 2;
                HealerDialogState = 2;
                SonDialogueState = 3; //answer to give potin gets added, if inventory contains potion
                break;
            case "gettingIngredients":
                HealerDialogState = 3;
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
            case "changeSonSprite":
                HunterDialogState = 3;
                Instance.sonDialog.GetComponent<Animator>().SetTrigger("TurnIntoHuman");
                var son = Instance.sonDialog.transform;
                son.localScale = new Vector3(1f, 1f, 1f);
                var position = son.position;
                position = new Vector3(position.x - 0.05f, position.y + 0.07f, 0f); //reposition after resize
                son.position = position;
                break;
            case "getSpell":
                HunterDialogState = 4;
                Inventory.Instance.AddItem(ItemDatabase.GetItem(34));
                Instance.sonDialog.gameObject.SetActive(false);
                break;
            case "getSword":
                HunterDialogState = 4;
                Inventory.Instance.AddItem(ItemDatabase.GetItem(1));
                Instance.sonDialog.gameObject.SetActive(false);
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