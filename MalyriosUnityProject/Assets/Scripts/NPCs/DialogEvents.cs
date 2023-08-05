using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Dialogue;
using Malyrios.Items;
using NPCs;
using UnityEngine;

public class DialogEvents : MonoBehaviour
{
    [SerializeField] private GameObject fireballButton;

    [SerializeField] private NPCManager npcManager;


    private static BaseItem apple;
    private static BaseItem schattenRose;
    private static BaseItem werwolfBlut;
    private static BaseItem schirmlinge;
    private bool addedDialogAnswer = false;
    private bool addedDialogAnswer2 = false;
    private NPC tommy;
    private NPC thrimbald;
    private NPC asmilda;
    private NPC jack;
    

    private void Start()
    {
        schattenRose = ItemDatabase.GetItem(30);
        werwolfBlut = ItemDatabase.GetItem(31);
        schirmlinge = ItemDatabase.GetItem(32);
        addedDialogAnswer = false;
        addedDialogAnswer2 = false;

    }

    private void Update()
    {
        if (LevelManager.CurrentLevelName == "HighForest")
        {
            if (Inventory.CountOccurrences(ItemDatabase.GetItem(33)) > 0 && !addedDialogAnswer)
            {
                var ans = new DialogueAnswers();
                ans.LinkedToSentenceId = 1;
                ans.AnswerDescription =
                    "Ja, ich habe einen Weg gefunden es herzustellen. Hier ist es *übergebe Heilmittel*";
                ans.Decision = "changeSonSprite";
                npcManager.npcs["Tommy"].allDialogs[2].dialogTexts[0].Answers.Add(ans);
                addedDialogAnswer = true;
                tommy.QuestStatus = 3;
            }

            //print($"Schattenrose: {Inventory.CountOccurrences(schattenRose) > 0}, Blut: {Inventory.CountOccurrences(werwolfBlut) > 0}, schirmlinge: {Inventory.CountOccurrences(schirmlinge) > 2}");
            if (Inventory.CountOccurrences(schattenRose) > 0 && Inventory.CountOccurrences(werwolfBlut) > 0 &&
                Inventory.CountOccurrences(schirmlinge) > 1 && !addedDialogAnswer2)
            {
                print("Adding asmilda Dialog Option");
                var ans = new DialogueAnswers();
                ans.LinkedToSentenceId = 1;
                ans.AnswerDescription =
                    "Ja, hier sind die Sachen";
                npcManager.npcs["Asmilda"].allDialogs[2].dialogTexts[0].Answers.Add(ans);
                npcManager.npcs["Asmilda"].CurrentDialogState =
                    npcManager.npcs["Asmilda"].CurrentDialogState; //Update state after anser is added
                addedDialogAnswer2 = true;
                asmilda.QuestStatus = 3;
            }
        }
    }


    public void FireEvent(string eventName)
    {
        tommy = npcManager.npcs["Tommy"];
        thrimbald = npcManager.npcs["Thrimbald"];
        asmilda = npcManager.npcs["Asmilda"];
        jack = npcManager.npcs["Jack"];
        
        switch (eventName)
        {
            case "":
                return;
            case "learn Fireball":
                fireballButton.SetActive(true);
                PlayerData.LearnedFireball = true;
                break;
            case "BigRatAttack":
                npcManager.npcs["Debby"].IsAggressive = true;
                break;
            case "Wizzard2":
                thrimbald.CurrentDialogState = 2;
                thrimbald.QuestStatus = 2;
                break;
            case "get apples":
                apple = ItemDatabase.GetItem(10);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                break;
            case "JackToldPlayerAboutTommy":
                tommy.CurrentDialogState = 2;
                tommy.QuestStatus = 3;
                jack.QuestStatus = 2;
                break;
            case "smallWerewolfAttack":
                tommy.IsAggressive = true;
                break;
            case "BringAntiWerewolfPotion":
                jack.CurrentDialogState = 2;
                asmilda.CurrentDialogState = 2;
                asmilda.QuestStatus = 3;
                tommy.CurrentDialogState = 3; //answer to give potin gets added, if inventory contains potion
                tommy.QuestStatus = 2;
                break;
            case "gettingIngredients":
                asmilda.CurrentDialogState = 3;
                asmilda.QuestStatus = 2;
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

                asmilda.QuestStatus = 0;

                break;
            case "changeSonSprite":
                jack.CurrentDialogState = 3;
                tommy.gameObject.GetComponent<Animator>().SetTrigger("TurnIntoHuman");
                var son = tommy.gameObject.transform;
                son.localScale = new Vector3(1f, 1f, 1f);
                var position = son.position;
                position = new Vector3(position.x - 0.05f, position.y + 0.07f, 0f); //reposition after resize
                son.position = position;
                tommy.QuestStatus = 0;
                jack.QuestStatus = 3;
                break;
            case "getSpell":
                jack.CurrentDialogState = 4;
                jack.QuestStatus = 0;
                Inventory.Instance.AddItem(ItemDatabase.GetItem(34));
                tommy.gameObject.SetActive(false);
                break;
            case "getSword":
                jack.CurrentDialogState = 4;
                jack.QuestStatus = 0;
                Inventory.Instance.AddItem(ItemDatabase.GetItem(1));
                tommy.gameObject.SetActive(false);
                break;
            default:
                return;
        }
    }
}