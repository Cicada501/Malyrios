using System;
using System.Collections.Generic;
using Malyrios.Dialogue;
using UnityEngine;
using UnityEngine.UI;

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
        private int currentDialogState = 1;
        private bool isAggressive;
        private int questStatus;

        [SerializeField]
        private GameObject enemy;
        private List<DialogueText> currentDialog;
        private NPCManager npcManager;

        [SerializeField] private SpriteRenderer questStatusImage;


        public void UpdateQuestStatusImage()
        {
            if (questStatus > 0 & questStatusImage!= null)
            {
                print($"Updating quest image of {npcName} to {questStatus}");
                questStatusImage.sprite = SpriteManager.Instance.GetSpriteForQuestStatus(questStatus);
            }
        }

        private void Awake()
        {
            npcManager = FindObjectOfType<NPCManager>();
            //questStatusImage = gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            npcManager.AddNpc(this);


            if (npcName == "Thrimbald" && CurrentDialogState==1)
            {
                QuestStatus = 1;
            }else if (npcName == "Jack" && CurrentDialogState == 1)
            {
                QuestStatus = 1;
            }
        }
        
        public int QuestStatus // Getter and Setter for QuestStatus
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

