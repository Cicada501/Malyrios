using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData{
    private static Vector3 _spawnPoint = new Vector3(0f, -20f, 0f);

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
}

