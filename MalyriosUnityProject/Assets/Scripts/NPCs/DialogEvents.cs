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
            }
        }
    }


    public void FireEvent(string eventName)
    {
        switch (eventName)
        {
            case "":
                return;
            case "learn Fireball":
                fireballButton.SetActive(true);
                PlayerData.LearnedFireball = true;
                break;
            case "BigRatAttack":
                npcManager.npcs["BigRatNPC"].IsAggressive = true;
                break;
            case "Wizzard2":
                npcManager.npcs["Thrimbald"].CurrentDialogState = 2;
                break;
            case "get apples":
                apple = ItemDatabase.GetItem(10);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                Inventory.Instance.AddItem(apple);
                break;
            case "JackToldPlayerAboutTommy":
                npcManager.npcs["Tommy"].CurrentDialogState = 2;
                break;
            case "smallWerewolfAttack":
                npcManager.npcs["Tommy"].IsAggressive = true;
                break;
            case "BringAntiWerewolfPotion":
                npcManager.npcs["Jack"].CurrentDialogState = 2;
                npcManager.npcs["Jack"].CurrentDialogState = 2;
                npcManager.npcs["Tommy"].CurrentDialogState = 3; //answer to give potin gets added, if inventory contains potion
                break;
            case "gettingIngredients":
                npcManager.npcs["Jack"].CurrentDialogState = 3;
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
                npcManager.npcs["Jack"].CurrentDialogState = 3;
                npcManager.npcs["Tommy"].gameObject.GetComponent<Animator>().SetTrigger("TurnIntoHuman");
                var son = npcManager.npcs["Tommy"].gameObject.transform;
                son.localScale = new Vector3(1f, 1f, 1f);
                var position = son.position;
                position = new Vector3(position.x - 0.05f, position.y + 0.07f, 0f); //reposition after resize
                son.position = position;
                break;
            case "getSpell":
                npcManager.npcs["Jack"].CurrentDialogState = 4;
                Inventory.Instance.AddItem(ItemDatabase.GetItem(34));
                npcManager.npcs["Tommy"].gameObject.SetActive(false);
                break;
            case "getSword":
                npcManager.npcs["Jack"].CurrentDialogState = 4;
                Inventory.Instance.AddItem(ItemDatabase.GetItem(1));
                npcManager.npcs["Tommy"].gameObject.SetActive(false);
                break;
            default:
                return;
        }
    }
}