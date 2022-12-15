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
    
    //cant they be private?
    public static bool BigRatAttack;
    public static bool LearnedFireball;
    public static int WizardDialogueState = 1;


    //fireball + wizard
    public GameObject fireballButton;
    private Dialogue wizzardDialog;
    [SerializeField] private List<DialogueText> wizzardDialogText2;
    private List<DialogueText> wizardNormalDialogueText;
    private static BaseItem apple;

    //big rat
    //[SerializeField] private GameObject bigRatNpc;
    //[SerializeField] private GameObject bigRatEnemy;

    private void Start()
    {
        wizzardDialog = GameObject.Find("Wizzard").GetComponent<Dialogue>();
        DecisionData loadDecisions = SaveSystem.LoadDecisions();
        BigRatAttack = loadDecisions.bigRatAttack;
        LearnedFireball = loadDecisions.learnedFireball;
        WizardDialogueState = loadDecisions.wizardDialogueState;
        wizardNormalDialogueText = wizzardDialog.DialogueText;
        apple = ItemDatabase.GetItem(10);
    }

    private void Update()
    {
        fireballButton.SetActive(LearnedFireball);
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

        // if (BigRatAttack)
        // {
        //     bigRatNpc.SetActive(false);
        //     bigRatEnemy.SetActive(true);
        // }
        // else
        // {
        //     bigRatNpc.SetActive(true);
        //     bigRatEnemy.SetActive(false);
        // }
        
    }

    public void ResetAllDecisions()
    {
        BigRatAttack = false;
        LearnedFireball = false;
        WizardDialogueState = 1;
    }

    public static void GetDecision(string answerDecision)
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
        SaveSystem.SaveDecisions(this);
    }
}