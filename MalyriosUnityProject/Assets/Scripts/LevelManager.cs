using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    
    private CinemachineVirtualCamera cam;
    private float originalDeadZoneWidth;
    private float originalDeadZoneHeight;
    private void Awake()
    {
        //Replace high forest here later by the last level the player was when the game was closed 
        currentLevel= Instantiate(level.Find(level => level.Name == "HighForest").Prefab);
        var startpoint = GameObject.Find("HighForestStartpoint").GetComponent<Transform>();
        player.transform.position = startpoint.position;
        prevLevelName = "HighForest";
        cam = ReferencesManager.instance.camera;
    }
    

    public void ChangeLevel(string levelName)
    {
        
        Destroy(currentLevel);
        currentLevel = Instantiate(level.Find(level1 => level1.Name == levelName).Prefab);
        print(prevLevelName);
        var startpoint = GameObject.Find($"{prevLevelName}LandingPoint").GetComponent<Transform>();
        prevLevelName = levelName;
        
        player.transform.position = startpoint.position;
        StartCoroutine(FocusPlayerCoroutine());
        
    }

    IEnumerator FocusPlayerCoroutine()
    {
        var transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();

        //keep original values
        originalDeadZoneHeight = transposer.m_DeadZoneHeight;
        originalDeadZoneWidth = transposer.m_DeadZoneWidth;
        
        // Set dead zone values to 0
        transposer.m_DeadZoneWidth = 0;
        transposer.m_DeadZoneHeight = 0;

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Reset dead zone values to original values
        transposer.m_DeadZoneWidth = originalDeadZoneWidth;
        transposer.m_DeadZoneHeight = originalDeadZoneHeight;
    }
    
}