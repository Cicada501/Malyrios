using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject word;
    private GameObject player;
    private bool worldLoaded = false;
    private Transform startpoint;
    
    private void Awake()
    {
        Instantiate(word);
        startpoint = GameObject.Find("LevelStartpoint").GetComponent<Transform>();
        worldLoaded = true;
        player.transform.position = startpoint.position;
    }
    
}
