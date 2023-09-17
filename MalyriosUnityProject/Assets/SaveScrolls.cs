using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using UnityEngine;

public class SaveScrolls : MonoBehaviour
{

    public static SaveScrolls Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            print("SaveScrolls instance exists already");
        }
        else
        {
            Instance = this;
        }
    }

    public ScrollData scrollData = new();

    [SerializeField] private BaseAttributes baseAttributes;
    

    public void ApplyScrollEffects()
    {
        baseAttributes.MaxHealth += 100 * scrollData.HealthScrollsUsed;
        baseAttributes.Strength += 10 * scrollData.StrengthScrollsUsed;
        baseAttributes.Energy += 10 * scrollData.IntScrollsUsed;
    }

    private void Update()
    {
        print($"Scrolldata: {JsonUtility.ToJson(scrollData)}");
    }
}


[System.Serializable]
public class ScrollData
{
    public int HealthScrollsUsed;
    public int StrengthScrollsUsed;
    public int IntScrollsUsed;

    public ScrollData()
    {
        HealthScrollsUsed = 0;
        StrengthScrollsUsed = 0;
        IntScrollsUsed = 0;
    }
}