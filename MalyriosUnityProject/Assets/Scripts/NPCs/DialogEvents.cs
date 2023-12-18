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
    private NPC lirion;
    private bool addedDialogAnswer3;
    private bool addedDialogAnswer4;
    private Enemy ww0;
    private Enemy ww1;


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
        }else if (LevelManager.CurrentLevelName == "Level 5")
        {
            if(lirion == null)npcManager.npcs.TryGetValue("Lirion", out lirion);
            if (ww0 == null && ww1 == null)
            {
                ww0 = GameObject.Find("Werewolf_0").GetComponent<Enemy>();
                ww1 = GameObject.Find("Werewolf_1").GetComponent<Enemy>();
                return;
            }
            if (!ww0.enabled && !ww1.enabled && lirion.CurrentDialogState == 2)
            {
                print("werwolfs got killed");
                lirion.CurrentDialogState = 3;
                lirion.QuestStatus = 3;
            }
          
        }
        if (LevelManager.CurrentLevelName == "HighForest")
        {
            if (npcManager.npcs["Thrimbald"].CurrentDialogState == 1)
            {
                npcManager.npcs["Thrimbald"].QuestStatus = 1;
            }
            if (npcManager.npcs["Jack"].CurrentDialogState == 1)
            {
                npcManager.npcs["Jack"].QuestStatus = 1;
            }
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
        }
    }


    public void FireEvent(string eventName)
    {
        npcManager.npcs.TryGetValue("Tommy", out tommy);
        npcManager.npcs.TryGetValue("Thrimbald", out thrimbald);
        npcManager.npcs.TryGetValue("Asmilda", out asmilda);
        npcManager.npcs.TryGetValue("Jack", out jack);
        npcManager.npcs.TryGetValue("Oris", out oris);
        npcManager.npcs.TryGetValue("Thea", out thea);
        npcManager.npcs.TryGetValue("Lirion", out lirion);
        
        switch (eventName)
        {
            case "":
                return;

            //Die Suche nach den Speeren
            case "Wizzard2lvl1":
                thrimbald.CurrentDialogState = 2;
                break;
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
            case "Lirion2":
                questLogWindow.AddQuest("Besiege die Wölfe", "Besiege die Wölfe und erstette Lirion bericht, sodass er seine Reise fortsetzten kann");
                lirion.QuestStatus = 2;
                lirion.CurrentDialogState = 2;
                break;
            case "Lirion4":
                questLogWindow.RemoveQuest("Besiege die Wölfe");
                lirion.QuestStatus = 0;
                Inventory.Instance.AddItem(ItemDatabase.GetArmor(170));
                lirion.CurrentDialogState = 4;
                break;
            case "learn Fireball":
                fireballButton.SetActive(true);
                PlayerData.LearnedFireball = true;
                break;
            case "BigRatAttack":
                npcManager.npcs["Debby"].IsAggressive = true;
                break;
            
            //Die verlorenen Seiten
            case "Wizzard2HF":
                thrimbald.CurrentDialogState = 2;
                thrimbald.QuestStatus = 2;
                questLogWindow.AddQuest("Die verlorenen Seiten", "Suche nach den 3 verlorenen Buchseiten für Thrimbald");
                break;
            case "givePages":
                Inventory.Instance.Remove(ItemDatabase.GetItem(40));
                Inventory.Instance.Remove(ItemDatabase.GetItem(40));
                Inventory.Instance.Remove(ItemDatabase.GetItem(40));
                thrimbald.QuestStatus = 0;
                break;
            case "Wizzard4HF":
                Inventory.Instance.AddItem(ItemDatabase.GetItem(16));
                Inventory.Instance.AddItem(ItemDatabase.GetItem(16));
                Inventory.Instance.AddItem(ItemDatabase.GetItem(16));
                Inventory.Instance.AddItem(ItemDatabase.GetItem(16));
                Inventory.Instance.AddItem(ItemDatabase.GetItem(16));
                questLogWindow.RemoveQuest("Die verlorenen Seiten");
                thrimbald.CurrentDialogState = 4;
                break;
                
            //
            case "sucheSchattenkristall":
                questLogWindow.AddQuest("Der magische Splitter","Finde Oris den Schmied und frage ihn nach etwas Staub eines Schattenkristalls, damit Thrimbald die echtheit des Malyrios Splitters überprüfen kann");
                thrimbald.CurrentDialogState = 6;
                thrimbald.QuestStatus = 2;
                oris.CurrentDialogState = 2;
                oris.QuestStatus = 3;
                break;
            case "BuyDust":
                if (Inventory.CountOccurrences(ItemDatabase.GetItem(16)) > 4)
                {
                    Inventory.Instance.Remove(ItemDatabase.GetItem(16));
                    Inventory.Instance.Remove(ItemDatabase.GetItem(16));
                    Inventory.Instance.Remove(ItemDatabase.GetItem(16));
                    Inventory.Instance.Remove(ItemDatabase.GetItem(16));
                    Inventory.Instance.Remove(ItemDatabase.GetItem(16));
                    Inventory.Instance.AddItem(ItemDatabase.GetItem(42));
                }
                thrimbald.CurrentDialogState = 7;
                thrimbald.QuestStatus = 3;
                questLogWindow.UpdateQuestDescription("Der magische Splitter", "Bringe den Staub des Schattenkristalls zu Thrimbald, damit dieser die Echtheit des Malyrios Splitters überprüfen kann");
                oris.QuestStatus = 0;
                break;
            case "Oris3":
                oris.CurrentDialogState = 3;
                break;
            case "giveDust":
                Inventory.Instance.Remove(ItemDatabase.GetItem(42));
                break;
            
            //Hunter Quest
            case "JackToldPlayerAboutTommy":
                tommy.CurrentDialogState = 2;
                tommy.QuestStatus = 3;
                jack.QuestStatus = 2;
                jack.CurrentDialogState = 2;
                questLogWindow.AddQuest("Ein Heilmittel für Tommy", "Suche den Sohn des Jägers, um ihn davon zu überzeugen, dess er ein sich in einen Menschen zurück verwandeln muss (Achtung: Er könnte gefährlich sein)");
                break;
            case "smallWerewolfAttack":
                tommy.IsAggressive = true;
                
                break;
            case "BringAntiWerewolfPotion":
                jack.CurrentDialogState = 3;
                asmilda.CurrentDialogState = 2;
                asmilda.QuestStatus = 3;
                tommy.CurrentDialogState = 3; //answer to give potin gets added, if inventory contains potion
                tommy.QuestStatus = 2;
                
                questLogWindow.UpdateQuestDescription("Ein Heilmittel für Tommy", "Finde jemanden, der ein Heilmittel für Tommy (den Sohn des Jägers) herstellen kann");
                break;
            case "gettingIngredients":
                asmilda.CurrentDialogState = 3;
                asmilda.QuestStatus = 2;
                questLogWindow.UpdateQuestDescription("Ein Heilmittel für Tommy",
                    "Finde folgende Zutaten: 1x Schattenrose, 1x Werwolfblut, 2x Dunkelstaubschirmling und bringe sie zu Asmilda");
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
                questLogWindow.UpdateQuestDescription("Ein Heilmittel für Tommy", "Bring das Heilmittel zu Tommy");

                break;
            case "changeSonSprite":
                jack.CurrentDialogState = 4;
                tommy.gameObject.GetComponent<Animator>().SetTrigger("TurnIntoHuman");
                var son = tommy.gameObject.transform;
                son.localScale = new Vector3(1f, 1f, 1f);
                var position = son.position;
                position = new Vector3(position.x - 0.05f, position.y + 0.07f, 0f); //reposition after resize
                son.position = position;
                tommy.QuestStatus = 0;
                jack.QuestStatus = 3;

                questLogWindow.UpdateQuestDescription("Ein Heilmittel für Tommy",
                    "berichte jack, dass du seinen Sohn gerettet hast");
                break;
            case "getSpell":
                questLogWindow.RemoveQuest("Ein Heilmittel für Tommy");
                jack.CurrentDialogState = 5;
                jack.QuestStatus = 0;
                Inventory.Instance.AddItem(ItemDatabase.GetItem(34));
                tommy.gameObject.SetActive(false);
                
                break;
            case "getSword":
                questLogWindow.RemoveQuest("Ein Heilmittel für Tommy");
                jack.CurrentDialogState = 5;
                jack.QuestStatus = 0;
                Inventory.Instance.AddItem(ItemDatabase.GetWeapon(1));
                tommy.gameObject.SetActive(false);
               
                break;
            default:
                return;
        }
    }
}