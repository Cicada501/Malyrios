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
    
    
    //fireball + wizard
    public GameObject fireballButton;
    [SerializeField] private Dialogue wizzardDialog;
    [SerializeField] private List<DialogueText> wizzardDialogText2;
  
    //big rat
    [SerializeField] private GameObject bigRatNpc;
    [SerializeField] private GameObject bigRatEnemy;


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
                break;
            case 3:
                break;
        }

        if (BigRatAttack)
        {
            
            bigRatNpc.SetActive(false);
            bigRatEnemy.SetActive(true);
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
            case "BigRatAttack":
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
