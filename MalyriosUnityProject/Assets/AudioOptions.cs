using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    [SerializeField] private GameObject audioOptionsPanel;
    [SerializeField] private Slider playerSoundsSlider;
    [SerializeField] private Slider enemySoundsSlider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private AudioSource[] playerSounds;
    [SerializeField] private AudioSource[] enemySounds;
    [SerializeField] private AudioSource[] music;
    
    private void Start()
    {
        InitializeSliders();
    }
    
    private void InitializeSliders()
    {
        if (playerSounds.Length > 0)
        {
            playerSoundsSlider.value = playerSounds[0].volume;
        }

        if (enemySounds.Length > 0)
        {
            enemySoundsSlider.value = enemySounds[0].volume;
        }

        if (music.Length > 0)
        {
            musicSlider.value = music[0].volume;
        }

        playerSoundsSlider.onValueChanged.AddListener(UpdatePlayerSoundsVolume);
        enemySoundsSlider.onValueChanged.AddListener(UpdateEnemySoundsVolume);
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
    }
    
    public void OpenAudioOptionsPanel()
    {
        audioOptionsPanel.SetActive(!audioOptionsPanel.activeSelf);
    }

    private void UpdatePlayerSoundsVolume(float volume)
    {
        foreach (var source in playerSounds)
        {
            source.volume = volume;
        }
    }

    private void UpdateEnemySoundsVolume(float volume)
    {
        foreach (var source in enemySounds)
        {
            source.volume = volume;
        }
    }

    private void UpdateMusicVolume(float volume)
    {
        foreach (var source in music)
        {
            source.volume = volume;
        }
    }
}