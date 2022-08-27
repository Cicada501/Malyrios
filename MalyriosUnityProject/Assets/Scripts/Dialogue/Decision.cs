using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Dialogue;
using SaveAndLoad;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public static bool BigRatAttack;
    public static bool LearnedFireball;
    public static int WizardDialogueState = 1;
    
    public GameObject fireballButton;
    [SerializeField] private Dialogue wizzardDialog;
    [SerializeField] private List<DialogueText> wizzardDialogText2;
  

    // Start is called before the first frame update
    void Start()
    {
        DecisionData loadDecisions = SaveSystem.LoadDecisions();
        BigRatAttack = loadDecisions.bigRatAttack;
        LearnedFireball = loadDecisions.learnedFireball;
        WizardDialogueState = loadDecisions.wizardDialogueState;
    }

    // Update is called once per frame
    void Update()
    {
        fireballButton.SetActive(LearnedFireball);
        switch (WizardDialogueState)
        {
            case 1:
                
                break;
            case 2:
                wizzardDialog.DialogueText = wizzardDialogText2;
                //instantiate list of type DialogueText with 4 elements in it
                
                break;
            case 3:
               
                break;
        }
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
            case "bigRatAttack":
                BigRatAttack = true;
                break;
            case "Wizzard2":
                WizardDialogueState = 2;
                break;
        }

        Debug.Log($"Decision: {answerDecision}");
    }

    private void OnDestroy()
    {
        SaveSystem.SaveDecisions(this);
    }
}
