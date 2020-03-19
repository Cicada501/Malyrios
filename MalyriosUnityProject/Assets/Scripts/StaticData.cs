﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData{
    private static Vector3 _spawnPoint = new Vector3(3f, -6f, 0f);
    private static bool _playerPutToSpawnPoint;

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
    public static bool playerPutToSpawnPoint{
        get{
            return _playerPutToSpawnPoint;

        }
        set{
            _playerPutToSpawnPoint = value;
        }
    }
}

