using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHolder : MonoBehaviour
{
    public static SoundHolder Instance;
    
    public AudioSource[] huntressAttackSound;
    public AudioSource huntressSpearSpawnSound;
    public AudioSource werewolfAttackSound;
    public AudioSource openGateSound;
    public AudioSource closeGateSound;
    public AudioSource startFireballSound;
    public AudioSource fireballImpactSound;
    private void Awake()
    {
        Instance = this;
    }
}
