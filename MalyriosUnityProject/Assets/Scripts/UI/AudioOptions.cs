using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioOptions : MonoBehaviour
{
    [SerializeField] private GameObject audioOptionsPanel;
    [SerializeField] private Slider playerSoundsSlider;
    [SerializeField] private Slider enemySoundsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider environmentSlider;
    [SerializeField] private Slider playerAbilitiesSlider;
    [SerializeField] private Slider inventorySlider;
    [SerializeField] private Slider uiSlider;

    [SerializeField] private AudioSource[] playerSounds;
    [SerializeField] private AudioSource[] enemySounds;
    [SerializeField] private AudioSource[] music;
    [SerializeField] private AudioSource[] environment;
    [SerializeField] private AudioSource[] playerAbilities;
    [SerializeField] private AudioSource[] inventory;
    [SerializeField] private AudioSource[] ui;
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
        environmentSlider.onValueChanged.AddListener(UpdateEnvironmentVolume);
        playerAbilitiesSlider.onValueChanged.AddListener(UpdatePlayerAbilitiesVolume);
        inventorySlider.onValueChanged.AddListener(UpdateInventoryVolume);
        uiSlider.onValueChanged.AddListener(UpdateUiVolume);
    }

    private void UpdateEnvironmentVolume(float volume)
    {
        foreach (var source in environment)
        {
            source.volume = volume;
        }
    }
    
    private void UpdateInventoryVolume(float volume)
    {
        foreach (var source in inventory)
        {
            source.volume = volume;
        }
    }
    
    private void UpdateUiVolume(float volume)
    {
        foreach (var source in ui)
        {
            source.volume = volume;
        }
    }
    
    private void UpdatePlayerAbilitiesVolume(float volume)
    {
        foreach (var source in playerAbilities)
        {
            source.volume = volume;
        }
    }

    public void ToggleAudioOptionsPanel()
    {
        audioOptionsPanel.SetActive(!audioOptionsPanel.activeSelf);
        if (audioOptionsPanel.activeSelf)
        {
            var i = Random.Range(0, SoundHolder.Instance.openButton.Length);
            SoundHolder.Instance.openButton[i].Play();
        }
        else
        {
            SoundHolder.Instance.closeButton.Play();
        }
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
        gameData.SaveAudioSettings(playerSoundsSlider.value, enemySoundsSlider.value, musicSlider.value, environmentSlider.value, playerAbilitiesSlider.value, inventorySlider.value, uiSlider.value);
    }

    public void ApplyLoadedAudioSettings()
    {
        playerSoundsSlider.value = gameData.LoadedPlayerSoundsVolume;
        UpdatePlayerSoundsVolume(gameData.LoadedPlayerSoundsVolume);
        
        enemySoundsSlider.value = gameData.LoadedEnemySoundsVolume;
        UpdateEnemySoundsVolume(gameData.LoadedEnemySoundsVolume);
        
        musicSlider.value = gameData.LoadedMusicVolume;
        UpdateMusicVolume(gameData.LoadedMusicVolume);
        
        environmentSlider.value = gameData.LoadedEnvironmentVolume;
        UpdateEnvironmentVolume(gameData.LoadedEnvironmentVolume);
        
        playerAbilitiesSlider.value = gameData.LoadedPlayerAbilitiesVolume;
        UpdatePlayerAbilitiesVolume(gameData.LoadedPlayerAbilitiesVolume);
        
        inventorySlider.value = gameData.LoadedInventoryVolume;
        UpdateInventoryVolume(gameData.LoadedInventoryVolume);
        
        uiSlider.value = gameData.LoadedUiVolume;
        UpdateUiVolume(gameData.LoadedUiVolume);
    }

    private void OnApplicationQuit()
    {
        SaveCurrentAudioSettings();
    }
}