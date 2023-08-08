using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using Malyrios.Dialogue;
using NPCs;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Level
{
    public string Name;
    public GameObject Prefab;
    //public Transform EntrancePoint;
    public Sprite loadingScreen;
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
    [SerializeField] private Transform loadingScreenBase;
    Stopwatch stopwatch = new Stopwatch();
    private SaveActiveItems activeItemsData;

    private void Start()
    {
        cam = ReferencesManager.Instance.camera;
        activeItemsData = FindObjectOfType<SaveActiveItems>();
    }

    public void ChangeLevel(string levelName)
    {
        ShowLoadingScreen(levelName);
        if (currentLevel)
        {
            activeItemsData.SaveItems();
        }
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
        


        StartCoroutine(FocusPlayerCoroutine(levelName));
    }
    
    public void ShowLoadingScreen(string levelName)
    {
        
        // Starte die Zeitmessung
        stopwatch.Start();
        //print("showingLS");
        loadingScreenBase.Find("Image").GetComponent<Image>().sprite =
            level.Find(level1 => level1.Name == levelName).loadingScreen;
        loadingScreenBase.gameObject.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        // Stoppe die Zeitmessung
        stopwatch.Stop();
        //print($"hidingLS, loading took {stopwatch.ElapsedMilliseconds}ms");
        loadingScreenBase.Find("Image").GetComponent<Image>().sprite = null;
        loadingScreenBase.gameObject.SetActive(false);
    }

    IEnumerator FocusPlayerCoroutine(string lvl)
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
        activeItemsData.LoadItems(lvl);

        // Reset dead zone values to original values
        transposer.m_DeadZoneWidth = originalDeadZoneWidth;
        transposer.m_DeadZoneHeight = originalDeadZoneHeight;
        HideLoadingScreen();
    }

    public string GetCurrentLevelName()
    {
        return CurrentLevelName;
    }
}