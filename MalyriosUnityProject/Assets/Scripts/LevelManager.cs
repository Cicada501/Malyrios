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
    public Transform EntrancePoint;
}
public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> level;
    [SerializeField] private GameObject HighForestPrefab;
    private static GameObject currentLevel;
    private string prevLevelName;
    [SerializeField] private GameObject player;
    
    private void Awake()
    {
        //Replace high forest here later by the last level the player was when the game was closed 
        currentLevel= Instantiate(level.Find(level => level.Name == "HighForest").Prefab);
        var startpoint = GameObject.Find("HighForestStartpoint").GetComponent<Transform>();
        player.transform.position = startpoint.position;
        prevLevelName = "HighForest";
    }
    

    public void ChangeLevel(string levelName)
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(level.Find(level1 => level1.Name == levelName).Prefab);
        print(prevLevelName);
        var startpoint = GameObject.Find($"{prevLevelName}LandingPoint").GetComponent<Transform>();
        player.transform.position = startpoint.position;
        prevLevelName = levelName;
    }
}