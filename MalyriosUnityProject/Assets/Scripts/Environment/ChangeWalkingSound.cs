using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWalkingSound : MonoBehaviour
{
    [SerializeField] private string changeSoundTo;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Sound is changing");
            other.gameObject.GetComponent<PlayerMovement>().ChangeRunSound(changeSoundTo);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Sound is changing back");
            other.gameObject.GetComponent<PlayerMovement>().ChangeRunSound("grass");
        }
    }
}
