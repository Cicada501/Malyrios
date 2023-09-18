using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    #region Singleton

    private void Awake()
    {
        if (Instance != null)
        {
            print("An Instance of LevelUnlock already exists");
        }
        else
        {
            Instance = this;
        }
    }

    public static LevelUnlock Instance;

    #endregion

    public int unlockedLevel;

}