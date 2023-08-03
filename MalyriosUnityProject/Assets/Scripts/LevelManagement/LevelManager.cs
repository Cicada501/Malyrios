using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Malyrios.Dialogue;
using NPCs;
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
    private CinemachineVirtualCamera cam;
    private float originalDeadZoneWidth;
    private float originalDeadZoneHeight;
    [SerializeField] public bool spawnAtPlayerDebugLocation;

    private void Start()
    {
        cam = ReferencesManager.Instance.camera;
    }


    public void ChangeLevel(string levelName)
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(level.Find(level1 => level1.Name == levelName).Prefab);
        CurrentLevelName = levelName;

        //get the Decision script (is on the same GameObject), to assign the 
        var npcManager = GetComponent<NPCManager>();
        npcManager.npcs.Clear();
        foreach (var npc in currentLevel.GetComponentsInChildren<NPC>())
        {
            //print($"found {npc.npcName} while changing level to {levelName}");
            npcManager.npcs[npc.npcName] = npc;
        }

        npcManager.ApplyLoadedData();

        if (prevLevelName != null)
        {
            var startpoint = GameObject.Find($"{prevLevelName}LandingPoint").GetComponent<Transform>();
            player.transform.position = startpoint.position;
        }

        if (spawnAtPlayerDebugLocation && prevLevelName == null)
        {
            var startpoint = GameObject.Find("Startpoint").GetComponent<Transform>();
            player.transform.position = startpoint.position;
        }

        prevLevelName = levelName; //after we used it to detect the right LandingPoint, we can update the value.

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

    public string GetCurrentLevelName()
    {
        return CurrentLevelName;
    }
}