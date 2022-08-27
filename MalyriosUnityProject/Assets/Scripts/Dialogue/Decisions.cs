using System.Collections;
using System.Collections.Generic;
using Malyrios.Dialogue;
using UnityEngine;

public class Decisions : MonoBehaviour
{
    public GameObject fireballButton;
    public static bool bigRatAttack;
    public static bool learnedFireball;
    public static int wizardDialogueState = 1;
    [SerializeField] private Dialogue wizzardDialog;
    [SerializeField] private List<DialogueText> wizzardDialogText2;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireballButton.SetActive(learnedFireball);
        switch (wizardDialogueState)
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
    
    public static void SaveDecision(string answerDecision) //z.B. BigRatAttack
    {
        switch (answerDecision)
        {
            case "":
                return;
            case "learn Fireball":
                learnedFireball = true;
                break;
            case "bigRatAttack":
                bigRatAttack = true;
                break;
            case "Wizzard2":
                wizardDialogueState = 2;
                break;
        }

        Debug.Log($"Decision: {answerDecision}");
    }
}
