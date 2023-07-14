using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeIntensity = 1f;
    private float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    
    public static CameraShake Instance { get; private set; }

    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        Instance = this;
    }

    private void Start()
    {
        StopShake();
    }

    public void ShakeCamera(float duration = 0.5f, float intensity = 0.2f)
    {
        _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }

    void StopShake()
    {
        _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopShake();
            }
        }
    }
}