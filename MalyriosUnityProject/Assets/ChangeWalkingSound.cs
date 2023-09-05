using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWalkingSound : MonoBehaviour
{
    [SerializeField] private string changeSoundTo;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Sound is changing");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().ChangeRunSound(changeSoundTo);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        print("Sound is changing back");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().ChangeRunSound("grass");
        }
    }
}
