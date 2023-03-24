using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Dialogue;
using UnityEngine;

[Serializable]
public struct Level
{
    public string Name;
    public GameObject Prefab;
}
public class LevelManager : MonoBehaviour
{
    [SerializeField] private static List<Level> level;
    private static GameObject currentLevel;

    [SerializeField] private GameObject player;
    
    private void Awake()
    {
        //Replace high forest here later by the last level the player was when the game was closed 
        currentLevel= Instantiate(level.Find(level => level.Name == "HighForest").Prefab);
        var startpoint = GameObject.Find("LevelStartpoint").GetComponent<Transform>();
        player.transform.position = startpoint.position;
    }
    

    public static void ChangeLevel(string levelName)
    {
        Destroy(currentLevel);
        Instantiate(level.Find(level1 => level1.Name == levelName).Prefab);
    }
}