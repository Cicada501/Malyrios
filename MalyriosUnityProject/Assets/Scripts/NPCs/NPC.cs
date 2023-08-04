using System;
using System.Collections.Generic;
using Malyrios.Dialogue;
using UnityEngine;

namespace NPCs
{
    [System.Serializable]
    public class DialogueList
    {
        public List<DialogueText> dialogTexts;
    }
    
    public class NPC : MonoBehaviour
    {
        //public GameObject npcGameObject;
        public string npcName;
        public List<DialogueList> allDialogs; // Liste von Dialogen
        private bool isAggressive;
        private int questStatus;

        [SerializeField]
        private GameObject enemy;
        private int currentDialogState = 1;
        private List<DialogueText> currentDialog;
        private NPCManager npcManager;
        
        public UnityEngine.UI.Image questStatusImage;


        public void UpdateQuestStatusImage()
        {
            questStatusImage.sprite = SpriteManager.Instance.GetSpriteForQuestStatus(questStatus);
        }

        private void Awake()
        {
            npcManager = FindObjectOfType<NPCManager>();
        }

        private void Start()
        {
            npcManager.AddNpc(this);
        }
        
        public int CurrentQuestStatus // Getter and Setter for QuestStatus
        {
            get
            {
                return questStatus;
            }
            set
            {
                questStatus = value;
                npcManager.UpdateNpcData(this, "questStatus");
                UpdateQuestStatusImage();
            }
        }
        public int CurrentDialogState 
        {
            get 
            {
                return currentDialogState;
            }
            set
            {
                currentDialogState = value;
                if (currentDialogState >= 0 && currentDialogState <= allDialogs.Count) //Check if valid
                {
                    currentDialog = allDialogs[currentDialogState-1].dialogTexts;
                    gameObject.GetComponent<Dialogue>().DialogueText = currentDialog;
                }
                npcManager.UpdateNpcData(this, "currentDialogueState");
            }
        }

        public bool IsActive
        {
            get
            {
                return gameObject.activeSelf;
            }
            set
            {
                if (isAggressive) return;
                gameObject.SetActive(value);
                npcManager.UpdateNpcData(this,"isActive");
            }
        }
        public bool IsAggressive
        {
            get
            {
                return isAggressive;
            }
            set
            {
                //print($"Set {npcName} to aggressive state {value}");
                isAggressive = value;
                gameObject.SetActive(!value);
                if (value)
                {
                    if (enemy != null)
                    {
                        enemy.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("Enemy Object is Missing");
                    }
                }
                
                npcManager.UpdateNpcData(this, "isAggressive");
                
            }
        }
    }
}

