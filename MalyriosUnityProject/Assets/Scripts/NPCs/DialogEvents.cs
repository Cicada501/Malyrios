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
    [SerializeField] private QuestLogWindow questLogWindow;


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
    private NPC oris;
    private NPC thea;
    private bool addedDialogAnswer3;
    private bool addedDialogAnswer4;


    private void Start()
    {
        schattenRose = ItemDatabase.GetItem(30);
        werwolfBlut = ItemDatabase.GetItem(31);
        schirmlinge = ItemDatabase.GetItem(32);
        addedDialogAnswer = false;
        addedDialogAnswer2 = false;
        addedDialogAnswer3 = false;
        addedDialogAnswer4 = false;

    }

    private void Update()
    {
        if (LevelManager.CurrentLevelName == "Level 4")
        {
            //wenn 3 speere im inventar und quest angenommen, thrimbald questatstus = 3
           if(Inventory.CountOccurrences(ItemDatabase.GetItem(70)) > 2 &&
              npcManager.npcs["Thrimbald"].CurrentDialogState == 2)
           {
               npcManager.npcs["Thrimbald"].QuestStatus = 3; 
               npcManager.npcs["Thrimbald"].CurrentDialogState = 3; 
           }
        }
        /*if (LevelManager.CurrentLevelName == "HighForest")
        {
            //check if player found pages
            if (Inventory.CountOccurrences(ItemDatabase.GetItem(40)) > 2 && !addedDialogAnswer4)
            {
                npcManager.npcs["Thrimbald"].CurrentDialogState = 3;
                npcManager.npcs["Thrimbald"].QuestStatus = 3;

            }
            
            //Check if player found splitter
            if (Inventory.CountOccurrences(ItemDatabase.GetItem(50)) > 0 && !addedDialogAnswer3 && npcManager.npcs["Thrimbald"].CurrentDialogState==4)
            {
                //var ans = new DialogueAnswers();
                // ans.LinkedToSentenceId = 1;
                // ans.AnswerDescription =
                //     "Ich habe übringens diesen Seltsamen Stein hier gefunden. Er scheint zu leuchten, weißt du etwas darüber?";
                // ans.Decision = "";
                // npcManager.npcs["Thrimbald"].allDialogs[2].dialogTexts[3].Answers.Add(ans);
                addedDialogAnswer3 = true;
                npcManager.npcs["Thrimbald"].CurrentDialogState = 5;
                npcManager.npcs["Thrimbald"].QuestStatus = 1;
            }
            if (Inventory.CountOccurrences(ItemDatabase.GetItem(33)) > 0 && !addedDialogAnswer)
            {
                var ans = new DialogueAnswers();
                ans.LinkedToSentenceId = 1;
                ans.AnswerDescription =
                    "Ja, ich habe einen Weg gefunden es herzustellen. Hier ist es *übergebe Heilmittel*";
                ans.Decision = "changeSonSprite";
                npcManager.npcs["Tommy"].allDialogs[2].dialogTexts[0].Answers.Add(ans);
                addedDialogAnswer = true;
                npcManager.npcs["Tommy"].QuestStatus = 3;
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
                    npcManager.npcs["Asmilda"].CurrentDialogState; //Update state after answer is added
                addedDialogAnswer2 = true;
                npcManager.npcs["Asmilda"].QuestStatus = 3;
            }
        }*/
    }


    public void FireEvent(string eventName)
    {
        npcManager.npcs.TryGetValue("Tommy", out tommy);
        npcManager.npcs.TryGetValue("Thrimbald", out thrimbald);
        npcManager.npcs.TryGetValue("Asmilda", out asmilda);
        npcManager.npcs.TryGetValue("Jack", out jack);
        npcManager.npcs.TryGetValue("Oris", out oris);
        npcManager.npcs.TryGetValue("Thea", out thea);
        
        switch (eventName)
        {
            case "":
                return;

            //Die Suche nach den Speeren
            case "Wizzard2":
                thrimbald.CurrentDialogState = 2;
                thrimbald.QuestStatus = 2;
                questLogWindow.AddQuest("Die Suche nach den Speeren", "Sammle 3 Speere der Jägerinnen und bringe sie Thrimbald dem Zauberer");
                break;
            case "finishSpearQuest":
                Inventory.Instance.Remove(ItemDatabase.GetItem(70));
                Inventory.Instance.Remove(ItemDatabase.GetItem(70));
                Inventory.Instance.Remove(ItemDatabase.GetItem(70));
                thrimbald.QuestStatus = 0;
                fireballButton.SetActive(true);
                break;
            case "Wizzard4": 
                thrimbald.CurrentDialogState = 4;
                break;
            case "TheaShop":
                ShopWindow.Instance.activeShop = thea.GetComponent<Shop>();
                ShopWindow.Instance.ShowShopWindow();
                break;
            case "giveNote":
                Inventory.Instance.AddItem(ItemDatabase.GetItem(43));
                break;
            default:
                return;
        }
    }
}