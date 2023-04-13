using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Dialogue;
using Malyrios.Items;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.Serialization;

public class Decision : MonoBehaviour
{
    
    //public static to save them into the saveFile
    public static bool BigRatAttack;
    public static bool LearnedFireball;
    public static int WizardDialogueState = 1;


    //fireball + wizard
    public GameObject fireballButton;
    public Dialogue wizzardDialog;
    [SerializeField] public List<DialogueText> wizzardDialogText2;
    public List<DialogueText> wizardNormalDialogueText;
    private static BaseItem apple;

    //big rat
    public GameObject bigRatNpc;
    public GameObject bigRatEnemy;

    private void Start()
    {
        apple = ItemDatabase.GetItem(10);
        // if (LevelManager.CurrentLevelName == "HighForest")
        // {
        //     wizzardDialog = GameObject.Find("Wizzard").GetComponent<Dialogue>();
        // }
    }

    private void Update()
    {
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
        }else if (LevelManager.CurrentLevelName == "HighForest")
        {
            switch (WizardDialogueState)
            {
                case 1:
                    wizzardDialog.DialogueText = wizardNormalDialogueText;
                    break;
                case 2:

                    wizzardDialog.DialogueText = wizzardDialogText2;
                    break;
                case 3:
                    break;
            }
        }
        fireballButton.SetActive(LearnedFireball);
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
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                break;
        }
    }



    private void OnDestroy()
    {
        //SaveSystem.SaveDecisions(this);
    }
}