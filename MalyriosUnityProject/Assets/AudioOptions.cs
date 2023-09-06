using System;
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
    private GameData gameData;
    
    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        InitializeSliders();
    }
    
    private void InitializeSliders()
    {
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
    public void SaveCurrentAudioSettings()
    {
        gameData.SaveAudioSettings(playerSoundsSlider.value, enemySoundsSlider.value, musicSlider.value);
    }

    public void ApplyLoadedAudioSettings()
    {
        playerSoundsSlider.value = gameData.LoadedPlayerSoundsVolume;
        UpdatePlayerSoundsVolume(gameData.LoadedPlayerSoundsVolume);
        enemySoundsSlider.value = gameData.LoadedEnemySoundsVolume;
        UpdateEnemySoundsVolume(gameData.LoadedEnemySoundsVolume);
        musicSlider.value = gameData.LoadedMusicVolume;
        UpdateMusicVolume(gameData.LoadedMusicVolume);
    }

    private void OnApplicationQuit()
    {
        SaveCurrentAudioSettings();
    }
}