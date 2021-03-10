using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData{
    private static Vector3 _spawnPoint = new Vector3(0f, -20f, 0f);   //(0f, -20f, 0f);    (24.5f, -10.8f, 0f)
    private static List<Item> _itemsStatic = new List<Item>();

    public static Vector3 spawnPoint 
    {
        get 
        {
            return _spawnPoint;
        }
        set 
        {
            _spawnPoint = value;
        }
    }

    public static List<Item> itemsStatic
    {
        get 
        {
            return _itemsStatic;
        }
        set 
        {
            _itemsStatic = value;
        }
    }
}

