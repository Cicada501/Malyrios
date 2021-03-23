using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour

{

    [SerializeField] AudioSource MusicNormal = null;
    [SerializeField] AudioSource MusicDark = null;
    [SerializeField] AudioSource MusicVillage = null;
    string currentlyPlaying;
    // Start is called before the first frame update
    void Start()
    {
        MusicNormal.Play();
        currentlyPlaying = "Normal";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (changeMusic.playerInZone == "Dark" && currentlyPlaying != "Dark")
        {
            MusicNormal.Stop();
            MusicVillage.Stop();
            MusicDark.Play();
            currentlyPlaying = "Dark";

        }
        else if (changeMusic.playerInZone == "Village" && currentlyPlaying != "Village")
        {
            MusicDark.Stop();
            MusicNormal.Stop();
            MusicVillage.Play();
            currentlyPlaying = "Village";

        }
    }
}
