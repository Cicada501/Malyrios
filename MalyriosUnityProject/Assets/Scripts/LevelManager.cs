using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Dialogue;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject word;
    [SerializeField] private GameObject player;
    //public static bool WorldLoaded = false;
    private Transform startpoint;
    
    private void Awake()
    {
        Instantiate(word);
        startpoint = GameObject.Find("LevelStartpoint").GetComponent<Transform>();
        //WorldLoaded = true;
        player.transform.position = startpoint.position;
    }
    
}
