using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour

{

    [SerializeField] AudioSource MusicNormal = null;
    [SerializeField] AudioSource MusicDark = null;
    [SerializeField] AudioSource MusicVillage = null;
    [SerializeField] AudioSource MusicCave = null;
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
        if (changeMusic.CurrentlyPlaying == "Dark" && currentlyPlaying != "Dark")
        {
            MusicNormal.Stop();
            MusicVillage.Stop();
            MusicDark.Play();
            currentlyPlaying = "Dark";

        }
        else if (changeMusic.CurrentlyPlaying == "Village" && currentlyPlaying != "Village")
        {
            MusicDark.Stop();
            MusicNormal.Stop();
            MusicVillage.Play();
            currentlyPlaying = "Village";

        }
        else if (changeMusic.CurrentlyPlaying == "Cave" && currentlyPlaying != "Cave")
        {
            MusicDark.Stop();
            MusicVillage.Stop();
            MusicNormal.Stop();
            MusicCave.Play();
            currentlyPlaying = "Cave";
        }
        else if (changeMusic.CurrentlyPlaying == "Normal" && currentlyPlaying != "Normal")
        {
            MusicDark.Stop();
            MusicVillage.Stop();
            MusicCave.Stop();
            MusicNormal.Play();
            currentlyPlaying = "Normal";
        }
    }
}
