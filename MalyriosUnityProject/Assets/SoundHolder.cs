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
    public AudioSource invOpen;
    public AudioSource invClose; 
    public AudioSource equipItem; 
    public AudioSource unequipItem; 
    public AudioSource placeRuneStone; 
    public AudioSource[] openButton;
    public AudioSource closeButton;
    public AudioSource wrongAnser;
    public AudioSource collectCoin;
    public AudioSource correctAnswer;
    public AudioSource levelComplete;
    public AudioSource pickupItem;
    public AudioSource buyItem;
    public AudioSource sellItem;
    private void Awake()
    {
        Instance = this;
    }
}
