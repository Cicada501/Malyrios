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
    private static GameObject currentLevel;
    public static string CurrentLevelName;
    private string prevLevelName;
    [SerializeField] private GameObject player;
    [SerializeField] private SaveLoadPlayer saveLoadPlayer;
    private CinemachineVirtualCamera cam;
    private float originalDeadZoneWidth;
    private float originalDeadZoneHeight;
    private void Awake()
    {
        ChangeLevel("Cave");
        

        if (saveLoadPlayer.SpawnAtPlayerDebugLocation)
        {
            var startpoint = GameObject.Find("CaveStartpoint").GetComponent<Transform>();
            player.transform.position = startpoint.position;
        }
        //this will be used to detect the correct spawn-point in the world, depending from where you come
        //prevLevelName = "HighForest";
        cam = ReferencesManager.Instance.camera;
    }
    

    public void ChangeLevel(string levelName)
    {
        
        Destroy(currentLevel);
        currentLevel = Instantiate(level.Find(level1 => level1.Name == levelName).Prefab);
        CurrentLevelName = levelName;
        
        //get the Decision script (is on the same GameObject)
        var decisionManager = GetComponent<Decision>();
        
        if (levelName == "Cave")
        {
            //if Cave is loaded assign the bigRat GameObjects to the respective variables in the Decision script
            decisionManager.bigRatNpc = currentLevel.transform.Find("BigRatNPC").gameObject;
            decisionManager.bigRatEnemy = currentLevel.transform.Find("BigRatEnemy").gameObject;
            
        }else if (levelName == "HighForest")
        {
            decisionManager.wizzardDialog = currentLevel.transform.Find("Wizzard").GetComponent<Dialogue>();
        }
        
        
        //print(prevLevelName);
        var startpoint = GameObject.Find($"{prevLevelName}LandingPoint").GetComponent<Transform>();
        prevLevelName = levelName; //after we used it to detect the right LandingPoint, we can update the value.
        
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