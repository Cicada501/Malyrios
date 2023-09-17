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
        baseAttributes.MaxHealth += 100 * scrollData.healthScrollsUsed;
        baseAttributes.Strength += 10 * scrollData.strengthScrollsUsed;
        baseAttributes.Energy += 10 * scrollData.intScrollsUsed;
        baseAttributes.Haste += 10 * scrollData.hasteScrollsUsed;
        baseAttributes.Balance += 10 * scrollData.balanceScrollsUsed;
    }
}


[System.Serializable]
public class ScrollData
{
    public int healthScrollsUsed;
    public int strengthScrollsUsed;
    public int intScrollsUsed;
    public int hasteScrollsUsed;
    public int balanceScrollsUsed;

    public ScrollData()
    {
        healthScrollsUsed = 0;
        strengthScrollsUsed = 0;
        intScrollsUsed = 0;
        hasteScrollsUsed = 0;
        balanceScrollsUsed = 0;
    }
}