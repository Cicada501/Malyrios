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
        var decisionManager = GetComponent<Decision>();

        if (levelName == "HighForest")
        {
            GameObject wizzard = currentLevel.transform.Find("NPCs/Wizzard").gameObject;
            decisionManager.wizzardDialog = wizzard.GetComponent<Dialogue>();
            decisionManager.wizardNormalDialogueText = decisionManager.wizzardDialog.DialogueText;
        }else if (levelName == "Cave")
        {
            decisionManager.bigRatNpc = currentLevel.transform.Find("BigRatNPC").gameObject;
            decisionManager.bigRatEnemy = currentLevel.transform.Find("BigRatEnemy").gameObject;
        }
        
        if (prevLevelName!=null) {
            var startpoint = GameObject.Find($"{prevLevelName}LandingPoint").GetComponent<Transform>();
            player.transform.position = startpoint.position;
        }
        if (spawnAtPlayerDebugLocation && prevLevelName==null)
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