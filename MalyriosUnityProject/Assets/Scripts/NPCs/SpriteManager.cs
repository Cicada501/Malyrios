using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance; // Singleton instance
    
    public Sprite newQuestSprite;
    public Sprite ongoingQuestSprite;
    public Sprite completedQuestSprite;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Makes the object persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite GetSpriteForQuestStatus(int status)
    {
        switch (status)
        {
            case 0: // NO_QUEST
                return null;
            case 1: // NEW_QUEST
                return newQuestSprite;
            case 2: // ONGOING_QUEST
                return ongoingQuestSprite;
            case 3: // COMPLETED_QUEST
                return completedQuestSprite;
            default:
                Debug.LogWarning("Unknown quest status!");
                return null;
        }
    }
}

